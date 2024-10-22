using Jint;
using Jint.Native;
using Jint.Native.Object;
using Peach.Infrastructure.Extends;
using Peach.Model;
using Peach.Model.Models;

namespace Peach.Drpy
{
    public class JsSpiderClient : ISpider
    {
        private Engine engine;
        ObjectInstance ns;
        public console console;
        HtmlParser hparser;
        RSAHandle rSAHandle;

        SiteModel Site;
        public JsSpiderClient()
        {
            console = new console();
            hparser = new HtmlParser();
            rSAHandle = new RSAHandle();
        }

        public Task<bool> InitSpiderAsync(SiteModel _site)
        {
            Site = _site;
            var api = _site.Api;
            var ext = _site.Ext?.ToString();
            if (string.IsNullOrEmpty(api))
                api = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "drpy_libs", "drpy2.min.js"));
            else if (api.ToLower().Contains("drpy2."))
                api = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "drpy_libs", "drpy2.min.js"));
            else if (api.ToLower().Contains("alist."))
                api = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "drpy_libs", "alist.min.js"));
            else if (api.ToLower().StartsWith("http"))
            {
                api = hparser.GetHtml(api);
            }
            else
                api = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "js", $"{api}.js"));

            if (!string.IsNullOrWhiteSpace(ext.Trim()) && ext.ToLower().StartsWith("http"))
                ext = hparser.GetHtml(ext);

            return Task.Factory.StartNew(() =>
            {
                try
                {
                    engine = new Engine(cfg =>
                    {
                        cfg.AllowClr();
                        cfg.EnableModules(new RequireModuleLoader("", AppContext.BaseDirectory));
                    });
                    //engine.SetValue("consolelog", new Action<object>(g => { Debug.WriteLine(g); }));

                    // 将 C# 函数转换为 JavaScript 函数，并将其添加到 engine 中
                    engine.SetValue("pdfh", new Func<string, string, string>(hparser.parseDomForUrl));
                    engine.SetValue("pd", new Func<string, string, string, string>(hparser.parseDom));
                    engine.SetValue("pdfl", new Func<string, string, string, string, string, string[]>(hparser.parseDomForList));
                    engine.SetValue("pdfa", new Func<string, string, string[]>(hparser.parseDomForArray));
                    engine.SetValue("joinUrl", new Func<string, string, string>(hparser.joinUrl));
                    engine.SetValue("req", new Func<string, JsValue, object>(hparser.request));

                    engine.SetValue("rsaX", new Func<string, bool, bool, string, bool, string, bool, string>(rSAHandle.RSAX));

                    engine.SetValue("local", new Local());
                    engine.SetValue("console", console);

                    engine.Modules.Add("drpyModel", api);
                    ns = engine.Modules.Import("drpyModel");

                    JSInvoke("init", ext);
                    //engine.Invoke(ns.Get("default").Get("init"), ext);
                }
                catch (Exception ex)
                {
                    return false;
                    throw;
                }
                return true;
            });
        }


        public string JSInvoke(string func, params object?[] objects)
        {
            try
            {
                return engine.Invoke(ns["default"].Get(func), objects).AsString();
            }
            catch (Exception ex)
            {

            }
            return null;
        }


        public Task<HomeModel> HomeAsync(string filter)
        {
            if (engine == null) return null;
            return Task.Factory.StartNew(_filier =>
            {
                var data = JSInvoke("home", _filier);// engine.Invoke(ns["default"].Get("home"), _filier).AsString();
                return data.ToObjectByJson<HomeModel>();
            }, filter);
        }
        public Task<SmallVodListModel> HomeVodAsync(string fidier)
        {
            if (engine == null) return null;
            return Task.Factory.StartNew(_fidier =>
            {
                var data = JSInvoke("homeVod", _fidier);//  engine.Invoke(ns["default"].Get("homeVod"), _fidier).AsString();
                return data.ToObjectByJson<SmallVodListModel>();
            }, fidier);
        }
        public Task<SmallVodListModel> CategoryAsync(string tid, int pg, string filter, string extend)
        {
            if (engine == null) return null;
            return Task.Factory.StartNew(() =>
            {
                var data = JSInvoke("category", tid, pg, filter, extend);//  engine.Invoke(ns["default"].Get("category"), tid, pg, filter, extend).AsString();
                return data.ToObjectByJson<SmallVodListModel>();
            });
        }

        public Task<VodListModel> DetailsAsync(string ids)
        {
            if (engine == null) return null;
            return Task.Factory.StartNew(_ids =>
            {
                var data = JSInvoke("detail", _ids);//engine.Invoke(ns["default"].Get("detail"), _ids).AsString();
                return data.ToObjectByJson<VodListModel>();
            }, ids);
        }

        public Task<SmallVodListModel> SearchAsync(string filter, bool quick = true)
        {
            if (engine == null) return null;
            return Task.Factory.StartNew(_filter =>
            {
                var data = JSInvoke("search", _filter, quick);// engine.Invoke(ns["default"].Get("search"), _filter, quick).AsString();
                return data.ToObjectByJson<SmallVodListModel>();
            }, filter);
        }

        public Task<PlayModel> PlayAsync(string line, string id, string flags)
        {
            if (engine == null) return null;
            return Task.Factory.StartNew(_id =>
            {
                //line线路名, id, array(vipFlags)全局配置需要解析的标识列表flags
                var data = JSInvoke("play", line, _id, flags);// engine.Invoke(ns["default"].Get("play"), line, _id, flags).AsString();
                return data.ToObjectByJson<PlayModel>();
            }, id);
        }
    }
}