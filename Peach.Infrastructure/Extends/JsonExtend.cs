using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace Peach.Infrastructure.Extends
{
    public static class JsonExtend
    {

        /// <summary>
        /// Json字符串转换成对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">带转换字符串（必须是严格json格式）</param>
        /// <returns></returns>
        public static T ToObjectByJson<T>(this string json)
        {
            JsonSerializerSettings jsetting = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            jsetting.Converters.Add(new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
            try
            {
                if (typeof(T).Name is "String" || typeof(T).Name is "string")
                {
                    return (T)Convert.ChangeType(json, typeof(T));
                }
                else
                {
                    return JsonConvert.DeserializeObject<T>(json, jsetting);
                }
            }
            catch (Exception)
            {
                // LogService.Instance.Writer(e.Message);
                return default(T);
            }
        }

        /// <summary>
        /// 对象转成json字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            JsonSerializerSettings jsetting = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            jsetting.Converters.Add(new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
            try
            {
                return JsonConvert.SerializeObject(obj, Formatting.Indented, jsetting);
            }
            catch (Exception)
            {
                // LogService.Instance.Writer(e.Message);
                return "";
            }
        }

    }
}
