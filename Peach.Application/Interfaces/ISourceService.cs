
using Peach.Model.Models;

namespace Peach.Application.Interfaces
{
    public interface ISourceService
    {
        SourceModel Source { get; set; }
        string Url { get; set; }
        string Name { get; set; }


        /// <summary>
        /// 加载配置文件
        /// </summary>
        /// <param name="url">配置地址</param>
        /// <param name="name">备注名称</param>
        /// <returns></returns>
        Task<bool> LoadConfig(string url, string name = "");


    }
}
