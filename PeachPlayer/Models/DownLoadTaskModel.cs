using System;
using System.Threading;

namespace PeachPlayer.Models
{
    public class DownLoadTaskModel
    {
        public DownLoadTaskModel() { }
        public DownLoadTaskModel(string url, string dir, string filename)
        {
            DUrl = url;
            SavePath = dir;
            FileName = filename;
            Status = DownStatus.Wait;
            Id = Math.Abs(Guid.NewGuid().GetHashCode());
        }

        //id
        public long Id { get; set; }
        //filename
        public string FileName { get; set; }

        //savepath
        public string SavePath { get; set; }

        //durl
        public string DUrl { get; set; }

        public DownStatus Status { get; set; }
        public CancellationTokenSource Cancellation { get; set; }

    }
}