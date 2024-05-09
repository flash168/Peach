using System;

namespace Peach.Model.Models
{
    public class LiveModel
    {
        public string group { get; set; }
        public List<ChannelModel> channels { get; set; }
    }


    public class ChannelModel
    {
        public string name { get; set; }
        public List<string> urls { get; set; }
    }


    public class ChannelInfoModel
    {
    }



}
