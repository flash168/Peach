using PeachPlayer.Models;
using PeachPlayer.uc;
using System;
using System.IO;
using System.Linq;
using Vlc.DotNet.Core;
using Vlc.DotNet.Wpf;

namespace PeachPlayer.ViewModel
{
    internal class PlayerViewVM : NotifyProperty
    {
        private VideoModel move;
        public VideoModel Move { get => move; set { SetProperty(ref move, value); } }


        VlcControl vlcVideo = null;

        public VlcControl InitVLC()
        {
            if (this.vlcVideo?.SourceProvider?.MediaPlayer != null)
            {
                this.vlcVideo.SourceProvider.MediaPlayer.PositionChanged -= MediaPlayer_PositionChanged;
                this.vlcVideo.SourceProvider.MediaPlayer.LengthChanged -= MediaPlayer_LengthChanged;
            }
            this.vlcVideo = new VlcControl();
            //this.contentCtrl.Content = this.vlcVideo;
            var libDirectory = new DirectoryInfo(System.IO.Directory.GetCurrentDirectory() + "\\LibVlc");
            this.vlcVideo.SourceProvider.CreatePlayer(libDirectory);//创建视频播放器
            this.vlcVideo.SourceProvider.MediaPlayer.PositionChanged += MediaPlayer_PositionChanged;//视频的定位移动事件
            this.vlcVideo.SourceProvider.MediaPlayer.LengthChanged += MediaPlayer_LengthChanged;//播放视频源的视频长度
            return vlcVideo;
        }

        private void MediaPlayer_LengthChanged(object? sender, VlcMediaPlayerLengthChangedEventArgs e)
        {
        }

        private void MediaPlayer_PositionChanged(object? sender, VlcMediaPlayerPositionChangedEventArgs e)
        {
        }

        public PlayerViewVM(VideoModel video)
        {
            SniffingService.Instance.OnResponseReceived += View2_OnResponseReceived;
            Move = video;
            GetData(video.Vod_id);
        }



        async void GetData(string id)
        {
            var data = await LeaderServices.Instance.GetDetails(id);
            if (data != null && data.List.Count > 0)
                Move = data.List[0];

            //var data = await LeaderServices.Instance.GetPlayInfo(flag, id);
            //if (data.jx == 1)
            //{
            //    //去解析
            //    view2.GoUrl(data.url);

            //}
        }

        public void Play(string videourl)
        {
            vlcVideo.SourceProvider.MediaPlayer.Stop();
            vlcVideo.SourceProvider.MediaPlayer.Play(new Uri(videourl));
        }

        public void Pause()
        {
            vlcVideo.SourceProvider.MediaPlayer.Pause();
        }

        private void View2_OnResponseReceived(string obj)
        {
            Play(obj);
        }
    }

}
