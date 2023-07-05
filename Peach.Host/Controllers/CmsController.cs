using Microsoft.AspNetCore.Mvc;
using Peach.Domain;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text.Json;
using System.Text;
using Microsoft.Extensions.Hosting.Internal;
using System.Diagnostics.CodeAnalysis;
using static NSoup.Select.Evaluator;
using System.Text.Json.Nodes;
using System.Runtime.Intrinsics.Arm;
using System;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;

namespace Peach.Host.Controllers
{
    /// <summary>
    /// 影视源管理（开发中···）
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class CmsController : ControllerBase
    {

        private readonly IWebHostEnvironment _hostingEnvironment;
        /// <summary>
        /// Cms
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        public CmsController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// js源管理，只支持drpy的js源
        /// </summary>
        /// <param name="file">js源</param>
        /// <returns></returns>
        [HttpPost("UploadFile")]
        public IActionResult UploadFile(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var filePath = Path.Combine(_hostingEnvironment.ContentRootPath, "js", file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }
            return Ok();
        }

        /// <summary>
        /// 生成配置文件
        /// </summary>
        /// <param name="fileName">配置文件名称</param>
        /// <param name="_type">解析类型（0：服务器解析，1：壳解析）</param>
        /// <returns></returns>
        [HttpPost("GenerateConfig")]
        public IActionResult GenerateConfig([NotNull] string fileName, type _type)
        {
            var directoryPath = Path.Combine(_hostingEnvironment.ContentRootPath, "js");
            var files = Directory.GetFiles(directoryPath, "*.js");
            string host = $"{Request.Scheme}://{Request.Host.Value}";

            List<object> collection = new List<object>();
            string site;

            //{
            //"key": "dr_007影视",
            // "name": "007影视(道长)",
            // "type": 1,
            // "api": "http://b.flash168.top/vod?rule=007影视",
            // "searchable": 2,
            // "quickSearch": 0,
            // "filterable": 1
            //},
            if (_type == type.js1)
                foreach (var file in files)
                {
                    site = Path.GetFileNameWithoutExtension(file);
                    collection.Add(new
                    {
                        key = site,
                        name = site,
                        type = 1,
                        searchable = 2,
                        quickSearch = 0,
                        filterable = 1,
                        api = $"{host}/libs/drpy2.min.js",
                        ext = $"{host}/js/{Path.GetFileName(file)}"
                    });
                }
            if (_type == type.js0)
                foreach (var file in files)
                {
                    site = Path.GetFileNameWithoutExtension(file);
                    collection.Add(new
                    {
                        key = site,
                        name = site,
                        type = 1,
                        searchable = 2,
                        quickSearch = 0,
                        filterable = 1,
                        api = $"{host}/vod?rule={site}",
                    });
                }

            var sites = JsonSerializer.Serialize(collection, new JsonSerializerOptions() { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping });
            JsonNode jsonNode = JsonNode.Parse(sites);

            var conf = System.IO.File.ReadAllText(Path.Combine(_hostingEnvironment.ContentRootPath, "config", "constant.conf"));
            var Jconf = JsonNode.Parse(conf);
            if (Jconf != null)
                Jconf["sites"] = jsonNode;
            Jconf["dr_count"] = collection.Count;

            var data = Jconf.ToJsonString();// new JsonSerializerOptions() { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping });

            string unescapedEmoji = Regex.Unescape(data);

            var directoryFilePath = Path.Combine(_hostingEnvironment.ContentRootPath, "config", fileName);
            System.IO.File.WriteAllText(directoryFilePath, unescapedEmoji);

            return Ok();
        }

        /// <summary>
        /// 源类型
        /// </summary>
        public enum type
        {
            /// <summary>
            /// 服务器解析
            /// </summary>
            js0,
            /// <summary>
            /// 壳解析
            /// </summary>
            js1
        }

    }
}
