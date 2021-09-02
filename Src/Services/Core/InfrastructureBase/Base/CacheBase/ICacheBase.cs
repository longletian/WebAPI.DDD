using System;
using System.Collections.Generic;
using System.Text;

namespace InfrastructureBase
{

    /// <summary>
    /// redis缓存基础库
    /// </summary>
    public interface ICacheBase
    {

        #region Key-Value
        /// <summary>
        /// 读取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="dbId"></param>
        /// <returns></returns>
        T Read<T>(string cacheKey, int dbId = 0);

        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="value"></param>
        /// <param name="dbId"></param>
        void Write<T>(string cacheKey, T value, int dbId = 0);

        /// <summary>
        /// 写入缓存(当key不存在时，才设置值)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="value"></param>
        /// <param name="dbId"></param>
        void WriteSetNx<T>(string cacheKey, T value, int dbId = 0);

        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="value"></param>
        /// <param name="expireTime"></param>
        /// <param name="dbId"></param>
        void Write<T>(string cacheKey, T value, DateTime expireTime, int dbId = 0);

        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="value"></param>
        /// <param name="timeSpan"></param>
        /// <param name="dbId"></param>
        void Write<T>(string cacheKey, T value, TimeSpan timeSpan, int dbId = 0);

        /// <summary>
        /// 移除指定数据缓存
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="dbId"></param>
        void Remove(string cacheKey, int dbId = 0);

        /// <summary>
        /// 移除全部缓存
        /// </summary>
        void RemoveAll(int dbId = 0);

        /// <summary>
        /// 移除全部库的缓存
        /// </summary>
        void RemoveAll();
        #endregion

        #region Hash 数据类型操作
        void HashSet<T>(string cacheKey, string field, T value, int dbId = 0);

        void HashSet<T>(string cacheKey, Dictionary<string, T> valuePairs, int dbId = 0);

        T HashGet<T>(string cacheKey, string field, int dbId = 0);

        Dictionary<string, T> HashGetAll<T>(string cacheKey, int dbId = 0);

        List<string> HashFields(string cacheKey, int dbId = 0);

        List<T> HashValues<T>(string cacheKey, int dbId = 0);

        void HashDelete(string cacheKey, string field, int dbId = 0);

        void HashDelete(string cacheKey, List<string> fields, int dbId = 0);
        #endregion

        #region List数据类型操作

        /// <summary>
        /// 从左向右存压栈
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="value"></param>
        /// <param name="dbId"></param>
        void ListRightPush<T>(string cacheKey, T value, int dbId = 0);

        #endregion
    }
}
