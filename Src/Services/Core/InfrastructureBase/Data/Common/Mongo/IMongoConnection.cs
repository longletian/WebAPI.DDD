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
        void AddEntityToCollection<T>(string collectionName, T item);

        /// <summary>
        /// 新增文件到集合
        /// </summary>
        Task AddEntityToCollectionAsync<T>(string collectionName, T item);

        /// <summary>
        /// 新增文件到集合
        /// </summary>
        void AddEntitiesToCollection<T>(string collectionName, List<T> items);
    } 
}
