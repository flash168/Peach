using Microsoft.AspNetCore.Mvc;
using Peach.Application.Configuration;
using Peach.Application.VodInfos;
using Peach.Application.VodInfos.VodDtos;
using Peach.Domain;
using Peach.Host.Filters;
using System.Text.Json.Nodes;

namespace Peach.Host.Controllers
{

    [Route("[controller]")]
    [ApiController]
    // [ServiceFilter(typeof(VodValidationFilter))] // 添加自定义的验证过滤器
    public class VodController : ControllerBase
    {
        private readonly IVodInfoService vodInfoService;
        private readonly string apiPwd;
        public VodController(IVodInfoService _vodInfoService)
        {
            apiPwd = AppSettingsHelper.GetContent<string>("AppConfig", "ApiPwd");

            vodInfoService = _vodInfoService;
        }

        /// <summary>
        /// 分类和首页推荐
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> HomeAsync(string pwd, string rule, string? t, string? pg, string? ac, string? f, string? ids, string? wd, string? play_url)
        {
            if (!pwd.ToLower().Equals(apiPwd.ToLower()))
                throw new BusinessException("接口密码错误！");
            if (!string.IsNullOrEmpty(t) && !string.IsNullOrEmpty(ac))//一级分类
                return await vodInfoService.ClassifyAsync(rule, t, pg, ac, f);
            else if (!string.IsNullOrEmpty(ids) && !string.IsNullOrEmpty(ac) && ac.ToLower().Equals("detail"))//二级详情
                return await vodInfoService.DetailsAsync(rule, ids);
            else if (!string.IsNullOrEmpty(wd))//搜索
                return await vodInfoService.SearchAsync(rule, wd);
            else if (!string.IsNullOrEmpty(play_url))//播放
                return await vodInfoService.SniffingAsync(rule, play_url);
            else
                return await vodInfoService.HomeAsync(rule);
        }


        ///// <summary>
        ///// 一级分类
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<string> ClassifyAsync(string pwd, string rule, string t, string pg, string ac, string f)
        //{
        //    if (!pwd.ToLower().Equals(apiPwd.ToLower()))
        //        throw new BusinessException("接口密码错误！");
        //    return await vodInfoService.ClassifyAsync(rule, t, pg, ac, f);
        //}

        ///// <summary>
        ///// 二级详情
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<string> DetailsAsync(string pwd, string rule, string ids, string ac)
        //{
        //    if (!pwd.ToLower().Equals(apiPwd.ToLower()))
        //        throw new BusinessException("接口密码错误！");
        //    return await vodInfoService.DetailsAsync(rule, ids);
        //}

        ///// <summary>
        ///// 搜索
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<string> SearchAsync(string pwd, string rule, string wd)
        //{
        //    if (!pwd.ToLower().Equals(apiPwd.ToLower()))
        //        throw new BusinessException("接口密码错误！");
        //    return await vodInfoService.SearchAsync(rule, wd);
        //}

        ///// <summary>
        ///// 嗅探播放
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<string> SniffingAsync(string pwd, string rule, string play_url)
        //{
        //    if (!pwd.ToLower().Equals(apiPwd.ToLower()))
        //        throw new BusinessException("接口密码错误！");
        //    return await vodInfoService.SniffingAsync(rule, play_url);
        //}


    }
}
