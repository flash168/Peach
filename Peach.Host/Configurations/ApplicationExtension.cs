using Peach.Application.Interfaces;
using Peach.Application.Services;

namespace Peach.Host.Configurations
{
    public static class VideoApplicationExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<ICmsService, CmsService>();
            //services.AddTransient<, VodInfoService>();

        }
    }
}
