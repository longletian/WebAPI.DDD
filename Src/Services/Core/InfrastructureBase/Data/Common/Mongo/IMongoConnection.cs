using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfrastructureBase.Data
{
    public interface IMongoConnection
    {
        /// <summary>
        /// 获取当前数据库
        /// </summary>
        IMongoDatabase Database { get; }

        /// <summary>
        /// 获取数据库
        /// </summary>
        /// <returns></returns>
        IMongoDatabase GetDataBase();

        /// <summary>
        /// 获取集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IMongoCollection<T> GetCollections<T>();

        /// <summary>
        /// 新增文件到集合
        /// </summary>
        void AddEntityToCollection<T>(T item, string collectionName = default);

        /// <summary>
        /// 新增文件到集合
        /// </summary>
        Task AddEntityToCollectionAsync<T>(T item,string collectionName= default);

        /// <summary>
        /// 新增文件到集合
        /// </summary>
        void AddEntitiesToCollection<T>(List<T> items,string collectionName= default);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="collectionName"></param>
        void DeleteEntityToCollection<T>(FilterDefinition<T> item, string collectionName = default);

        /// <summary>
        /// 清除集合下面所有文档
        /// </summary>
        void DeleteCollection<T>(string collectionName = default);
    } 
}
