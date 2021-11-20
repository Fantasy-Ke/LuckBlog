using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LuckyBlog.IService
{
    public interface IBaseService<T>where T:class,new()
    {
        /// <summary>
        /// 进行添加数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> CreateAsync(T entity);
        /// <summary>
        /// 通过id删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(int id);
        /// <summary>
        /// 通过条件进行编辑
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> EditAsync(T entity);
        /// <summary>
        /// 通过id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> FindAsync(int id);
        Task<T> FindAsync(Expression<Func<T, bool>> func);
        /// <summary>
        /// 查询全部数据
        /// </summary>
        /// <returns></returns>
        Task<List<T>> QueryAsync();
        /// <summary>
        /// 按照条件查询数据
        /// </summary>
        /// <param name="fuuc"></param>
        /// <returns></returns>
        Task<List<T>> QueryAsync(Expression<Func<T, bool>> fuuc);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        Task<List<T>> QueryAsync(int page, int size, RefAsync<int> total);
        /// <summary>
        /// 自定义条件分页
        /// </summary>
        /// <param name="fuuc"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        Task<List<T>> QueryAsync(Expression<Func<T, bool>> fuuc, int page, int size, RefAsync<int> total);
    }
}
