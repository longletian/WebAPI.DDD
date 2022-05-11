using System;
using System.Collections.Generic;
using System.Text;

namespace InfrastructureBase.Data
{
    /// <summary>
    /// rabbitmq配置实体
    /// </summary>
    public class RabbitmqOptions
    {
        /// <summary>
        /// 主机名称
        /// </summary>
        public string HostName { get; set; }

        /// <summary>
        ///  用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 主机
        /// </summary>
        public string VirtualHost { get; set; }


        /// <summary>
        /// 重试次数，默认为5
        /// </summary>
        public int EventBusRetryCount { get; set; }
    }
}
