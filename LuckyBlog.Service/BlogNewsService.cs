using LuckyBlog.IRepository;
using LuckyBlog.Model;
using LuckyBlog.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyBlog.Repository
{
    public class BlogNewsService: BaseService<BlogNews>, IBlogNewsService
    {
        private readonly IBlogNewsRepository _blogNewsRepository;
        public BlogNewsService(IBlogNewsRepository blogNewsRepository)
        {
            base._IBaseRepository = blogNewsRepository;
            _blogNewsRepository = blogNewsRepository;
        }
    }
}
