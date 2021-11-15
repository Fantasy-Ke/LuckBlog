using MyBBS.IRepository;
using MyBBS.Model;
using MyBBS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBBS.Repository
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
