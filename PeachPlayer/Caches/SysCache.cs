using System;
using System.IO;

namespace PeachPlayer.Caches
{
    public static class SysCache
    {
        public static string DownPath { get; set; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"video");

        public static string FfmpegPath { get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"tools\ffmpeg.exe"); } }

        //文件下载默认路径文件名
        private static readonly string DownLoadPath = "MedierDownLoads";

        /// <summary>
        /// 下载默认路径
        /// </summary>
        public static string DownloadDefaultPath
        {
            get
            {
                string tmp = Environment.CurrentDirectory;
                if (string.IsNullOrEmpty(tmp))
                {
                    if (Directory.Exists("D:\\"))
                        tmp = "D:\\";
                    else
                        tmp = "C:\\";
                }
                tmp = Path.Combine(tmp, DownLoadPath);
                return tmp;
            }
        }


    }
}
