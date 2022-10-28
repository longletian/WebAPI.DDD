using System.Collections.Generic;

namespace Workflow.Api.Models
{
    public class SendDingDto
    {
        /// <summary>
        /// 创建人
        /// </summary>
        public AccountInfo CreaterAccount { get; set; }

        /// <summary>
        /// DING通知方式，短信(sms)、APP内(app)
        /// </summary>
        public string NotifyType { get; set; }

        /// <summary>
        /// 接收人列表
        /// </summary>
        public List<AccountInfo> ReceiverAccounts { get; set; }

        /// <summary>
        /// DING消息体加密方式，明文(plaintext) or 密文(ciphertext)
        /// </summary>
        public string TextType { get; set; }

        /// <summary>
        /// DING内容消息体，格式参考下示例，只支持文本
        /// </summary>
        public BodyContent Body { get; set; }

        /// <summary>
        /// DING消息体类型，文本--text
        /// </summary>
        public string BodyType { get; set; }
    }
    
    public class AccountInfo
    {
        /// <summary>
        /// 账号标识
        /// </summary>
        public string AccountId { get; set; }
    }

    public class BodyContent
    {
        /// <summary>
        /// 内容
        /// </summary>
        public string text { get; set; }
    }
}