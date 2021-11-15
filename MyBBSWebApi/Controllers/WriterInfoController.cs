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
    [Route("api/[controller]")]
    [ApiController]
    public class WriterInfoController : ControllerBase
    {
        private readonly IWriterInfoService _writerInfoService;
        public WriterInfoController(IWriterInfoService writerInfoService)
        {
            this._writerInfoService = writerInfoService;
        }
        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="name"></param>
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

        [HttpPut("Edit")]
        public async Task<ApiResult> Edit(string name)
        {
            int id = Convert.ToInt32(this.User.FindFirst("Id").Value);
            return ApiResultHelper.Error("修改失败");
        }
    }
}
