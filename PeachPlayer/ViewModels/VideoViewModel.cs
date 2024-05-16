using Peach.Model.Models;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;

namespace PeachPlayer.ViewModels
{
    public class VideoViewModel : ViewModelBase
    {

        public Interaction<VideoPlayViewModel, SmallVodModel?> ShowDialog { get; }
        public ReactiveCommand<Unit, Unit> PlayVodCommand { get; }
        private readonly SmallVodModel Vod;

        public VideoViewModel(SmallVodModel vod)
        {
            Vod = vod;
            ShowDialog = new Interaction<VideoPlayViewModel, SmallVodModel?>();
            PlayVodCommand = ReactiveCommand.Create(PlayVod);
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
                var result = await ShowDialog.Handle(new VideoPlayViewModel(Vod));
            }
        }

    }
}
