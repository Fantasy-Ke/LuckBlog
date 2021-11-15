using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBBS.IRepository;
using MyBBS.Model;
using MyBBS.Repository;
using MyBBSWebApi.Utility.ApiResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBBSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeInfoController : ControllerBase
    {
        private readonly ITypeInfoService _typeInfoService;
        public TypeInfoController(ITypeInfoService typeInfoService)
        {
            this._typeInfoService = typeInfoService;
        }
        /// <summary>
        /// 查询所有类型
        /// </summary>
        /// <returns></returns>
        [HttpGet("Types")]
        public async Task<ApiResult> GetType()
        {
            var types = await _typeInfoService.QueryAsync();
            if (types.Count == 0) return ApiResultHelper.Error("没有文章类型");
            return ApiResultHelper.Success(types);

        }
        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<ApiResult> Create(string name)
        {
            #region 数据验证
            if (string.IsNullOrWhiteSpace(name)) return ApiResultHelper.Error("文章类型名不能为空");
            #endregion
            TypeInfo typeInfo = new TypeInfo
            {
                Name=name
            };
            bool b =await  _typeInfoService.CreateAsync(typeInfo);
            if (!b) return ApiResultHelper.Error("添加失败");
            return ApiResultHelper.Success(b);
        }

        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Delete")]
        public async Task<ApiResult> Delete(int id)
        {
            bool b = await _typeInfoService.DeleteAsync(id);
            if (!b) return ApiResultHelper.Error("删除失败");
            return ApiResultHelper.Success(b);
        }
        /// <summary>
        /// 修改操作
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPut("Edit")]
        public async Task<ApiResult> Edit(int id, string name)
        {
            var types = await _typeInfoService.FindAsync(id);
            if (types == null) return ApiResultHelper.Error($"没有查到有关id为{id}的文章类型");
            types.Name = name;
            bool b = await _typeInfoService.EditAsync(types);
            if (!b) return ApiResultHelper.Error("修改失败");
            return ApiResultHelper.Success(b);
        }
    }
}
