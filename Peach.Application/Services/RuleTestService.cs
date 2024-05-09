using Jint.Native;
using Peach.Application.Interfaces;
using Peach.Domain;
using Peach.Drpy;
using Peach.Infrastructure.Extends;
using Peach.Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;



namespace Peach.Application.Services
{
    public class RuleTestService : IRuleTestService
    {
        public RuleTestService()
        {
        }
        // 配置 JsonSerializerOptions
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        JsSpiderClient jsClient;
        public event Action<string> WriterLog;

        //获取引擎（有了拿出来，没有则初始化）
        public Task<bool> InitSite(string rule)
        {
            if (jsClient != null)
                jsClient.console.WriterLog -= WriterLog;

            jsClient = new JsSpiderClient();
            jsClient.console.WriterLog += WriterLog;
            return jsClient.InitSpiderAsync(string.Empty, rule);
        }

        /// <summary>
        /// 分类和首页推荐
        /// </summary>
        /// <returns></returns>
        public async Task<string> HomeAsync(string rule)
        {
            if (jsClient == null)
                InitSite(rule);
            try
            {
                var data = await jsClient.HomeAsync("");
                //var da = data.ToObjectByJson<HomeModel>();

                var hv = await jsClient.HomeVodAsync("");

                var Jcls = JsonNode.Parse(data);
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
            try
            {
                var clas = await jsClient.CategoryAsync(tid, "1", filter, extend);
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
            try
            {
                var clas = await jsClient.DetailsAsync(ids);
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
            try
            {
                var clas = await jsClient.SearchAsync(filter);
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
        public async Task<string> SniffingAsync(string line, string purl)
        {
            try
            {
                var clas = await jsClient.PlayAsync(line, purl, "");
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
