using System.Collections.Generic;
using Mapster;
using MapsterMapper;

namespace InfrastructureBase
{
    /// <summary>
    /// Mapster实体映射
    /// </summary>
    public static class MapHelper
    {
        /// <summary>
        /// 映射一个新的实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T MapTo<T>(this object obj)
        {
            if (obj == null) return default(T);
            return obj.Adapt<T>();
        }

        public static TDTO MapTo<T, TDTO>(this T obj)
        {
            if (obj == null) return default(TDTO);
            return obj.Adapt<T, TDTO>();
        }

        public static T MapToExist<T>(this object obj)
        {
            IMapper mapper = new Mapper();
            if (obj == null) return default(T);
            return mapper.Map<T>(obj);
        }

        public static T MapTo<T>(this object obj, Dictionary<string, string> dict)
        {
            if (obj == null) return default(T);
            if (dict.Count > 0)
            {
                TypeAdapterBuilder<object> typeAdapterBuilder = obj.BuildAdapter();
                foreach (var item in dict)
                {
                    typeAdapterBuilder.AddParameters(item.Key, item.Value);
                }

                return typeAdapterBuilder.AdaptToType<T>();
            }
            else
            {
                return obj.BuildAdapter().AdaptToType<T>();
            }
        }

        /// <summary>
        /// 泛型映射
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static List<TDestination> MapToList<TDestination>(this IEnumerable<TDestination> source)
        {
            return source.Adapt<List<TDestination>>();
        }
        
        public static void MapTo<T, TDto>()
        {
            //类中包含子类集合
            TypeAdapterConfig<T, TDto>.NewConfig()
                .Fork(config => config.Default.PreserveReference(true));
        }
    }
}