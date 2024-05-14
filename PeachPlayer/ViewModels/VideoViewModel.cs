
using Peach.Model.Models;

namespace PeachPlayer.ViewModels
{
    public class VideoViewModel : ViewModelBase
    {

        private readonly SmallVodModel Vod;

        public VideoViewModel(SmallVodModel vod)
        {
            Vod = vod;
        }

        public string Vid => Vod.vod_id;
        public string Vname => Vod.vod_name;
        public string VRemarks => Vod.vod_remarks;
        public string VPoc => Vod.vod_pic;
        public string VContent => Vod.vod_content;

    }
}
