using Dapper;
using DomainBase;
using FreeRedis;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace InfrastructureBase
{
    /// <summary>
    /// 目前只支持mysql数据库
    /// </summary>
    /// <typeparam name="DomainModel"></typeparam>
    public class QueryRepository : IQueryRepository
    {
        private bool _disposed = false;
        private readonly string _connectionString;
        private IDbConnection dbConnection;

        public QueryRepository()
        {
            _connectionString = AppSettingConfig.GetConnStrings("MysqlCon");
        }

        public IDbConnection GetOpenConnection()
        {
            if (this.dbConnection == null || this.dbConnection.State != ConnectionState.Open)
            {
                dbConnection = new MySqlConnection(_connectionString);
                dbConnection.Open();
            }
            return this.dbConnection;
        }

        public IDbConnection CreateNewConnection()
        {
            var connection = new MySqlConnection(_connectionString);
            connection.Open();
            return connection;
        }


        /// <summary>
        /// 查找一个实体（根据sql）
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="strSql">sql语句</param>
        /// <param name="dbParameter">参数</param>
        /// <returns></returns>
        public Task<TEntity> FindEntityAsync<TEntity>(string strSql, DynamicParameters dynamicParameters)
        {
            return dbConnection.QueryFirstAsync<TEntity>(strSql, dynamicParameters);
        }

        /// <summary>
        /// 列表查询
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="strSql"></param>
        /// <param name="dbParameter"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> FindListAsync<TEntity>(string strSql, DynamicParameters dbParameter)
        {
            return await dbConnection.QueryAsync<TEntity>(strSql, dbParameter);
        }

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
        public async Task<PageQueryDto<TEntity>> FindListAsync<TEntity>(string strSql, string orderField, int pageSize, int pageIndex, DynamicParameters dynamicParameters = null) where TEntity : class
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            int num = (pageIndex - 1) * pageSize;
            string OrderBy = "";
            if (!string.IsNullOrEmpty(orderField))
            {
                OrderBy = " Order By " + orderField;
            }
            stringBuilder.Append(strSql + OrderBy);
            stringBuilder.Append(" limit " + num + "," + pageSize + "");
            return new PageQueryDto<TEntity>(await this.dbConnection.QueryAsync<TEntity>(strSql, dynamicParameters), dynamicParameters.Get<long>("@Total"));
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
