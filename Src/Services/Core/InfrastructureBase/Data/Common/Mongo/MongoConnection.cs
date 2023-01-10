using InfrastructureBase.Data.Common.Mongo;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FreeSql.Internal.GlobalFilter;

namespace InfrastructureBase.Data
{
    public class MongoConnection : IMongoConnection
    {
        public readonly string defaultCollectionName;
        public readonly string dataBaseName;
        private readonly MongoClient client;
        private readonly IMongoDatabase database;
        public MongoConnection(MongoClient _client, string _dataBaseName, string _collectionName)
        {
            client = _client;
            dataBaseName = _dataBaseName;
            defaultCollectionName = _collectionName;
            database = GetDataBase();
        }

        public IMongoDatabase Database { get { return database ?? GetDataBase(); } }

        /// <summary>
        /// 获取数据库
        /// </summary>
        /// <returns></returns>
        public IMongoDatabase GetDataBase()
        {
            return client.GetDatabase(dataBaseName);
        }

        /// <summary>
        /// 获取集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IMongoCollection<T> GetCollections<T>()
        {
            return this.database.GetCollection<T>(defaultCollectionName ?? nameof(T));
        }

        /// <summary>
        /// 新增文件到集合
        /// </summary>
        public void AddEntityToCollection<T>(string collectionName, T item)
        {
            if (item == null)
                throw new ArgumentNullException("参数异常，不能为空");
            this.database.GetCollection<T>(collectionName ?? defaultCollectionName).InsertOne(item);
        }

        /// <summary>
        /// 新增文件到集合
        /// </summary>
        public async Task AddEntityToCollectionAsync<T>(string collectionName, T item)
        {
            if (item == null)
                throw new ArgumentNullException("参数异常，不能为空");
            await this.database.GetCollection<T>(collectionName ?? defaultCollectionName).InsertOneAsync(item);
        }

        /// <summary>
        /// 新增文件到集合
        /// </summary>
        public void AddEntitiesToCollection<T>(string collectionName, List<T> items)
        {
            if (items == null && items.Count == 0)
                throw new ArgumentNullException("参数异常，不能为空");
            this.database.GetCollection<T>(collectionName ?? defaultCollectionName).InsertMany(items);
        }
    }
}

