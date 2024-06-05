using Peach.Infrastructure.Extends;
using Peach.Model;
using Peach.Model.Models;
using RestSharp;

namespace Peach.Spider.Drpy
{
    public class HipyT4Client : ISpider
    {
        public HipyT4Client()
        { }
        SiteModel Site;
        RestClient client;
        private Dictionary<string, string> headers;
        private string pwd;
        //http://www.smarth.top:5707/api/v1/vod/两个BT?pwd=dzyyds
        public Task<bool> InitSpiderAsync(SiteModel _site)
        {
            headers = new Dictionary<string, string>();
            Site = _site;
            var tup = new Uri(_site.Api);
            var query = tup.Query.Trim('?').Split('=');
            if (query.Length > 1)
                pwd = query[1];

            var options = new RestClientOptions()
            {
                BaseUrl = new Uri($"{tup.Scheme}://{tup.Authority}{tup.LocalPath}"),
                // RemoteCertificateValidationCallback = (a, c, d, v) => true,
                MaxTimeout = 60000,
                ThrowOnAnyError = false,  //设置不然不会报异常
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/110.0.0.0 Safari/537.36"
            };
            client = new RestClient(options);
            return Task.FromResult(true);
        }

        private RestRequest CreatRequest()
        {
            RestRequest req = new RestRequest();
            req.Method = Method.Get;
            req.AddQueryParameter("pwd", pwd);
            req.AddQueryParameter("extend", Site.Ext?.ToString());
            foreach (var item in headers)
            {
                req.AddQueryParameter(item.Key, item.Value);
            }
            return req;
        }

        //?pwd=dzyyds&extend=http://www.smarth.top:5707/files/hipy/cntv央视.json&filter=true
        public async Task<HomeModel> HomeAsync(string filter)
        {
            headers.Clear();
            headers["filter"] = "true";
            var req = await client.ExecuteAsync(CreatRequest());
            if (req.IsSuccessful)
            {
                return req.Content.ToObjectByJson<HomeModel>();
            }
            return null;
        }

        public async Task<SmallVodListModel> HomeVodAsync(string filter)
        {
            throw null;
        }

        //&extend=http://www.smarth.top:5707/files/hipy/cntv央视.json&filter=true&ac=videolist&t=4K专区&pg=1&ext=
        public async Task<SmallVodListModel> CategoryAsync(string tid, int pg, string filter, string extend)
        {
            headers.Clear();
            headers["filter"] = "true";
            headers["ac"] = "videolist";
            headers["t"] = tid;
            headers["pg"] = pg.ToString();
            headers["ext"] = extend;
            var req = await client.ExecuteAsync(CreatRequest());
            if (req.IsSuccessful)
            {
                return req.Content.ToObjectByJson<SmallVodListModel>();
            }
            return null;
        }
        //&ac=detail&ids=4K专区||《智造美好生活》 第6集 创 未来||https://tv.cctv.com/2023/05/20/VIDEVB9nRh6ubMu5j7z5bngU230520.shtml||
        //https://p3.img.cctvpic.com/fmspic/2023/05/20/657bc654c18e41088ec3bfda801d51db-2.jpg||VIDAwNF4b8AJ7tJjipAq6kSa230519||||||",
        //"vod_name": "《智造美好生活》 第6集 创 未来&extend=http://www.smarth.top:5707/files/hipy/cntv央视.json
        public async Task<VodListModel> DetailsAsync(string ids)
        {
            headers.Clear();
            headers["ids"] = ids;
            headers["ac"] = "detail";
            var req = await client.ExecuteAsync(CreatRequest());
            if (req.IsSuccessful)
            {
                return req.Content.ToObjectByJson<VodListModel>();
            }
            return null;
        }

        // ?wd=${wd}&extend=${ext}
        public async Task<SmallVodListModel> SearchAsync(string filter, bool quick = true)
        {
            headers.Clear();
            headers["wd"] = filter;
            var req = await client.ExecuteAsync(CreatRequest());
            if (req.IsSuccessful)
            {
                return req.Content.ToObjectByJson<SmallVodListModel>();
            }
            return null;
        }

        //&extend=http://www.smarth.top:5707/files/hipy/cntv央视.json&flag=CCTV&play=28f751281bbf46b78417e4d297ec3f2f
        public async Task<PlayModel> PlayAsync(string line, string id, string flags)
        {
            headers.Clear();
            headers["flag"] = line;
            headers["play"] = id;
            var req = await client.ExecuteAsync(CreatRequest());
            if (req.IsSuccessful)
            {
                return req.Content.ToObjectByJson<PlayModel>();
            }
            return null;
        }

    }



}
