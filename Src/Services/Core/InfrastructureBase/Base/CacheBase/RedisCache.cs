using FreeRedis;
using System;
using System.Text.Json;
using System.Collections.Generic;

namespace InfrastructureBase
{
    public class RedisCache : ICacheBase, ISingletonDependency
    {
        /// <summary>
        /// 框架 freeredis
        /// </summary>
        private readonly RedisClient redisClient;
        public RedisCache()
        {
            redisClient = new Lazy<RedisClient>(() =>
            {
                //注意必须是ip 
                var r = new RedisClient(AppSettingConfig.GetSection("RedisConfig:RedisCon").ToString());
                r.Serialize = obj => JsonSerializer.Serialize(obj);
                r.Deserialize = (json, type) => JsonSerializer.Deserialize(json, type);
                return r;
            }).Value;
        }

        #region Hash
        //Hash是一个string类型的field(字段)和value(值)的映射表，hash 特别适合用于存储对象。
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="field"></param>
        /// <param name="dbId"></param>
        public void HashDelete(string cacheKey, string field, int dbId = 0)
        {
            using (var db = redisClient.GetDatabase(dbId))
            {
                db.HDel(cacheKey, field);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="fields"></param>
        /// <param name="dbId"></param>
        public void HashDelete(string cacheKey, List<string> fields, int dbId = 0)
        {
            using (var db = redisClient.GetDatabase(dbId))
            {
                db.HDel(cacheKey, fields.ToArray());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="dbId"></param>
        /// <returns></returns>
        public List<string> HashFields(string cacheKey, int dbId = 0)
        {
            using (var db = redisClient.GetDatabase(dbId))
            {
                string[] arrstr = db.HKeys(cacheKey);
                return new List<string>(arrstr);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="field"></param>
        /// <param name="dbId"></param>
        /// <returns></returns>
        public T HashGet<T>(string cacheKey, string field, int dbId = 0)
        {
            using (var db = redisClient.GetDatabase(dbId))
            {
                return db.HGet<T>(cacheKey, field);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="dbId"></param>
        /// <returns></returns>
        public Dictionary<string, T> HashGetAll<T>(string cacheKey, int dbId = 0)
        {
            using (var db = redisClient.GetDatabase(dbId))
            {
                return db.HGetAll<T>(cacheKey);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="dbId"></param>
        public void HashSet<T>(string cacheKey, string field, T value, int dbId = 0)
        {
            using (var db = redisClient.GetDatabase(dbId))
            {
                db.HSet<T>(cacheKey, field, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="valuePairs"></param>
        /// <param name="dbId"></param>
        public void HashSet<T>(string cacheKey, Dictionary<string, T> valuePairs, int dbId = 0)
        {
            using (var db = redisClient.GetDatabase(dbId))
            {
                db.HSet<T>(cacheKey, valuePairs);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="dbId"></param>
        /// <returns></returns>
        public List<T> HashValues<T>(string cacheKey, int dbId = 0)
        {
            using (var db = redisClient.GetDatabase(dbId))
            {
                return new List<T>(db.HVals<T>(cacheKey));
            }
        }
        #endregion

        #region List

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="value"></param>
        /// <param name="dbId"></param>
        public void ListRightPush<T>(string cacheKey, T value, int dbId = 0)
        {
            using (var db = redisClient.GetDatabase(dbId))
            {
                db.LPushX(cacheKey, value);
            }
        }
        #endregion

        #region Set

        /// <summary>
        /// 读取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="dbId"></param>
        /// <returns></returns>
        public T Read<T>(string cacheKey, int dbId = 0)
        {
            using (var db = redisClient.GetDatabase(dbId))
            {
                return db.Get<T>(cacheKey);
            }
        }


        /// <summary>
        /// 写入缓存(当key不存在时，才设置值)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="value"></param>
        /// <param name="dbId"></param>
        public void WriteSetNx<T>(string cacheKey, T value, int dbId = 0)
        {
            using (var db = redisClient.GetDatabase(dbId))
            {
                db.SetNx(cacheKey, value);
            }
        }

        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="value"></param>
        /// <param name="dbId"></param>
        public void Write<T>(string cacheKey, T value, int dbId = 0)
        {
            using (var db = redisClient.GetDatabase(dbId))
            {
                db.Set(cacheKey, value);
            }
        }

        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="value"></param>
        /// <param name="expireTime"></param>
        /// <param name="dbId"></param>
        public void Write<T>(string cacheKey, T value, DateTime expireTime, int dbId = 0)
        {
            using (var db = redisClient.GetDatabase(dbId))
            {
                int timeSecond = (int)expireTime.Subtract(DateTime.Now).TotalSeconds;
                db.Set(cacheKey, value, timeSecond);
            }
        }
        /// <summary>
        ///  写入缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="value"></param>
        /// <param name="timeSpan"></param>
        /// <param name="dbId"></param>
        public void Write<T>(string cacheKey, T value, TimeSpan timeSpan, int dbId = 0)
        {
            using (var db = redisClient.GetDatabase(dbId))
            {
                int timeSecond = (int)timeSpan.TotalSeconds;
                db.Set(cacheKey, value, timeSecond);
            }
        }
        /// <summary>
        ///  移除指定数据缓存
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="dbId"></param>
        public void Remove(string cacheKey, int dbId = 0)
        {
            using (var db = redisClient.GetDatabase(dbId))
            {
                db.Del(cacheKey);
            }
        }

        /// <summary>
        /// 删除指定数据库的全部键
        /// </summary>
        /// <param name="dbId"></param>
        public void RemoveAll(int dbId = 0)
        {
            using (var db = redisClient.GetDatabase(dbId))
            {
                db.Del();
            }
        }

        /// <summary>
        ///  删除所有数据库的全部键
        /// </summary>
        public void RemoveAll()
        {
            using (var db = redisClient.GetDatabase())
            {
                db.Del();
            }
        }
        #endregion

    }
}
