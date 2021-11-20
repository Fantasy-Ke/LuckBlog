using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LuckyBlog.API.Controllers
{
    /// <summary>
    /// 测试
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        /// <summary>
        /// 不是验证的
        /// </summary>
        /// <returns></returns>
        [HttpGet("NoAuthorize")]
        public string NoAuthorize()
        {
            return "this is NoAuthorize";
        }
        /// <summary>
        /// 鉴权
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("Authorize")]
        public string Authorize()
        {
            return "this is Authorize";
        }
    }
}
