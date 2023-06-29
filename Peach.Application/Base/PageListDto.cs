using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peach.Application.Base
{
    public class PageListDto
    {
        public int limit { get; set; }

        public int page { get; set; }
        public int pagecount { get; set; }
        public int total { get; set; }

    }
}
