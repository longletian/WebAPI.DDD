using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DomainBase
{
    /// <summary>
    /// 读
    /// </summary>
    public interface IQueryRepository
    {

        /// <summary>
        /// 获取请求连接
        /// </summary>
        /// <returns></returns>
        IDbConnection GetOpenConnection();

        /// <summary>
        /// 创建一个新的连接
        /// </summary>
        /// <returns></returns>
        IDbConnection CreateNewConnection();

        /// <summary>
        /// 查找一个实体（根据sql）
        /// </summary>
        /// <typeparam name="TEntity">类型</typeparam>
        /// <param name="strSql">sql语句</param>
        /// <param name="dynamicParameters">参数</param>
        /// <returns></returns>
        Task<TEntity> FindEntityAsync<TEntity>(string strSql, DynamicParameters dynamicParameters);

        /// <summary>
        /// 列表查询
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="strSql"></param>
        /// <param name="dbParameter"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> FindListAsync<TEntity>(string strSql, DynamicParameters dbParameter);

        /// <summary>
        /// 查询列表(分页)根据sql语句
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="strSql">sql语句</param>
        /// <param name="dynamicParameters">参数</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        Task<PageReturnDto<TEntity>> FindListAsync<TEntity>(string strSql, string orderField, int pageSize, int pageIndex, DynamicParameters dynamicParameters = null) where TEntity : class;
    }
}
