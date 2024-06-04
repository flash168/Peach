
using Peach.Model.Models;

namespace Peach.Model
{
    /// <summary>
    /// 爬虫接口
    /// </summary>
    public interface ISpider
    {
        /// <summary>
        /// 初始化爬虫
        /// </summary>
        /// <param name="site">爬虫站点</param>
        /// <returns></returns>
        Task<bool> InitSpiderAsync(SiteModel site);

        /// <summary>
        /// 分类和首页推荐
        /// </summary>
        /// <param name="filter">过滤</param>
        Task<HomeModel> HomeAsync(string filter);

        /// <summary>
        /// 首页推荐
        /// </summary>
        /// <param name="filter">过滤</param>
        Task<SmallVodListModel> HomeVodAsync(string filter);

        /// <summary>
        /// 一级分类
        /// </summary>
        /// <param name="tid">分类id</param>
        /// <param name="pg">页码</param>
        /// <param name="filter">过滤</param>
        /// <param name="extend">扩展</param>
        Task<SmallVodListModel> CategoryAsync(string tid, int pg, string filter, string extend);

        /// <summary>
        /// 二级详情
        /// </summary>
        /// <param name="ids">ids</param>
        Task<VodListModel> DetailsAsync(string ids);

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="filter">过滤</param>
        /// <param name="quick">快搜</param>
        Task<SmallVodListModel> SearchAsync(string filter, bool quick = true);

        /// <summary>
        /// 嗅探播放
        /// </summary>
        /// <param name="line">线路</param>
        /// <param name="id">id</param>
        /// <param name="flags">标签</param>
        Task<SniffingModel> SniffingAsync(string line, string id, string flags);

    }
}
