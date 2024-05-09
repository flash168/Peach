using System;

namespace Peach.Model.Models
{
    /// <summary>
    /// 数据源
    /// </summary>
    public class SourceModel
    {
        public string Spider { get; set; }
        public string Wallpaper { get; set; }
        public List<LiveModel> Lives { get; set; }
        public List<SiteModel> Sites { get; set; }
        public string Parses { get; set; }
        public List<string> Flags { get; set; }
        public List<string> Ads { get; set; }

    }
}
