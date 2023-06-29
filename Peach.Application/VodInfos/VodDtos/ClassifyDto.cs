using Peach.Application.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peach.Application.VodInfos.VodDtos
{
    public class ClassifyDto:PageListDto
    {
        public List<SmallVodDto> list { get; set; }
    }
}
