using PeachPlayer.ViewModels;
using PeachPlayer.Views;
using Peach.Model.Models;
using Avalonia.Controls;
using Splat;
using Peach.Application.Interfaces;
using System.Collections.Generic;
using System;
using ReactiveUI;

namespace PeachPlayer.Services
{
    public class PlayerService : IPlayerService
    {
        //初始化嗅探器
        private readonly string snifferPath = "sniffer/main.exe";

        private static VideoPlayView videoPlayView;
        private static VideoPlayViewModel videoPlayViewModel;

        private VodModel vod = new VodModel();
        public VodModel Vod
        {
            get { return vod; }
            set { vod = value; }
        }

        private ICmsService infoService;
        public PlayerService()
        {
            infoService = Locator.Current.GetService<ICmsService>();
            videoPlayView = new VideoPlayView();
            videoPlayView.Closing += (s, e) =>
            {
                ((Window)s).Hide();
                e.Cancel = true;
            };
            videoPlayViewModel = new VideoPlayViewModel();
            videoPlayView.DataContext = videoPlayViewModel;

            // 接收消息
            MessageBus.Current.Listen<string>("SelectedJi").Subscribe(Play);
        }

        private SmallVodModel vodModel;
        //播放
        public void Show(SmallVodModel vod)
        {
            videoPlayView.Show();
            videoPlayView.Activate();
            if (videoPlayView.WindowState == WindowState.Minimized)
                videoPlayView.WindowState = WindowState.Normal;

            if (vodModel == vod) return;
            vodModel = vod;
            ShowDetails(vod);
        }

        private async void ShowDetails(SmallVodModel vod)
        {
            var datas = await infoService.DetailsAsync(vod.vod_id);
            if (datas?.List?.Count > 0)
            {
                Vod = datas.List[0];
                videoPlayViewModel.Show(Vod, MakeLines());
            }
        }
        private List<LineModel> lines = new List<LineModel>();
        private List<LineModel> MakeLines()
        {
            lines.Clear();
            var xianlus = Vod.vod_play_from?.Split("$$$");
            var xianlujishus = Vod.vod_play_url?.Split("$$$");
            if (xianlus?.Length > 0 && xianlujishus?.Length == xianlus?.Length)
            {
                for (int i = 0; i < xianlus?.Length; i++)
                {
                    var lin = new LineModel();
                    lin.LineName = xianlus[i];
                    lin.Episodes = new List<JIShuModel>();
                    var jishu = xianlujishus?[i]?.Split("#");
                    if (jishu?.Length > 0)
                        for (int j = 0; j < jishu?.Length; j++)
                        {
                            var ji = jishu[j].Split("$");
                            if (ji.Length == 2)
                                lin.Episodes.Add(new JIShuModel(ji[0], ji[1]));
                        }
                    lines.Add(lin);
                }
            }
            return lines;
        }

        private async void Play(string url)
        {
            var playdata = await infoService.PlayAsync(videoPlayViewModel.Lines[videoPlayViewModel.SelectedLine].LineName, url);
            if (playdata.parse == 0)
                videoPlayView.Play("https://newcntv.qcloudcdn.com/asp/hls/4000/0303000a/3/default/28f751281bbf46b78417e4d297ec3f2f/4000.m3u8");
            else
            {
                //去嗅探 http://127.0.0.1:5708/sniffer?url=

            }
        }


    }
}
