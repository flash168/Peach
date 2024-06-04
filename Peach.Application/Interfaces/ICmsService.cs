using Peach.Model.Models;

namespace Peach.Application.Interfaces
{
    public interface ICmsService
    {
        Task<bool> InitSite(SiteModel site);

        /// <summary>
        /// 分类
        /// </summary>
        /// <returns></returns>
        Task<HomeModel> HomeAsync(string filter = "");


        /// <summary>
        /// 首页推荐
        /// </summary>
        /// <returns></returns>
        Task<SmallVodListModel> HomeVodAsync(string filter);

        /// <summary>
        /// 一级分类
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SmallVodListModel> CategoryAsync(string tid, int pg, string filter, string extend);


        /// <summary>
        /// 二级详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<VodListModel> DetailsAsync(string ids);


        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SmallVodListModel> SearchAsync(string filter);


        /// <summary>
        /// 嗅探播放
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SniffingModel> SniffingAsync(string flag, string url);


    }
}
