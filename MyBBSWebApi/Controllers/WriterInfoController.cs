using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBBS.IRepository;
using MyBBS.Model;
using MyBBSWebApi.Utility._MD5;
using MyBBSWebApi.Utility.ApiResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBBSWebApi.Controllers
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WriterInfoController : ControllerBase
    {
        private readonly IWriterInfoService _writerInfoService;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="writerInfoService"></param>
        public WriterInfoController(IWriterInfoService writerInfoService)
        {
            this._writerInfoService = writerInfoService;
        }
        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="name"></param>
        /// <param name="username"></param>
        /// <param name="userpwd"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<ApiResult> Create(string name,string username,string userpwd)
        {
            WriterInfo writer = new WriterInfo
            {
                Name = name,
                UserName = username,
                UserPwd = MD5Helper.GenerateMD5(userpwd)
            };
            var oldWriter = await _writerInfoService.FindAsync(c => c.UserName == writer.UserName);
            if (oldWriter != null) return ApiResultHelper.Error("账号已存在");
            bool b = await _writerInfoService.CreateAsync(writer);
            if (!b) return ApiResultHelper.Error("添加失败");
            return ApiResultHelper.Success(writer);
        }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPut("Edit")]
        public async Task<ApiResult> Edit(string name)
        {
            int id = Convert.ToInt32(this.User.FindFirst("Id").Value);
            return ApiResultHelper.Error("修改失败");
        }
    }
}
