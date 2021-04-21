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
    public class QueryRepository<DomainModel> : IQueryRepository<DomainModel> where DomainModel : Entity
    {
        private readonly bool _disposed = false;

        private readonly IDbConnection dbConnection;

        public QueryRepository(IConfiguration configuration)
        {
            try
            {
                dbConnection = new MySqlConnection(configuration.GetConnectionString(""));
                if (dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection.Open();
                }
            }
            catch (Exception ex)
            {
                Log.Error("数据库连接异常", ex.Message);
                this.Dispose();
            }
        }

        public bool IsConnected
        {
            get
            {
                return this.dbConnection != null && this.dbConnection.State == ConnectionState.Open;
            }
        }


        public DomainModel FindEntity(object KeyValue)
        {
            return dbConnection.Query();
        }

        public DomainModel FindEntity(Expression<Func<DomainModel, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public DomainModel FindEntity(string strSql, Dictionary<string, string> dbParameter = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DomainModel> FindList()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DomainModel> FindList(Expression<Func<DomainModel, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DomainModel> FindList(string strSql)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DomainModel> FindList(string strSql, object dbParameter)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DomainModel> FindList(string orderField, int pageSize, int pageIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DomainModel> FindList(Expression<Func<DomainModel, bool>> condition, string orderField, int pageSize, int pageIndex, out long total)
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
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
