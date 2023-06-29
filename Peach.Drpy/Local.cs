using Jint.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peach.Drpy
{
    public class Local
    {
        private Dictionary<string, string> maps = new Dictionary<string, string>();

        public JsValue get(String R_KEY, String k)
        {
            return maps.GetValueOrDefault("js_engine_" + R_KEY + "_" + k);
        }

        public void set(String R_KEY, String k, String v)
        {
            maps["\"js_engine_\" + R_KEY + \"_\" + k"] = v;
        }

        public void delete(String R_KEY, String k)
        {
            maps.Remove("js_engine_" + R_KEY + "_" + k);
        }
    }
}
