﻿using LuckyBlog.IRepository;
using LuckyBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LuckyBlog.Repository
{
    public class BlogNewsRepository:BaseRepository<BlogNews>,IBlogNewsRepository
    {
        public  async override Task<List<BlogNews>> QueryAsync()
        {
            return await base.Context.Queryable<BlogNews>()
                .Mapper(c=>c.TypeInfo,c=>c.TypeId,c=>c.TypeInfo.Id)
                .Mapper(c=>c.WriterInfo,c=>c.WriterId,c=>c.WriterInfo.Id)
                .ToListAsync();
        }
        public async override Task<List<BlogNews>> QueryAsync(Expression<Func<BlogNews,bool>> func)
        {
            return await base.Context.Queryable<BlogNews>()
                .Where(func)
                .Mapper(c => c.TypeInfo, c => c.TypeId, c => c.TypeInfo.Id)
                .Mapper(c => c.WriterInfo, c => c.WriterId, c => c.WriterInfo.Id)
                .ToListAsync();
        }
    }
}
