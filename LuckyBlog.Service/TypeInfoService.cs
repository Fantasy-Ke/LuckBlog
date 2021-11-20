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
    public class TypeInfoService : BaseService<TypeInfo>, ITypeInfoService
    {
        private readonly ITypeInfoRepository _TypeInfoRepository;
        public TypeInfoService(ITypeInfoRepository typeInfoRepository)
        {
            base._IBaseRepository = typeInfoRepository;
            _TypeInfoRepository = typeInfoRepository;
        }
    }
}
