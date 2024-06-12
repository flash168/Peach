using Peach.Infrastructure.Extends;
using Peach.Model;
using Peach.Model.Models;
using RestSharp;
using System.Diagnostics;


namespace Peach.Spider.Drpy
{
    public class HipySnifferClient : ISniffer
    {
        //去嗅探 http://127.0.0.1:5708/sniffer?url=
        public HipySnifferClient()
        {
            headers = new Dictionary<string, string>();
        }
        RestClient client;
        private Dictionary<string, string> headers = new Dictionary<string, string>();
        // private readonly string snifferPath = "sniffer/main.exe";
        // private readonly string baseUrl = "http://127.0.0.1:5708/";

        private RestRequest CreatRequest(string url = "")
        {
            RestRequest req = new RestRequest(url);
            req.Method = Method.Get;
            foreach (var item in headers)
            {
                req.AddQueryParameter(item.Key, item.Value);
            }
            return req;
        }


        /// <summary>
        /// 初始化嗅探器
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <returns></returns>
        public Task<bool> InitSnifferAsync(string baseUrl)
        {
            client = new RestClient(baseUrl);
            return Task.FromResult(true);
        }

        /// <summary>
        /// 嗅探
        /// </summary>
        /// <param name="url">嗅探地址</param>
        /// <returns></returns>
        public async Task<SnifferModel> SnifferAsync(string url)
        {
            headers.Clear();
            headers["url"] = url;
            var req = await client.ExecuteAsync(CreatRequest("sniffer"));
            if (req.IsSuccessful)
            {
                return req.Content.ToObjectByJson<SnifferModel>();
            }
            return null;
        }

    }



}
