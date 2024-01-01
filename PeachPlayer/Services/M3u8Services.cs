using PeachPlayer.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PeachPlayer.Services
{

    public class M3u8Services
    {
        private static object lockValue = 0;
        private static M3u8Communication instance;
        public static M3u8Communication Instance
        {
            get
            {
                if (instance == null)
                    lock (lockValue)
                    {
                        if (instance == null)
                            instance = new M3u8Communication();
                    }
                return instance;
            }
        }
    }


    public class M3u8Communication
    {
        TaskContext task;
        CancellationTokenSource cts = null;

        public M3u8Communication()
        {
            task = new TaskContext();
        }


        //添加任务
        public void AddM3u8DownTask(VideoModel video, string url, string name)
        {
            task.AddTask(video, url, name);
            StartTask();
        }

        Task GetM3u8Task;
        Task GetTsTask;
        Task ConvertTask;

        //开始任务
        public void StartTask()
        {
            cts = new CancellationTokenSource();

            if (GetM3u8Task == null)
            {
                GetM3u8Task = new Task(() => task.DownTsByM3u8(cts.Token), cts.Token, TaskCreationOptions.LongRunning);
                GetM3u8Task.Start();
            }

            if (GetTsTask == null)
            {
                GetTsTask = new Task(() => task.DownColudData(cts.Token), cts.Token, TaskCreationOptions.LongRunning);
                GetTsTask.Start();
            }
            if (ConvertTask == null)
            {
                ConvertTask = new Task(() => task.ConvertMp4ByTs(cts.Token), cts.Token, TaskCreationOptions.LongRunning);
                ConvertTask.Start();
            }
        }

        bool _shutdownFlag = false;
        //停止任务
        public async void StopTask()
        {

            if (_shutdownFlag)
                return;

            cts.Cancel();
            try
            {
                await GetM3u8Task;
                await GetTsTask;
                await ConvertTask;
            }
            catch (Exception)
            {
            }
            _shutdownFlag = true;
            //TaskContext.Instance.Save();

        }

        //任务进度反馈回调
        public void GetTaskProgress()
        {


        }










    }
}
