using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peach.Application.VodInfos.VodDtos
{
    public class VodDto
    {
        public string? TypeId { get; set; }
        public string? typeName { get; set; }//": "剧情片美国2012",


        public string vod_id { get; set; }
        public string vod_name { get; set; }
        public string? vod_content { get; set; }
        public string? vod_pic { get; set; }
        public string? vod_remarks { get; set; }//

        
        public string? vod_actor { get; set; }//": "贝拉·索恩 斯戴芬妮·斯考特 尼克·罗宾森 Mar",
        public string? vod_area { get; set; }//": "",
        public string? vod_director { get; set; }//": "戴斯·冯·施勒·梅耶",
        public string? vod_play_from { get; set; }//": "非凡",
        public string? vod_play_url { get; set; }//": "HD中字$http://www.8kvod.com/p/121513-1-1/",
        public string? vod_year { get; set; }//": ""

    }
}
