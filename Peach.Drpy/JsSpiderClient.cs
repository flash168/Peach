using Jint;
using Jint.Native;
using Jint.Native.Object;
using System.Diagnostics;
using System.IO.Compression;
using static System.Net.Mime.MediaTypeNames;

namespace Peach.Drpy
{
    public class JsSpiderClient
    {
        public JsSpiderClient()
        { }

        private Engine engine;
        ObjectInstance ns;

        HtmlParser hparser = new HtmlParser();
        public bool InitEngine(string dph, string rule)
        {
            try
            {
                if (string.IsNullOrEmpty(dph))
                    dph = AppContext.BaseDirectory;

                string drpy = "";
                drpy = File.ReadAllText(Path.Combine(dph, "libs", "drpy2.min.js"));
                //drpy = drpy.Replace("console.log", "consolelog");

                engine = new Engine(cfg =>
                {
                    cfg.AllowClr();
                    cfg.EnableModules(new RequireModuleLoader("", dph));
                });
                //engine.SetValue("consolelog", new Action<object>(g => { Debug.WriteLine(g); }));

                // 将 C# 函数转换为 JavaScript 函数，并将其添加到 engine 中
                engine.SetValue("pdfh", new Func<string, string, string>(hparser.parseDomForUrl));
                engine.SetValue("pd", new Func<string, string, string, string>(hparser.parseDom));
                engine.SetValue("pdfl", new Func<string, string, string, string, string, string[]>(hparser.parseDomForList));
                engine.SetValue("pdfa", new Func<string, string, string[]>(hparser.parseDomForArray));
                engine.SetValue("joinUrl", new Func<string, string, string>(hparser.joinUrl));
                engine.SetValue("req", new Func<string, JsValue, object>(hparser.request));

                engine.SetValue("local", new Local());
                engine.SetValue("console", new console());

                engine.AddModule("drpyModel", drpy);
                ns = engine.ImportModule("drpyModel");

                var ext = File.ReadAllText(Path.Combine(dph, "js", $"{rule}.js"));

                engine.Invoke(ns.Get("default").Get("init"), ext);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public Task<string> GetHome(string fidier)
        {
            if (engine == null) return null;
            Task<string> task = Task.Factory.StartNew(_fidier =>
            {
                return engine.Invoke(ns["default"].Get("home"), _fidier).AsString();
            }, fidier);
            return task;
        }
        public Task<string> GetHomeVod(string fidier)
        {
            if (engine == null) return null;
            return Task.Factory.StartNew(_fidier =>
            {
                return engine.Invoke(ns["default"].Get("homeVod"), _fidier).AsString();
            }, fidier);
        }

        public Task<string> GetCategory(string tid, string pg, string filter, string extend)
        {
            if (engine == null) return null;
            return Task.Factory.StartNew(() =>
            {
                return engine.Invoke(ns["default"].Get("category"), tid, pg, filter, extend).AsString();
            });
        }

        public Task<string> Search(string fidier, bool quick = true)
        {
            if (engine == null) return null;
            return Task.Factory.StartNew(_filter =>
            {
                return engine.Invoke(ns["default"].Get("search"), _filter, quick).AsString();
            }, fidier);
        }

        //play search detail
        public Task<string> GetDetails(string ids)
        {
            if (engine == null) return null;
            return Task.Factory.StartNew(_ids =>
            {
                return engine.Invoke(ns["default"].Get("detail"), _ids).AsString();
            }, ids);
        }

        public Task<string> GetPlayInfo(string flag, string id, string flags)
        {
            if (engine == null) return null;
            return Task.Factory.StartNew(_id =>
            {
                //flag线路名, id, array(vipFlags)全局配置需要解析的标识列表flags
                return engine.Invoke(ns["default"].Get("play"), flag, _id, flags).AsString();
            }, id);
        }



    }
}