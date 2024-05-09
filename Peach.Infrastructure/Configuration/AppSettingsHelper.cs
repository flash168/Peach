using Microsoft.Extensions.Configuration;

namespace Peach.Infrastructure.Configuration
{
    public class AppSettingsHelper
    {
        private static IConfiguration Configuration;
        private static string path;
        public AppSettingsHelper(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static T GetContent<T>(string key)
        {
            try
            {
                var val = Configuration[key];
                if (val == null)
                    throw new Exception($"在{path}配置文件中没找到{key}的配置!");

                return (T)Convert.ChangeType(val, typeof(T));
            }
            catch (Exception ex)
            {
                throw new Exception($"没有在配置文件中的{path}中找到{key}的配置，请检查配置文件!");
            }
        }
        /// <summary>
        /// 封装要操作的字符
        /// AppSettingsHelper.GetContent(new string[] { "ConnectionStrings", "SqlConnection" });
        /// </summary>
        /// <param name="sections">节点配置</param>
        /// <returns></returns>
        public static T GetContent<T>(params string[] sections)
        {
            try
            {
                if (!sections.Any())
                {
                    throw new Exception($"在{path}配置文件中没找到{string.Join(":", sections)}的配置!");
                }
                var val = Configuration[string.Join(":", sections)];
                if (val == null)
                    throw new Exception($"在{path}配置文件中没找到{string.Join(":", sections)}的配置!");

                return (T)Convert.ChangeType(val, typeof(T));
            }
            catch (Exception)
            {
                throw new Exception($"没有在配置文件中的{path}中找到{string.Join(":", sections)}的配置，请检查配置文件!");
            }

            return default;
        }
    }
}
