using Newtonsoft.Json;
using System;
using System.Data;
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

        /// <summary>
        /// 对象转string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string SerializeJSON<T>(T data)
        {
            if (data == null)
                throw new ArgumentNullException("参数不能为空");
            return JsonConvert.SerializeObject(data);
        }

        /// <summary>
        /// string转对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T DeserializeJSON<T>(string json)
        {
            if (!string.IsNullOrWhiteSpace(json))
                throw new ArgumentNullException("参数不能为空");
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// datatable转json互转
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string SerializeDataTableToJSON(DataTable dt)
        {
            if (dt == null)
                throw new ArgumentNullException("参数不能为空");
            return JsonConvert.SerializeObject(dt);
        }

        /// <summary>
        /// json转datatable
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static DataTable SerializeJSONToDataTable(string json)
        {
            if (!string.IsNullOrWhiteSpace(json))
                throw new ArgumentNullException("参数不能为空");
            return JsonConvert.DeserializeObject<DataTable>(json);
        }
    }
}
