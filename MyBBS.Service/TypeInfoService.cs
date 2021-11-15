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
