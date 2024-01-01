using PeachPlayer.Models;
using PeachPlayer.uc;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PeachPlayer
{
    internal class MainWindowVM : NotifyProperty
    {
        private ObservableCollection<SitesItem> apiConfig;
        public ObservableCollection<SitesItem> ApiConfig
        {
            get { return apiConfig; }
            set { SetProperty(ref apiConfig, value); }
        }


        private ObservableCollection<ListClass> dataList;
        public ObservableCollection<ListClass> DataList
        {
            get { return dataList; }
            set { SetProperty(ref dataList, value); }
        }


        public MainWindowVM()
        {
            SniffingService.Instance.Init();
        }

        public async Task<bool> GetAPIConfig(string url)
        {

            var ok = await ApiConfigServices.Instance.LoadConfig(url);
            var sites = ApiConfigServices.Instance.GetSites(3);

            ApiConfig = new ObservableCollection<SitesItem>(sites);

            return true;
        }


        public async Task<bool> SetSite(string key)
        {
            var site = await ApiConfigServices.Instance.GetSite(key);

            return await Task.Run(() =>
            {
                return LeaderServices.Instance.InitLeader(site.Api, site.ApiData, site.Ext?.ToString());
            });
        }


        public async Task<bool> GetHome()
        {
            var datas = await LeaderServices.Instance.GetHome("");
            if (datas != null)
            {
                DataList = new ObservableCollection<ListClass>(datas.Class);
                return true;
            }
            return false;
        }

    }
}
