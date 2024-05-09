
namespace Peach.Model
{
    public interface ISpider
    {
        //  ISpiderClient爬虫接口
        Task<bool> InitSpiderAsync(string api, string ext);

        /// <summary>
        /// 分类和首页推荐
        /// </summary>
        /// <param name="filter">过滤</param>
        Task<string> HomeAsync(string filter);

        /// <summary>
        /// 首页推荐
        /// </summary>
        /// <param name="filter">过滤</param>
        Task<string> HomeVodAsync(string filter);
        
        /// <summary>
        /// 一级分类
        /// </summary>
        /// <param name="tid">分类id</param>
        /// <param name="pg">页码</param>
        /// <param name="filter">过滤</param>
        /// <param name="extend">扩展</param>
        Task<string> CategoryAsync(string tid, string pg, string filter, string extend);

        /// <summary>
        /// 二级详情
        /// </summary>
        /// <param name="ids">ids</param>
        Task<string> DetailsAsync(string ids);

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="filter">过滤</param>
        /// <param name="quick">快搜</param>
        Task<string> SearchAsync(string filter, bool quick = true);

        /// <summary>
        /// 嗅探播放
        /// </summary>
        /// <param name="line">线路</param>
        /// <param name="id">id</param>
        /// <param name="flags">标签</param>
        Task<string> PlayAsync(string line, string id, string flags);



    }
}
