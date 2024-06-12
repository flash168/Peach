using System;

namespace Peach.Model.Models
{
    public class SnifferModel
    {
        public string url { get; set; }
        public string from { get; set; }
        public string cost { get; set; }
        public int code { get; set; }
        public string script { get; set; }
        public string init_script { get; set; }
        public object headers { get; set; }

        public string msg { get; set; }

    }
}
