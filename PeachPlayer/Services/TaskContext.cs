using PeachPlayer.Caches;
using PeachPlayer.Models;
using PeachPlayer.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace PeachPlayer.Services
{
    public class TaskContext
    {
        public bool IsDoing = false;
        public TaskData TData { get; private set; }

        public TaskContext()
        { TData = new TaskData(); }

        List<VideoModel> videos = new List<VideoModel>();

        public void AddTask(VideoModel video, string url, string name)
        {
            videos.Add(video);
            TData.WaitTasksForM3u8.Enqueue(new M3u8TaskInfo(video, url, name));
        }

        //任务1(获取m3u8文件，并创建ts任务s)
        public void DownTsByM3u8(CancellationToken token)
        {
            DoTask(token, TData.WaitTasksForM3u8, async s =>
             {
                 var result = await new HttpHelper().GetMessageAsy<string>(s.MUrl);
                 if (result.IsSuccessful)
                 {
                     string[] tss = result.Content.Split('\n');
                     Uri u = null;
                     Dictionary<string, string> turls = new Dictionary<string, string>();
                     for (int i = 0; i < tss.Length; i++)
                     {
                         if (!string.IsNullOrWhiteSpace(tss[i]) && !tss[i].Contains("#"))
                         {
                             Uri turl = new Uri(new Uri(s.MUrl), tss[i]);
                             if (tss[i].ToLower().Contains(".m3u8"))
                             {
                                 TData.WaitTasksForM3u8.Enqueue(new M3u8TaskInfo(s.VideoInfo, turl.AbsoluteUri, s.Name));
                                 return;
                             }
                             else
                             {
                                 turls.Add(turl.AbsoluteUri, $"{turl.Segments[turl.Segments.Length - 1]}.ts");
                             }
                         }
                     }
                     TData.WaitTasksForTs.Enqueue(new TsTaskInfo(s.VideoInfo, s.Name, turls));
                 }
                 else
                     TData.ErrorTasksForM3u8.Enqueue(new M3u8TaskInfo(s.VideoInfo, s.MUrl, s.Name));
             });
        }

        // 公共函数
        private void DoTask<T>(CancellationToken token, Queue<T> Twait, Action<T> action)
        {
            CancellationTokenSource tcs = null;
            token.Register(() =>
            {
                if (tcs != null)
                    tcs.Cancel();
            });
            T task;
            var http = new HttpHelper();
            while (!token.IsCancellationRequested)
            {
                task = default(T);
                lock (Twait)
                {//检查下载队列
                    if (Twait.Count > 0)
                        task = Twait.Dequeue();
                }
                if (task == null)
                {
                    Thread.Sleep(100);
                    continue;
                }
                action(task);
            }
        }


        //任务2（下载ts文件）
        public void DownColudData(CancellationToken token)
        {
            DoTask(token, TData.WaitTasksForTs, s =>
            {
                foreach (var item in s.TUrls)
                {
                    //  new HttpHelper().DownLoad(item.TsUrl, s.DownloadRoot, item.TsName, s.TaskId, d => { ShowInfo(d); });
                }
                lock (TData.FinishTasksForTs)
                {
                    TData.FinishTasksForTs.Enqueue(s);
                }
            });
        }

        //private void ShowInfo( arg2)
        //{
        //    if (arg2.Complete)
        //    {
        //        lock (TData.FinishTasksForTs)
        //        {
        //            if (TData.FinishTasksForTs.Count(s => s.TaskId.Equals(arg2.VName)) <= 0) return;
        //            TData.FinishTasksForTs.First(s => s.TaskId.Equals(arg2.VName)).TUrls.First(u => u.TsName == arg2.Name).TsDownOk = true;

        //            if (TData.FinishTasksForTs.First(s => s.TaskId.Equals(arg2.VName)).TUrls.Count(u => u.TsDownOk == false) == 0)
        //            {
        //                var tt = TData.FinishTasksForTs.First(s => s.TaskId.Equals(arg2.VName));
        //                lock (TData.WaitTasksForMp4)
        //                    TData.WaitTasksForMp4.Enqueue(new ConrertTaskInfo(tt.VideoInfo, tt.DownloadRoot));
        //            }
        //        }
        //    }
        //}

        //任务3（转换ts文件-> mp4）
        public void ConvertMp4ByTs(CancellationToken token)
        {
            DoTask(token, TData.WaitTasksForMp4, s =>
            {
                var saves = s.FilePath.SaveAllTsByDirc();
                if (saves)
                {
                    int id = RealAction(s.FilePath, s.OutPath);
                    lock (TData.FinishTasksForMp4)
                    {
                        TData.FinishTasksForMp4.Enqueue(new ConrertTaskInfo(s.VideoInfo, s.DownPath, id));
                    }
                }
                else
                    TData.ErrorTasksForMp4.Enqueue(s);
            });
        }

        private int RealAction(string filepath, string outpath)
        {
            //直接根据m3u8地址下载视频并合成。
            //string com1 = $"-threads 0 -i {url} -c copy -y -bsf:a aac_adtstoasc {outpath}";
            string com = $"-f concat -safe 0 -i {filepath} -c copy -bsf:a aac_adtstoasc -threads 0 {outpath}";

            Process CmdProcess = new Process();
            CmdProcess.StartInfo.FileName = SysCache.FfmpegPath;      // 命令  
            CmdProcess.StartInfo.Arguments = com;      // 参数  

            CmdProcess.StartInfo.CreateNoWindow = true;         // 不创建新窗口  
            CmdProcess.StartInfo.UseShellExecute = false;
            CmdProcess.StartInfo.RedirectStandardInput = true;  // 重定向输入  
            CmdProcess.StartInfo.RedirectStandardOutput = true; // 重定向标准输出  
            CmdProcess.StartInfo.RedirectStandardError = true;  // 重定向错误输出
            CmdProcess.StartInfo.StandardOutputEncoding = Encoding.UTF8; //设置标准输出编码
            CmdProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            CmdProcess.OutputDataReceived += (s, d) => { Debug.WriteLine(d.Data); };
            CmdProcess.ErrorDataReceived += (s, d) => { Debug.WriteLine("-------[ERROR]:" + d.Data); };

            CmdProcess.EnableRaisingEvents = true;                      // 启用Exited事件  
            CmdProcess.Exited += new EventHandler(CmdProcess_Exited);   // 注册进程结束事件  

            CmdProcess.Start();
            //ffmpegid = CmdProcess.Id;//获取ffmpeg.exe的进程ID
            CmdProcess.BeginOutputReadLine();
            CmdProcess.BeginErrorReadLine();
            return CmdProcess.Id;
            // 如果打开注释，则以同步方式执行命令，此例子中用Exited事件异步执行。  
            // CmdProcess.WaitForExit();       

        }

        private void CmdProcess_Exited(object sender, EventArgs e)
        {
            if (sender is Process process)
            {
                if (TData.FinishTasksForMp4.Count(s => s.ProcessId == process.Id) > 0) { TData.FinishTasksForMp4.First(s => s.ProcessId == process.Id).IsOk = true; }
            }
        }
    }
}
