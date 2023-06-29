using Peach.Application.Base;

namespace Peach.Application.VodInfos.VodDtos
{
    public class VodInput : VodBaseDto
    {
        /// <summary>
        /// 分类
        /// </summary>
        public string? t { get; set; }
        public string? pg { get; set; }
        public string? ac { get; set; }
        public string? f { get; set; }
        public string? ids { get; set; }

        public string? wd { get; set; }
        public string? play_url { get; set; }

    }
}