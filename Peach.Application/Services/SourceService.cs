using Newtonsoft.Json;
using Peach.Application.Interfaces;
using Peach.Model.Models;


namespace Peach.Application.Services
{
    public class SourceService : ISourceService
    {
        private readonly HttpClient httpClient;
        public SourceService()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/57.0.2987.133 Safari/537.36");

           // httpClient.setHeader("User-Agent", "Mozilla/5.0(Windows NT 6.1;Win64; x64; rv:50.0) Gecko/20100101 Firefox/50.0");
        }
        //源
        public SourceModel Source { get; set; }

        public string Url { get; set; }
        public string Name { get; set; }

        //url加载源
        public async Task<bool> LoadConfig(string url, string name = "")
        {
            if (string.IsNullOrEmpty(url)) return false;
            Url = url;
            Name = name;
            var req = await httpClient.GetAsync(url);
            if (req.IsSuccessStatusCode)
            {
                Source = JsonConvert.DeserializeObject<SourceModel>(await req.Content.ReadAsStringAsync());
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<string> GetHtml(string url)
        {
            if (string.IsNullOrEmpty(url)) return "";
            Url = url;
            var req = await httpClient.GetAsync(url);
            if (req.IsSuccessStatusCode)
            {
                return await req.Content.ReadAsStringAsync();
            }
            else
            {
                return "";
            }
        }


    }
}
