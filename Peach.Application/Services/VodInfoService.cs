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
        public async Task<SmallVodListModel> HomeVodAsync(string filter)
        {
            try
            {
                var clas = await jsSpider.HomeVodAsync(filter);
                return clas.ToObjectByJson<SmallVodListModel>();
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
        public async Task<SmallVodListModel> ClassifyAsync(string tid, int pg, string filter, string extend)
        {
            try
            {
                var cate = await jsSpider.CategoryAsync(tid, pg.ToString(), filter, extend);
                return cate.ToObjectByJson<SmallVodListModel>();
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
        public async Task<VodListModel> DetailsAsync(string ids)
        {

            try
            {
                var data = await jsSpider.DetailsAsync(ids);
                return data.ToObjectByJson<VodListModel>();
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
        public async Task<SmallVodListModel> SearchAsync(string filter)
        {

            try
            {
                var data = await jsSpider.HomeVodAsync(filter);
                return data.ToObjectByJson<SmallVodListModel>();
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
