using Peach.Model.Models;

namespace Peach.Application.Interfaces
{
    /// <summary>
    /// 嗅探服务接口
    /// </summary>
    public interface ISnifferService
    {
        /// <summary>
        /// 初始化嗅探
        /// </summary>
        /// <param name="baseUrl">嗅探地址</param>
        /// <returns></returns>
        Task<bool> InitSnifferAsync(string baseUrl);

        /// </summary>
        /// 嗅探播放
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SnifferModel> SnifferAsync(string url);


    }
}
