using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureBase.Data.Common.Mongo
{
    public class MongoOptions
    {
        /// <summary>
        ///连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 数据库名称
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// 集合名称
        /// </summary>
        public string CollectionName { get; set; }
    }
}
