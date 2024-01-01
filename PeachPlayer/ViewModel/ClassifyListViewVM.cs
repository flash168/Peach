using PeachPlayer.Models;
using System.Collections.ObjectModel;

namespace PeachPlayer.ViewModel
{
    internal class ClassifyListViewVM : NotifyProperty
    {
        private ObservableCollection<VideoModel> dataList;
        public ObservableCollection<VideoModel> DataList
        {
            get { return dataList; }
            set { SetProperty(ref dataList, value); }
        }


        public ClassifyListViewVM(string tag)
        {
            if (tag.Equals("-1"))
            {
                GetData();
            }
            else
            {
                GetDataByClass(tag);
            }
        }

        async void GetData()
        {
            var datas = await LeaderServices.Instance.GetHomeVod();
            if (datas != null)
                DataList = new ObservableCollection<VideoModel>(datas.List);
        }

        async void GetDataByClass(string tid)
        {
            var datas = await LeaderServices.Instance.GetCategory(tid, "1", "", "");
            if (datas != null)
                DataList = new ObservableCollection<VideoModel>(datas.List);
        }
    }
}
