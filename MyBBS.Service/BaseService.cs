using MyBBS.IRepository;
using MyBBS.IService;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyBBS.Service
{
    public class BaseService<T> : IBaseService<T> where T : class, new()
    {
        protected IBaseRepository<T> _IBaseRepository;
        public async Task<bool> CreateAsync(T entity)
        {
            return await _IBaseRepository.CreateAsync(entity); 
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _IBaseRepository.DeleteAsync(id);
        }

        public async Task<bool> EditAsync(T entity)
        {
            return await _IBaseRepository.EditAsync(entity);
        }

        public async Task<T> FindAsync(int id)
        {
            return await _IBaseRepository.FindAsync(id);
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> func)
        {
            return await _IBaseRepository.FindAsync(func);
        }

        public async Task<List<T>> QueryAsync()
        {
            return await _IBaseRepository.QueryAsync();
        }

        public async Task<List<T>> QueryAsync(Expression<Func<T, bool>> fuuc)
        {
            return await _IBaseRepository.QueryAsync(fuuc);
        }

        public async Task<List<T>> QueryAsync(int page, int size, RefAsync<int> total)
        {
            return await _IBaseRepository.QueryAsync(page, size, total);
        }

        public async Task<List<T>> QueryAsync(Expression<Func<T, bool>> fuuc, int page, int size, RefAsync<int> total)
        {
            return await _IBaseRepository.QueryAsync(fuuc, page, size, total);
        }
    }
}
