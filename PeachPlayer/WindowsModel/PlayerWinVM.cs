using PeachPlayer.Models;
using System;
using System.Linq;
using Vlc.DotNet.Wpf;

namespace PeachPlayer.ViewModel
{
    internal class PlayerWinVM : NotifyProperty
    {
        private PlayerData move;
        public PlayerData Move { get => move; set { SetProperty(ref move, value); } }

        string selji;
        public PlayerWinVM(PlayerData video, string ji, VlcControl vlcVideo, Action<string> _webUc)
        {
            Move = video;
            selji = ji;

            GetVideoInfo(Move.Title, Move.Collection.FirstOrDefault(s => s.Name == selji)?.Purl);
            this.vlcVideo = vlcVideo;
            webUc = _webUc;
        }

        VlcControl vlcVideo;
        Action<string> webUc;

        async void GetVideoInfo(string flag, string id)
        {
            var data = await LeaderServices.Instance.GetPlayInfo(flag, id);
            if (data.jx == 1)
            {
                //去解析
            }
            else
            {
                if (data.parse == 1)
                    webUc.Invoke(data.url);
                else
                    Play(data.url);
            }
        }

        public void Play(string videourl)
        {
            vlcVideo.SourceProvider.MediaPlayer.Play(new Uri(videourl));
        }

        public void Pause()
        {
            vlcVideo.SourceProvider.MediaPlayer.Pause();
        }


    }

}
