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

    }
}
