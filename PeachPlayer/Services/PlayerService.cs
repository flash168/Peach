using PeachPlayer.ViewModels;
using PeachPlayer.Views;
using Peach.Model.Models;
using Avalonia.Controls;

namespace PeachPlayer.Services
{
    public class PlayerService : IPlayerService
    {

        //初始化嗅探器

        private static VideoPlayView videoPlayView;
        private static SmallVodModel vodModel;
        private static VideoPlayViewModel videoPlayViewModel;
        public PlayerService()
        {
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

        //播放
        public void Play(SmallVodModel vod)
        {
            videoPlayView.Show();
            videoPlayView.Activate();
            if (videoPlayView.WindowState == WindowState.Minimized)
                videoPlayView.WindowState = WindowState.Normal;

            if (vodModel != vod)
                videoPlayViewModel.Play(vod);

            vodModel = vod;
        }


    }
}
