using Peach.Application.Interfaces;
using Peach.Model;
using Peach.Model.Models;
using Peach.Spider.Drpy;

namespace Peach.Application.Services
{
    public class SnifferService : ISnifferService
    {
        private ISniffer sniffer;
        public SnifferService() { }

        public Task<bool> InitSnifferAsync(string baseUrl)
        {
            sniffer = new HipySnifferClient();
            return sniffer.InitSnifferAsync(baseUrl);
        }

        public Task<SnifferModel> SnifferAsync(string url)
        {
            return sniffer.SnifferAsync(url);
        }
    }
}
