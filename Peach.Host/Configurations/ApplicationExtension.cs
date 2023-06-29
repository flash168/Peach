using Microsoft.AspNetCore.Cors.Infrastructure;
using Peach.Application.VodInfos;

namespace Peach.Host.Configurations
{
    public static class VideoApplicationExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<IVodInfoService, VodInfoService>();

        }
    }
}
