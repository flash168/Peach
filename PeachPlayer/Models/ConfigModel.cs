using System.Collections.Generic;

namespace PeachPlayer.Models
{
    public class SitesItem
    {
        /// <summary>
        /// dr_豆瓣
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 🐼豆瓣热播┃dr首页
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Api { get; set; }
        public string ApiData { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Searchable { get; set; }

        public int PlayerType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int QuickSearch { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Filterable { get; set; }
        // [JsonConverter(typeof(ObjectConverter))]
        public object Ext { get; set; }
        public string Jar { get; set; }

    }

    public class ChannelsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> urls { get; set; }
    }

    public class LivesItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string @group { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ChannelsItem> channels { get; set; }
    }

    public class ParsesItem
    {
        /// <summary>
        /// Json并发
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string url { get; set; }
        public int showType { get; set; }
        public object ext { get; set; }
        // public string header { get; set; }

    }

    public class OptionsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int category { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string value { get; set; }
    }

    public class IjkItem
    {
        /// <summary>
        /// 软解码
        /// </summary>
        public string @group { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<OptionsItem> options { get; set; }
    }
    public class DrivesItem
    {
        public string name { get; set; }
        public string server { get; set; }
        public string type { get; set; }
    }


    public class Root
    {
        /// <summary>
        /// 
        /// </summary>
        public List<SitesItem> Sites { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<LivesItem> Lives { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ParsesItem> Parses { get; set; }

        public List<DrivesItem> Drives { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<IjkItem> Ijk { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> Ads { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Wallpaper { get; set; }
        /// <summary>
        /// http://我不是.肥猫.love:63/Jar/panda1207.jar;md5;A94278B58C65214762595EDD514FDD77
        /// </summary>
        public string homepage { get; set; }
        public string Spider { get; set; }
        public int Dr_count { get; set; }
        public int Mode { get; set; }

    }

}
