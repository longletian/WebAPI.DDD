﻿using Dapper;
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

        public DomainModel FindEntity(object KeyValue)
        {
            //return dbConnection.QueryFirst<DomainModel>();
            throw new NotImplementedException();
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
                    dbConnection.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
