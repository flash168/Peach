using Peach.Application.Interfaces;
using Peach.Domain;
using Peach.Drpy;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;



namespace Peach.Application.Services
{
    public class VodInfoService : IVodInfoService
    {
        private readonly string path;
        public VodInfoService()
        {
            path = "";
            Sites = new();
        }
        // 配置 JsonSerializerOptions
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        //js引擎实例集合
        private Dictionary<string, JsSpiderClient> Sites;

        //获取引擎（有了拿出来，没有则初始化）
        private async Task<JsSpiderClient> GetSite(string rule)
        {
            if (Sites?.Count <= 0 || !Sites.ContainsKey(rule))
            {
                var jse = new JsSpiderClient();
                var isok =await jse.InitSpiderAsync(rule, path);
                if (!isok)
                    throw new BusinessException($"初始化[{rule}]-DRPY异常。");
                Sites.Add(rule, jse);
                return jse;
            }
            else
                return Sites[rule];
        }


        /// <summary>
        /// 分类和首页推荐
        /// </summary>
        /// <returns></returns>
        public async Task<string> HomeAsync(string rule)
        {
            var sp =await GetSite(rule);
            try
            {
                var clas = await sp.HomeAsync("");
                var hv = await sp.HomeVodAsync("");

                var Jcls = JsonNode.Parse(clas);
                var Jhvv = JsonNode.Parse(hv);
                if (Jhvv.ToJsonString(options) != "{}")
                {
                    var Jv = Jhvv["list"].ToString();
                    Jcls["list"] = JsonNode.Parse(Jv);
                }
                else
                {
                    Jcls["list"] = "[]";
                }

                return Jcls.ToJsonString(options);
            }
            catch (Exception e)
            {
                throw new BusinessException(e.Message);
            }
        }


        /// <summary>
        /// 一级分类
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<string> ClassifyAsync(string rule, string tid, string pg, string filter, string extend)
        {
            var sp =await GetSite(rule);
            try
            {
                var clas = await sp.CategoryAsync(tid, pg, filter, extend);
                return clas;//JsonNode.Parse()?.ToJsonString(); //JsonSerializer.Deserialize<ClassifyDto>(clas);
            }
            catch (Exception e)
            {
                throw new BusinessException(e.Message);
            }
        }


        /// <summary>
        /// 二级详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<string> DetailsAsync(string rule, string ids)
        {
            var sp =await GetSite(rule);
            try
            {
                var clas = await sp.DetailsAsync(ids);
                return clas;
                //return JsonSerializer.Deserialize<DetailsDto>(clas);
            }
            catch (Exception e)
            {
                throw new BusinessException(e.Message);
            }
        }


        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<string> SearchAsync(string rule, string filter)
        {
            var sp =await GetSite(rule);
            try
            {
                var clas = await sp.SearchAsync(filter);
                return clas;
                // return JsonSerializer.Deserialize<ClassifyDto>(clas);
            }
            catch (Exception e)
            {
                throw new BusinessException(e.Message);
            }
        }


        /// <summary>
        /// 嗅探播放
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<string> SniffingAsync(string rule, string purl)
        {
            var sp =await GetSite(rule); 
            try
            {
                var clas = await sp.PlayAsync(rule, purl, "");
                return clas;
                // return JsonSerializer.Deserialize<ClassifyDto>(clas);
            }
            catch (Exception e)
            {
                throw new BusinessException(e.Message);
            }
        }


    }
}
