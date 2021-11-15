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
    public class WriterInfoService: BaseService<WriterInfo>, IWriterInfoService
    {
        private readonly IWriterInfoRepository _WriterInfoRepository;
        public WriterInfoService(IWriterInfoRepository writerInfoRepository)
        {
            base._IBaseRepository = writerInfoRepository;
            _WriterInfoRepository = writerInfoRepository;
        }
    }
}
