using Peach.Application.Interfaces;
using Peach.Domain;
using Peach.Drpy;
using Peach.Infrastructure.Extends;
using Peach.Model;
using Peach.Model.Models;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;



namespace Peach.Application.Services
{
    public class VodInfoService : IVodInfoService
    {
        public VodInfoService()
        {
        }
        // 配置 JsonSerializerOptions
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        //js引擎实例集合
        public SiteModel Site;
        private JsSpiderClient jsSpider;
        //获取引擎（有了拿出来，没有则初始化）
        public Task<bool> InitSite(SiteModel site)
        {
            Site = site;
            jsSpider = new JsSpiderClient();
            return jsSpider.InitSpiderAsync(site.Api, site.Ext.ToString());
        }

        /// <summary>
        /// 分类
        /// </summary>
        public async Task<HomeModel> HomeAsync(string filter)
        {
            try
            {
                var clas = await jsSpider.HomeAsync(filter);
                return clas.ToObjectByJson<HomeModel>();
            }
            catch (Exception e)
            {
                throw new BusinessException(e.Message);
            }
        }

        /// <summary>
        /// 首页推荐
        /// </summary>
        public async Task<VodListModel> HomeVodAsync(string filter)
        {
            try
            {
                var clas = await jsSpider.HomeVodAsync(filter);
                return clas.ToObjectByJson<VodListModel>();
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
        public async Task<string> ClassifyAsync(string tid, string pg, string filter, string extend)
        {
            try
            {
                return await jsSpider.HomeVodAsync(filter);
                //  return clas.ToObjectByJson<VodListModel>();
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
        public async Task<string> DetailsAsync(string ids)
        {

            try
            {
                return await jsSpider.HomeVodAsync(ids);
                //  return clas.ToObjectByJson<VodListModel>();
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
        public async Task<string> SearchAsync(string filter)
        {

            try
            {
                return await jsSpider.HomeVodAsync(filter);
                //  return clas.ToObjectByJson<VodListModel>();
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
        public async Task<string> SniffingAsync(string purl)
        {

            try
            {
                return await jsSpider.HomeVodAsync(purl);
                //  return clas.ToObjectByJson<VodListModel>();
            }
            catch (Exception e)
            {
                throw new BusinessException(e.Message);
            }
        }


    }
}
