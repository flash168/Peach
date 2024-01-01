using PeachPlayer.Caches;
using PeachPlayer.Models;
using System.Collections.Generic;
using System.IO;

namespace PeachPlayer.Services
{
    public class TaskData
    {
        public TaskData()
        {
            WaitTasksForM3u8 = new Queue<M3u8TaskInfo>();
            FinishTasksForM3u8 = new Queue<M3u8TaskInfo>();
            ErrorTasksForM3u8 = new Queue<M3u8TaskInfo>();

            WaitTasksForTs = new Queue<TsTaskInfo>();
            FinishTasksForTs = new Queue<TsTaskInfo>();
            ErrorTasksForTs = new Queue<TsTaskInfo>();

            WaitTasksForMp4 = new Queue<ConrertTaskInfo>();
            FinishTasksForMp4 = new Queue<ConrertTaskInfo>();
            ErrorTasksForMp4 = new Queue<ConrertTaskInfo>();
        }
        //m3u8地址下载


        public Queue<M3u8TaskInfo> WaitTasksForM3u8 { get; set; }
        public Queue<M3u8TaskInfo> FinishTasksForM3u8 { get; set; }
        public Queue<M3u8TaskInfo> ErrorTasksForM3u8 { get; set; }

        public Queue<TsTaskInfo> WaitTasksForTs { get; set; }
        public Queue<TsTaskInfo> FinishTasksForTs { get; set; }
        public Queue<TsTaskInfo> ErrorTasksForTs { get; set; }

        public Queue<ConrertTaskInfo> WaitTasksForMp4 { get; set; }
        public Queue<ConrertTaskInfo> FinishTasksForMp4 { get; set; }
        public Queue<ConrertTaskInfo> ErrorTasksForMp4 { get; set; }

    }


    public class M3u8TaskInfo
    {
        public M3u8TaskInfo(VideoModel v, string url, string name)
        {
            Name = name;
            MUrl = url;
            VideoInfo = v;
        }
        public VideoModel VideoInfo { get; set; }
        public string MUrl { get; set; }
        public string Name { get; set; }
    }

    public class TsTaskInfo
    {
        public TsTaskInfo(VideoModel v, string name, Dictionary<string, string> urls)
        {
            VideoInfo = v;
            TaskId = v.Vod_id + name;
            DownloadRoot = name;
            TUrls = new List<TsUrlInfo>();
            foreach (var item in urls)
            {
                TUrls.Add(new TsUrlInfo(item.Key, item.Value));
            }
        }
        public string TaskId { get; set; }
        public VideoModel VideoInfo { get; set; }
        public List<TsUrlInfo> TUrls { get; set; }

        private string downRoot;
        public string DownloadRoot { get { return downRoot; } set { downRoot = Path.Combine(SysCache.DownPath, VideoInfo.Vod_name, value); } }
    }

    public class TsUrlInfo
    {
        public TsUrlInfo(string url, string name)
        {
            TsUrl = url;
            TsName = name;
            TsDownOk = false;
        }
        public string TsUrl { get; set; }
        public string TsName { get; set; }
        public bool TsDownOk { get; set; }
    }

    public class ConrertTaskInfo
    {
        public ConrertTaskInfo(VideoModel v, string downP)
        {
            VideoInfo = v;
            DownPath = downP;
        }
        public ConrertTaskInfo(VideoModel v, string downP, int id)
        {
            VideoInfo = v;
            DownPath = downP;
            ProcessId = id;
        }
        public bool IsOk { get; set; } = false;
        public VideoModel VideoInfo { get; set; }
        public int ProcessId { get; set; }
        public string DownPath { get; set; }
        public string FilePath { get { return Path.Combine(DownPath, $@"files.txt"); } }
        public string OutPath { get { return Path.Combine(DownPath, $@"{VideoInfo.Vod_name}.mp4"); } }
    }


}
