using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MyBBS.IRepository;
using MyBBS.Model;
using SqlSugar;
using SqlSugar.IOC;

namespace MyBBS.Repository
{
    public class BaseRepository<T> : SimpleClient<T>, IBaseRepository<T> where T : class, new()
    {
        public BaseRepository(ISqlSugarClient context=null):base(context)
        {
            base.Context = DbScoped.Sugar;
            //创建数据库
            //base.Context.DbMaintenance.CreateDatabase();
            //base.Context.CodeFirst.InitTables(
            //    typeof(BlogNews),
            //    typeof(WriterInfo),
            //    typeof(TypeInfo)
            //    );
        }
        public async Task<bool> CreateAsync(T entity)
        {
            return await base.InsertAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {

            return await base.DeleteByIdAsync(id);
        }

        public async Task<bool> EditAsync(T entity)
        {
            return await base.UpdateAsync(entity);
        }

        public virtual async Task<T> FindAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> func)
        {
            return await base.GetSingleAsync(func);
        }

        public virtual async Task<List<T>> QueryAsync()
        {
            return await base.GetListAsync();
        }

        public virtual async Task<List<T>> QueryAsync(Expression<Func<T, bool>> fuuc)
        {
            return await base.GetListAsync(fuuc);
        }

        public virtual async Task<List<T>> QueryAsync(int page, int size, RefAsync<int> total)
        {
            return await base.Context.Queryable<T>()
                .ToPageListAsync(page, size, total);
        }

        public virtual async Task<List<T>> QueryAsync(Expression<Func<T, bool>> fuuc, int page, int size, RefAsync<int> total)
        {
            return await base.Context.Queryable<T>().Where(fuuc)
                .ToPageListAsync(page, size, total);
        }
    }
}
