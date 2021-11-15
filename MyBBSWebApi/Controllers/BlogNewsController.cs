using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBBS.IRepository;
using MyBBS.Model;
using MyBBSWebApi.Utility.ApiResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyBBSWebApi.Controllers
{
    /// <summary>
    /// 新闻控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BlogNewsController : ControllerBase
    {
        // GET: api/<BlogNewsController>
        private readonly IBlogNewsService _blogNewsService;
        /// <summary>
        /// 新闻构造函数
        /// </summary>
        /// <param name="blogNewsService"></param>
        public BlogNewsController(IBlogNewsService blogNewsService)
        {
            this._blogNewsService = blogNewsService;
        }
        /// <summary>
        /// 查询操作
        /// </summary>
        /// <returns></returns>
        [HttpGet("BlogNews")]
        public async Task<ActionResult<ApiResult>> GetBlogNews()
        {
           var data= await _blogNewsService.QueryAsync();
            if (data == null) return ApiResultHelper.Error("查询出错");
            return ApiResultHelper.Success(data);
        }
        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="typeid"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public  async Task<ActionResult<ApiResult>> Create(string title,string content,int typeid)
        {
            BlogNews blogNews = new BlogNews()
            {
                BrowseCount=0,
                LikeCount=0,
                Content=content,
                Time=DateTime.Now,
                Title=title,
                TypeId=typeid,
                WriterId=1
            };
            bool b = await _blogNewsService.CreateAsync(blogNews);
            if (!b) return ApiResultHelper.Error("添加错误");
            return ApiResultHelper.Success(blogNews);
        }
        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Delete")]
        public async Task<ActionResult<ApiResult>> Delete(int id)
        {
            bool b = await _blogNewsService.DeleteAsync(id);
            if (!b) return ApiResultHelper.Error("删除失败");
            return ApiResultHelper.Success(b);
        }
        /// <summary>
        /// 修改操作
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="typid"></param>
        /// <returns></returns>
        [HttpPut("Edit")]
        public async Task<ActionResult<ApiResult>> Edit(int id,string title,string content,int typid)
        {
            var blogNews = await _blogNewsService.FindAsync(id);
            if (blogNews == null) return ApiResultHelper.Error($"没有查到有关id为{id}的数据");
            blogNews.Content = content;
            blogNews.Title = title;
            blogNews.TypeId = typid;
            bool b = await _blogNewsService.EditAsync(blogNews);
            if (!b) return ApiResultHelper.Error("修改失败");
            return ApiResultHelper.Success(b);
        }
    }
}
