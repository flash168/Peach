using PeachPlayer.ViewModels;
using PeachPlayer.Views;
using Peach.Model.Models;
using Avalonia.Controls;
using Peach.Application.Interfaces;
using Splat;
using System;

namespace PeachPlayer.Services
{
    public class PlayerService : IPlayerService
    {

        //初始化嗅探器

        private static VideoPlayView videoPlayView;
        private static VideoPlayViewModel videoPlayViewModel;

        private VodModel vod = new VodModel();
        public VodModel Vod
        {
            get { return vod; }
            set { vod = value; }
        }

        private IVodInfoService infoService;
        public PlayerService()
        {
            infoService = Locator.Current.GetService<IVodInfoService>();
            videoPlayView = new VideoPlayView();
            videoPlayView.Closing += (s, e) =>
            {
                videoPlayViewModel.Stop();
                ((Window)s).Hide();
                e.Cancel = true;
            };
            videoPlayViewModel = new VideoPlayViewModel();
            videoPlayView.DataContext = videoPlayViewModel;
        }
        private SmallVodModel vodModel;
        //播放
        public async void Play(SmallVodModel vod)
        {
            videoPlayView.Show();
            videoPlayView.Activate();
            if (videoPlayView.WindowState == WindowState.Minimized)
                videoPlayView.WindowState = WindowState.Normal;

            if (vodModel == vod) return;
            vodModel = vod;

            videoPlayViewModel.Show(vod);

            var datas = await infoService.DetailsAsync(vod.vod_id);
            if (datas?.List?.Count > 0)
            {
                Vod = datas.List[0];
                videoPlayViewModel.Show(Vod);
            }

            var playurl = await infoService.SniffingAsync(Vod.vod_play_url);

            videoPlayView.Play("https://newcntv.qcloudcdn.com/asp/hls/4000/0303000a/3/default/28f751281bbf46b78417e4d297ec3f2f/4000.m3u8");

        }


    }
}
