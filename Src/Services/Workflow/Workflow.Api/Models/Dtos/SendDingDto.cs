using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Workflow.Api.Models
{
    public class SendDingDto
    {
        /// <summary>
        /// 创建人
        /// </summary>
        [JsonProperty("createrAccount")]
        public AccountInfo CreaterAccount { get; set; }

        /// <summary>
        /// DING通知方式，短信(sms)、APP内(app)
        /// </summary>
        [JsonProperty("notifyType")]
        public string NotifyType { get; set; } = string.Empty;

        /// <summary>
        /// 接收人列表
        /// </summary>
        [JsonProperty("receiverAccounts")]
        public List<AccountInfo> ReceiverAccounts { get; set; } = new List<AccountInfo>();

        /// <summary>
        /// DING消息体加密方式，明文(plaintext) or 密文(ciphertext)
        /// </summary>
        [JsonProperty("textType")]
        public string TextType { get; set; } = string.Empty;

        /// <summary>
        /// DING内容消息体，格式参考下示例，只支持文本
        /// </summary>
        [JsonProperty("body")]
        public BodyContent Body { get; set; }

        /// <summary>
        /// DING消息体类型，文本--text
        /// </summary>
        [JsonProperty("bodyType")]
        public string BodyType { get; set; }=string.Empty;
    }
    
    public class AccountInfo
    {
        /// <summary>
        /// 账号标识
        /// </summary>
        [JsonProperty("accountId")]
        public string AccountId { get; set; } = string.Empty;
    }

    public class BodyContent
    {
        /// <summary>
        /// 内容
        /// </summary>
        [JsonProperty("text")]
        public string text { get; set; } = string.Empty;
    }
}