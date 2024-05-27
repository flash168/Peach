using Peach.Application.Interfaces;
using Peach.Model.Models;
using ReactiveUI;
using Splat;


namespace PeachPlayer.ViewModels
{
    public class VideoPlayViewModel : ViewModelBase
    {
        private VodModel vod = new VodModel();
        public VodModel Vod
        {
            get { return vod; }
            set { vod = value; this.RaiseAndSetIfChanged(ref vod, value); }
        }

        public string VodName => Vod?.vod_name;

        public string VodContent => Vod?.vod_content;

        public string Type_Remarks => Vod?.vod_remarks;

        public VideoPlayViewModel()
        {
        }

        public void Show(SmallVodModel svod)
        {
            Vod.vod_id = svod.vod_id;
            Vod.vod_name = svod.vod_name;
            Vod.vod_content = svod.vod_content;
            Vod.vod_pic = svod.vod_pic;
            Vod.vod_remarks = svod.vod_remarks;
            if (svod is VodModel vod)
            {
                Vod.typeName = vod.typeName;
                Vod.typeName = vod.typeName;

                Vod.vod_actor = vod.vod_actor;//": "贝拉·索恩 斯戴芬妮·斯考特 尼克·罗宾森 Mar",
                Vod.vod_area = vod.vod_area;//": "",
                Vod.vod_director = vod.vod_director;//": "戴斯·冯·施勒·梅耶",
                Vod.vod_play_from = vod.vod_play_from;//": "非凡",
                                                      // Vod.vod_play_url = vod.typeName;//": "HD中字$http://www.8kvod.com/p/121513-1-1/",
                Vod.vod_year = vod.vod_year;//": ""
            }
        }

        public void Stop()
        {

        }

    }
}
