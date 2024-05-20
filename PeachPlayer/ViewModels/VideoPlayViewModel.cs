using Peach.Application.Interfaces;
using Peach.Model.Models;
using ReactiveUI;
using Splat;


namespace PeachPlayer.ViewModels
{
    public class VideoPlayViewModel : ViewModelBase
    {
        private VodModel vod=new VodModel();
        public VodModel Vod
        {
            get { return vod; }
            set { vod = value; this.RaiseAndSetIfChanged(ref vod, value); }
        }

        public string VodName => Vod?.vod_name;

        public string VodContent => Vod?.vod_content;

        public string Type_Remarks => Vod?.vod_remarks;

        private IVodInfoService infoService;
        public VideoPlayViewModel()
        {
            infoService = Locator.Current.GetService<IVodInfoService>();
        }

        public async void Play(SmallVodModel vod)
        {
            Vod.vod_id = vod.vod_id;
            Vod.vod_name = vod.vod_name;
            Vod.vod_content = vod.vod_content;
            Vod.vod_pic = vod.vod_pic;
            Vod.vod_remarks = vod.vod_remarks;

            await infoService.DetailsAsync(vod.vod_id);
        }

        public void Stop()
        {

        }

    }
}
