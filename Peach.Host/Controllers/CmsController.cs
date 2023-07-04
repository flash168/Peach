using Microsoft.AspNetCore.Mvc;
using Peach.Domain;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text.Json;
using System.Text;
using Microsoft.Extensions.Hosting.Internal;
using System.Diagnostics.CodeAnalysis;

namespace Peach.Host.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CmsController : ControllerBase
    {

        private readonly IWebHostEnvironment _hostingEnvironment;
        /// <summary>
        /// 
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
        [HttpPost]
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
        /// <returns></returns>
        [HttpPost]
        public IActionResult GenerateDirectoryFile([NotNull] string fileName)
        {
            var directoryPath = Path.Combine(_hostingEnvironment.ContentRootPath, "js");
            var files = Directory.GetFiles(directoryPath);

            var directoryFileContent = string.Join(Environment.NewLine, files);

            var directoryFilePath = Path.Combine(_hostingEnvironment.ContentRootPath, "config", fileName);
            System.IO.File.WriteAllText(directoryFilePath, directoryFileContent);

            return Ok();
        }


        //[HttpGet]
        //public async Task<string> GetAsync([FromQuery] GetListInput input)
        //{
        //    return await _userInfoService.GetListAsync(input);
        //}




    }
}
