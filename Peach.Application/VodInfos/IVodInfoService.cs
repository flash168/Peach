using Peach.Application.VodInfos.VodDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Peach.Application.VodInfos
{
    public interface IVodInfoService
    {
        /// <summary>
        /// 分类和首页推荐
        /// </summary>
        /// <returns></returns>
        Task<string> HomeAsync(string rule);


        /// <summary>
        /// 一级分类
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<string> ClassifyAsync(string rule, string tid, string pg, string filter, string extend);


        /// <summary>
        /// 二级详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<string> DetailsAsync(string rule, string ids);


        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<string> SearchAsync(string rule, string filter);


        /// <summary>
        /// 嗅探播放
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<string> SniffingAsync(string rule, string url);


    }
}
