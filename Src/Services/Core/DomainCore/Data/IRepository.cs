using FreeSql;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DomainBase
{
    /// <summary>
    /// 写仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<TEntity> where TEntity : Entity
    {
        /// <summary>
        /// 新增对象
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        void Add(TEntity t);
        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        void Update(TEntity t);
        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        void Delete(TEntity t);
        /// <summary>
        /// 根据条件删除对象
        /// </summary>
        /// <param name="t"></param>
        void Delete(Expression<Func<TEntity, bool>> condition);
        /// <summary>
        /// 根据主键获取对象
        /// </summary>
        /// <returns></returns>
        Task<TEntity> GetAsync(object key = null);
        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> AnyAsync(object key = null);
        /// <summary>
        /// 根据条件判断对象是否存在
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> condition);
        /// <summary>
        /// 根据主键获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetManyAsync(Guid[] key);
        /// <summary>
        /// 根据条件获取对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> condition);
    }
}
