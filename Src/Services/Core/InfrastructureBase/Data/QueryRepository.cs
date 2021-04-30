using Dapper;
using DomainBase;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Text;

namespace InfrastructureBase
{
    /// <summary>
    /// 目前只支持mysql数据库
    /// </summary>
    /// <typeparam name="DomainModel"></typeparam>
    public class QueryRepository<DomainModel> : IQueryRepository<DomainModel> where DomainModel : Entity
    {
        private bool _disposed = false;
        private readonly string _connectionString;
        private IDbConnection dbConnection;

        public QueryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("");
        }

        /// <summary>
        /// 获取请求连接
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetOpenConnection()
        {
            if (this.dbConnection == null || this.dbConnection.State != ConnectionState.Open)
            {
                dbConnection = new MySqlConnection(_connectionString);
                dbConnection.Open();
            }
            return this.dbConnection;
        }

        /// <summary>
        /// 连接自定义（支持mysql,动态配置处理）
        /// </summary>
        /// <returns></returns>
        public IDbConnection CreateNewConnection()
        {
            var connection = new MySqlConnection(_connectionString);
            connection.Open();
            return connection;
        }

        /// <summary>
        /// 执行条数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql, DynamicParameters parameters = null)
        {
            return this.dbConnection.Execute(sql, parameters);
        }

        /// <summary>
        /// 查询结果集中的第一行第一列
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object ExecuteScalar(string sql, DynamicParameters parameters = null)
        {
            return this.dbConnection.ExecuteScalar(sql, parameters);
        }


        public DomainModel FindEntity(string sql, object KeyValue)
        {
            return dbConnection.QueryFirstOrDefault<DomainModel>(sql, KeyValue);
        }

        public DomainModel FindEntity(string strSql, Dictionary<string, string> dbParameter = null)
        {
            return dbConnection.QueryFirstOrDefault<DomainModel>(strSql, dbParameter);
        }

        /// <summary>
        /// 查询动态参数
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="parameters">支持实体，参数</param>
        /// <returns></returns>
        public DomainModel FindEntity(string strSql, DynamicParameters parameters = null)
        {
            return dbConnection.QueryFirstOrDefault<DomainModel>(strSql, parameters);
        }

        /// <summary>
        /// 查询列表根据sql语句
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="strSql">sql语句</param>
        /// <returns></returns>
        public IEnumerable<DomainModel> FindList(string strSql, DynamicParameters parameters = null)
        {
            return dbConnection.Query<DomainModel>(strSql, parameters);
        }

        public IEnumerable<DomainModel> FindList(string strSql, object dbParameter)
        {
            return dbConnection.Query<DomainModel>(strSql, dbParameter);
        }

        public IEnumerable<DomainModel> FindList(string orderField, int pageSize, int pageIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DomainModel> FindList(string strSql, string orderField, int pageSize, int pageIndex, out long total)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DomainModel> FindList(string strSql, string orderField, int pageSize, int pageIndex, out int total, Dictionary<string, string> dict = null)
        {
            throw new NotImplementedException();
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    dbConnection.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
