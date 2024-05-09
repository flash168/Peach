using Esprima;
using Esprima.Ast;
using Jint;
using Jint.Runtime.Modules;
using RestSharp;

namespace Peach.Drpy
{
    public class RequireModuleLoader : IModuleLoader
    {
        private readonly RestClient client;
        private readonly string _basePath;
        private readonly string _baseUrl;
        public RequireModuleLoader(string baseurl, string basePath)
        {
            client = new RestClient();
            _basePath = basePath;
            _baseUrl = baseurl;
        }

        private readonly string[] _basePaths = { "assets://js/lib/", "../libs/js/", "../js/", "./" };
        public Module LoadModule(Engine engine, ResolvedSpecifier resolved)
        {
            Module module;
            var url = resolved.Key;
            try
            {
                string code = "";
                string fileName;
                if (_basePaths.Any(s => url.IndexOf(s) >= 0))
                {
                    var tp = _basePaths.First(s => url.IndexOf(s) >= 0);

                    fileName = Path.Combine(_basePath, url.Replace(tp, "drpy_libs/")).Replace('\\', '/');
                }
                else
                    fileName = Path.Combine(_basePath, url).Replace('\\', '/');

                if (File.Exists(fileName))
                    code = File.ReadAllText(fileName);

                var parserOptions = new ParserOptions
                {
                    AdaptRegexp = true,
                    Tolerant = true
                };
                module = new JavaScriptParser(parserOptions).ParseModule(code, source: resolved.Uri?.LocalPath!);

                //string code = "";
                //if (url.ToLower().StartsWith("http"))
                //    code = GetWebJs(url);
                //else
                //{
                //    string fileName;
                //    if (_basePaths.Any(s => url.IndexOf(s) >= 0))
                //    {
                //        var tp = _basePaths.First(s => url.IndexOf(s) >= 0);

                //        fileName = Path.Combine(_basePath, url.Replace(tp, "drpy_libs/")).Replace('\\', '/');
                //    }
                //    else
                //        fileName = Path.Combine(_basePath, url).Replace('\\', '/');

                //    if (File.Exists(fileName))
                //        code = File.ReadAllText(fileName);
                //    else
                //        code = GetWebJs(new Uri(new Uri(_baseUrl), url).ToString());
                //}
            }
            catch (ParserException ex)
            {
                module = null;
            }
            catch (Exception ex)
            {
                var message = $"Could not load module {resolved.Uri?.LocalPath}: {ex.Message}";
                module = null;
            }

            return module;
        }

        private string GetWebJs(string url)
        {
            var ret = new RestRequest(url);
            var req = client.Get(ret);
            if (req.IsSuccessful)
                return req.Content;
            return "";
        }



        public ResolvedSpecifier Resolve(string? referencingModuleLocation, string specifier)
        {
            return new ResolvedSpecifier(referencingModuleLocation ?? "", specifier ?? "", null, SpecifierType.RelativeOrAbsolute);
        }
    }
}
