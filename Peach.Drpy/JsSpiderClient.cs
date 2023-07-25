using Jint;
using Jint.Native;
using Jint.Native.Object;
using System.Diagnostics;
using System.IO.Compression;
using System.Runtime.Intrinsics.Arm;
using static System.Net.Mime.MediaTypeNames;

namespace Peach.Drpy
{
    public class JsSpiderClient
    {
        public JsSpiderClient()
        {
            console = new console();
            hparser = new HtmlParser();
        }

        private Engine engine;
        ObjectInstance ns;
        public console console;
        HtmlParser hparser;

        public bool InitEngine(string dph, string rule)
        {
            try
            {
                if (string.IsNullOrEmpty(dph))
                    dph = AppContext.BaseDirectory;

                //drpy = drpy.Replace("console.log", "consolelog");

                var ext = File.ReadAllText(Path.Combine(dph, "js", $"{rule}.js"));

                return InitEngine(string.Empty, dph, ext);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool InitEngine(string drpy, string path, string ext)
        {
            if (string.IsNullOrEmpty(path))
                path = AppContext.BaseDirectory;

            if (string.IsNullOrEmpty(drpy))
                drpy = File.ReadAllText(Path.Combine(path, "libs", "drpy2.min.js"));
            try
            {
                engine = new Engine(cfg =>
                {
                    cfg.AllowClr();
                    cfg.EnableModules(new RequireModuleLoader("", path));
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
                engine.SetValue("console", console);

                engine.AddModule("drpyModel", drpy);
                ns = engine.ImportModule("drpyModel");

                engine.Invoke(ns.Get("default").Get("init"), ext);
            }
            catch (Exception ex)
            {
                return false;
                throw;
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