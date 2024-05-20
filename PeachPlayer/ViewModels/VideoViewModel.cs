using Peach.Model.Models;
using PeachPlayer.Services;
using ReactiveUI;
using Splat;
using System.Reactive;

namespace PeachPlayer.ViewModels
{
    public class VideoViewModel : ViewModelBase
    {

        public ReactiveCommand<Unit, Unit> PlayVodCommand { get; }
        private readonly SmallVodModel Vod;
        private readonly IPlayerService player;
        public VideoViewModel(SmallVodModel vod)
        {
            Vod = vod;
            PlayVodCommand = ReactiveCommand.Create(PlayVod);
            player = Locator.Current.GetService<IPlayerService>();
        }

        public string Vid => Vod.vod_id;
        public string Vname => Vod.vod_name;
        public string VRemarks => Vod.vod_remarks;
        public string VPoc => Vod.vod_pic;
        public string VContent => Vod.vod_content;


        public async void PlayVod()
        {
            if (Vod.vod_id != null)
            {
                player.Play(Vod);
            }
        }

    }
}
