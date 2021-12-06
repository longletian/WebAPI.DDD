using Dapper;
using DomainBase;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
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

        public QueryRepository()
        {
            _connectionString = AppSettingConfig.GetConnStrings("");
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

        public DomainModel FindEntity(string sql, object KeyValue)
        {
            return dbConnection.QueryFirstOrDefault<DomainModel>(sql, KeyValue);
        }

        public DomainModel FindEntity(string strSql, Dictionary<string, string> dbParameter = null)
        {
            return dbConnection.QueryFirstOrDefault<DomainModel>(strSql, dbParameter);
        }

        public IEnumerable<DomainModel> FindList(string  sql)
        {
            return dbConnection.Query<DomainModel>(sql);
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
            total = Convert.ToInt32(dbConnection.ExecuteScalar(strSql));
            return this.dbConnection.Query<DomainModel>(strSql);
        }

        public IEnumerable<DomainModel> FindList(string strSql, string orderField, int pageSize, int pageIndex, out int total, Dictionary<string, string> dict = null)
        {
            throw new NotImplementedException();
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

        /// <summary>
        /// 查询集合
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="dbParameter"></param>
        /// <returns></returns>
        public IEnumerable<DomainModel> FindList(string strSql, params string[] dbParameter)
        {
            return dbConnection.Query<DomainModel>(strSql, dbParameter);
        }

        public IEnumerable<DomainModel> FindList(string strSql, DynamicParameters dbParameter)
        {
            return dbConnection.Query<DomainModel>(strSql, dbParameter);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
