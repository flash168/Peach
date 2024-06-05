using Peach.Application.Interfaces;
using Peach.Drpy;
using Peach.Model;
using Peach.Model.Models;
using Peach.Spider.Drpy;

namespace Peach.Application.Services
{
    public class CmsService : ICmsService
    {
        public CmsService() { }

        //js引擎实例集合
        public SiteModel Site;
        ISpider spider;
        //获取引擎（有了拿出来，没有则初始化）
        public Task<bool> InitSite(SiteModel site)
        {
            Site = site;
            switch (site.Type)
            {
                case 3:
                    spider = new JsSpiderClient();
                    break;
                case 4:
                    spider = new HipyT4Client();
                    break;
                default:
                    break;
            }
            return spider.InitSpiderAsync(site);
        }


        public Task<HomeModel> HomeAsync(string filter = "")
        {
            return spider.HomeAsync(filter);
        }

        public Task<SmallVodListModel> HomeVodAsync(string filter)
        {
            return spider.HomeVodAsync(filter);
        }

        public Task<SmallVodListModel> CategoryAsync(string tid, int pg, string filter, string extend)
        {
            return spider.CategoryAsync(tid, pg, filter, extend);
        }

        public Task<VodListModel> DetailsAsync(string ids)
        {
            return spider.DetailsAsync(ids);
        }

        public Task<SmallVodListModel> SearchAsync(string filter)
        {
            return spider.SearchAsync(filter);
        }

        public Task<PlayModel> PlayAsync(string flag, string url)
        {
            return spider.PlayAsync(flag, url, Site.flags != null ? string.Join(",", Site.flags) : "");
        }


    }
}
