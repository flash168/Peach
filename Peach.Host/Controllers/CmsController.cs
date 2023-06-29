using Microsoft.AspNetCore.Mvc;
using Peach.Domain;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text.Json;
using System.Text;

namespace Peach.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CmsController : ControllerBase
    {

        public CmsController()
        {
        }

        //[HttpGet]
        //public async Task<string> GetAsync([FromQuery] GetListInput input)
        //{
        //    return await _userInfoService.GetListAsync(input);
        //}




    }
}
