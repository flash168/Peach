
using Peach.Model.Models;

namespace Peach.Model
{
    /// <summary>
    /// 嗅探接口
    /// </summary>
    public interface ISniffer
    {
        /// <summary>
        /// 初始化嗅探
        /// </summary>
        /// <param name="baseUrl">嗅探地址</param>
        /// <returns></returns>
        Task<bool> InitSnifferAsync(string baseUrl);

        /// <summary>
        /// 嗅探
        /// </summary>
        /// <param name="url">地址</param>
        Task<SnifferModel> SnifferAsync(string url);


    }
}
