using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;

namespace DomainBase
{
    /// <summary>
    /// 读
    /// </summary>
    public interface IQueryRepository<TEntity> where TEntity: Entity
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
        /// 执行条数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int ExecuteNonQuery(string sql, DynamicParameters parameters = null);


        /// <summary>
        /// 查询结果集中的第一行第一列
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        object ExecuteScalar(string sql, DynamicParameters parameters = null);

        /// <summary>
        /// 查找一个实体（根据sql）
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="strSql">sql语句</param>
        /// <param name="dbParameter">参数</param>
        /// <returns></returns>
        TEntity FindEntity(string strSql, Dictionary<string, string> dbParameter = null);


        /// <summary>
        /// 查询动态参数
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="parameters">支持实体，参数</param>
        /// <returns></returns>
        TEntity FindEntity(string strSql, DynamicParameters parameters = null);

        /// <summary>
        /// 查询列表根据sql语句
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="strSql">sql语句</param>
        /// <returns></returns>
        IEnumerable<TEntity> FindList(string strSql, DynamicParameters parameters = null);

        /// <summary>
        /// 查询列表根据sql语句(带参数)
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="strSql">sql语句</param>
        /// <param name="dbParameter">参数</param>
        /// <returns></returns>
        IEnumerable<TEntity> FindList(string strSql, object dbParameter);

        /// <summary>
        /// 查询列表(分页)
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="orderField">排序字段</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="total">总共数据条数</param>
        /// <returns></returns>
        IEnumerable<TEntity> FindList(string orderField, int pageSize, int pageIndex);

        /// <summary>
        /// 查询列表(分页)根据sql语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strSql">sql语句</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="total">总共数据条数</param>
        /// <returns></returns>
        IEnumerable<TEntity> FindList(string strSql, string orderField, int pageSize, int pageIndex, out long total);

        /// <summary>
        /// 查询列表(分页)根据sql语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strSql">sql语句</param>
        /// <param name="dbParameter">参数</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="total">总共数据条数</param>
        /// <returns></returns>
        IEnumerable<TEntity> FindList(string strSql, string orderField, int pageSize, int pageIndex, out int total, Dictionary<string, string> dict = null);

    }
}
