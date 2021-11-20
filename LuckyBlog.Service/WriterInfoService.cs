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
