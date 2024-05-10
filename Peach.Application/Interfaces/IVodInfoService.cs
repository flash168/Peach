﻿using Peach.Model.Models;

namespace Peach.Application.Interfaces
{
    public interface IVodInfoService
    {
        Task<bool> InitSite(SiteModel site);

        /// <summary>
        /// 分类
        /// </summary>
        /// <returns></returns>
        Task<HomeModel> HomeAsync(string rule);


        /// <summary>
        /// 首页推荐
        /// </summary>
        /// <returns></returns>
        Task<VodListModel> HomeVodAsync(string rule);

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
