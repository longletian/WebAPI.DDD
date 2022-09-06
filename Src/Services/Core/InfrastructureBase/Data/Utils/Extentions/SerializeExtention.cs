using System.IO;
using System.Xml.Serialization;

namespace InfrastructureBase.Data.Utils
{
    /// <summary>
    /// 序列化实体扩展（json、xml）
    /// </summary>
    public static class SerializeExtention
    {
        /// <summary>
        /// Model实体对象序列化为XML字符串
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="t">Model实体对象</param>
        /// <returns></returns>
        public static string SerializeXMLL<T>(T t)
        {
            using StringWriter sw = new StringWriter();
            XmlSerializer xmlSerializer = new XmlSerializer(t.GetType());
            xmlSerializer.Serialize(sw, t);
            return sw.ToString();
        }

        /// <summary>
        /// XML反序列化为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="xml">xml字符串</param>
        /// <returns></returns>
        public static T Deserialize<T>(string xml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using StringReader stringReader = new StringReader(xml);
            return (T)xmlSerializer.Deserialize(stringReader);
        }
    }
}
