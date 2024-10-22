using Jint;
using Jint.Runtime.Modules;
using RestSharp;

namespace Peach.Drpy
{
    public class RequireModuleLoader : ModuleLoader
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

        public override ResolvedSpecifier Resolve(string referencingModuleLocation, ModuleRequest moduleRequest)
        {
            var moduleSpec = moduleRequest.Specifier;
            Uri uri = Resolve(referencingModuleLocation, moduleRequest.Specifier);
            Console.WriteLine($"Resolved Get '{moduleRequest.Specifier}' referenced by '{referencingModuleLocation}' to '{uri}'");
            return new ResolvedSpecifier(moduleRequest, moduleSpec, uri, SpecifierType.RelativeOrAbsolute);
        }
        protected override string LoadModuleContents(Engine engine, ResolvedSpecifier resolved)
        {
            string fileName = Path.GetFileName(Uri.UnescapeDataString(resolved.Uri.AbsolutePath));
            var path = Path.Combine(_basePath, "drpy_libs", fileName);
            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }
            // this test dont need to load content
            throw new InvalidOperationException();
        }
        private Uri Resolve(string? referencingModuleLocation, string specifier)
        {
            if (Uri.TryCreate(specifier, UriKind.Absolute, out Uri? absoluteLocation))
                return absoluteLocation;

            if (!string.IsNullOrEmpty(referencingModuleLocation) && Uri.TryCreate(referencingModuleLocation, UriKind.Absolute, out Uri? baseUri))
            {
                if (Uri.TryCreate(baseUri, specifier, out Uri? relative))
                    return relative;
            }

            return new Uri("file:///./" + specifier);
        }


        private string GetWebJs(string url)
        {
            var ret = new RestRequest(url);
            var req = client.Get(ret);
            if (req.IsSuccessful)
                return req.Content;
            return "";
        }


    }
}
