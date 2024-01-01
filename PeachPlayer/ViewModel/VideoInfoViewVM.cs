using PeachPlayer.Models;
using System.Linq;

namespace PeachPlayer.ViewModel
{
    internal class VideoInfoViewVM : NotifyProperty
    {
        private VideoModel move;
        public VideoModel Move { get => move; set { SetProperty(ref move, value); } }

        public VideoInfoViewVM(VideoModel video)
        {
            Move = video;
            GetData(video.Vod_id);
        }

        async void GetData(string id)
        {
            var data = await LeaderServices.Instance.GetDetails(id);
            if (data != null && data.List.Count > 0)
                Move = data.List[0];
        }


    }

}
