using PeachPlayer.Models;
using PeachPlayer.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PeachPlayer
{
    public class DownloadService
    {
        private static object lockValue = 0;
        private static DownloadCommunication instance = null;
        public static DownloadCommunication Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockValue)
                        if (instance == null)
                            instance = new DownloadCommunication();
                }
                return instance;
            }
        }
    }


    public class DownloadCommunication
    {
        //线程数
        //private int TaskCount { get; set; }

        private bool IsInitialized { get; set; } = false;


        //回调函数
        public delegate void DownloadDataHandle(dynamic data);
        public event DownloadDataHandle CallBackMsgEvent;

        //下载队列集合
        List<DownLoadTaskModel> DownTasks = new List<DownLoadTaskModel>();

        LimitedConcurrencyLevelTaskScheduler scheduler;
        CancellationTokenSource Cts;
        TaskFactory factory;

        ~DownloadCommunication()
        {
            if (Cts != null)
                Cts.Cancel();
        }
        //下载初始化
        public void InitDown(int taskcount = 3)
        {
            if (taskcount < 0) throw new Exception("初始化失败，参数有误！");

            scheduler = new LimitedConcurrencyLevelTaskScheduler(taskcount);
            factory = new TaskFactory(scheduler);

            DownTasks = new List<DownLoadTaskModel>();
            Cts = new CancellationTokenSource();
            IsInitialized = true;
        }

        private object LockObj = new object();
        public void AddDownTask(string durl, string filename, string tmpPath = null)
        {
            if (!IsInitialized) throw new Exception("下载服务未初始化！");
            if (string.IsNullOrWhiteSpace(filename) || string.IsNullOrWhiteSpace(durl)) throw new Exception("文件名称不能为空！");
            if (string.IsNullOrWhiteSpace(tmpPath)) tmpPath = Path.Combine(Environment.CurrentDirectory, "Download");
            if (!Directory.Exists(tmpPath))
            {
                try
                {
                    Directory.CreateDirectory(tmpPath);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            if (Cts.IsCancellationRequested) Cts = new CancellationTokenSource();
            DownLoadTaskModel mtask = new DownLoadTaskModel(durl, tmpPath, filename);
            lock (LockObj)
                DownTasks.Add(mtask);
            CallBackMsgEvent?.Invoke(mtask);
            DoTask(mtask);
        }


        public void TaskAgain(long tid = -1)
        {
            if (Cts.IsCancellationRequested)
                Cts = new CancellationTokenSource();

            lock (LockObj)
                if (tid > 0 && DownTasks.Count(s => s.Id == tid && s.Status != DownStatus.DownLoading && s.Status != DownStatus.Start) > 0)
                {
                    DoTask(DownTasks.FirstOrDefault(s => s.Id == tid));
                }
        }


        //暂停下载
        public void TaskStop(long tid = -1)
        {
            if (tid > 0)
            {
                lock (LockObj)
                {
                    var tt = DownTasks.FirstOrDefault(s => s.Id.Equals(tid));
                    if (tt != null)
                    {
                        tt.Status = DownStatus.Cancel;
                        if (tt.Cancellation?.IsCancellationRequested == false)
                            tt.Cancellation.Cancel();
                        CallBackMsgEvent?.Invoke(tt);
                    }
                }

            }
            else
            {
                if (Cts != null)
                    Cts.Cancel();
            }
        }

        //删除任务
        public void TaskDel(long tid = -1)
        {
            if (tid > 0)
            {
                var tt = DownTasks.FirstOrDefault(s => s.Id.Equals(tid));
                if (tt != null)
                {
                    TaskStop(tt.Id);
                    lock (LockObj)
                        DownTasks.Remove(tt);
                }
            }
            else
            {
                TaskStop();
                lock (LockObj)
                    DownTasks.Clear();
            }

        }


        private void DoTask(DownLoadTaskModel data)
        {
            data.Cancellation = new CancellationTokenSource();
            data.Status = DownStatus.Start;
            CallBackMsgEvent?.Invoke(data);

            factory.StartNew(task =>
            {
                if (task is DownLoadTaskModel m && m != null)
                {
                    HttpExtend.DownLoad(m.DUrl, m.FileName, m.SavePath, m.Id, m.Cancellation, r => { ProgressInfo(r); });
                    m.Cancellation.Dispose();
                    m.Cancellation = null;
                }
            }, data, Cts.Token);
            //    .ContinueWith(t =>
            //{
            //    var ta = t.AsyncState as DownLoadTaskModel;
            //    if (t.Status == TaskStatus.RanToCompletion && ta != null)
            //    {
            //        lock (LockObj)
            //        {
            //            DownTasks.First(s => s.Id == ta.Id).Status = DownStatus.Complete;
            //            if (DownTasks.Count(s => s.Status != DownStatus.Complete) == 0)
            //            {
            //                Debug.WriteLine($"下载完成！！！总下载{DownTasks.Count}条");
            //            }
            //        }
            //    }
            //    else
            //    {
            //        throw new Exception();
            //    }
            //    CallBackMsgEvent?.Invoke(ta);
            //});
        }

        private void ProgressInfo(dynamic data)
        {
            if (data is DownloadData down)
            {
                lock (LockObj)
                {
                    DownTasks.First(s => s.Id == down.Id).Status = down.Status;
                    if (DownTasks.Count(s => s.Status != DownStatus.Complete) == 0)
                    {
                        Debug.WriteLine($"下载完成！！！总下载{DownTasks.Count}条");
                    }
                    if (down.Status == DownStatus.Error)
                    {
                        Debug.WriteLine($"下载失败！！！{DownTasks.First(s => s.Id == down.Id).DUrl}");
                    }

                }
            }
            CallBackMsgEvent?.Invoke(data);
        }


    }

}
