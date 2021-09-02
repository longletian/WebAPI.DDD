using DomainBase;
using Identity.Domain.Enums;
using System;

namespace Identity.Domain
{
    /// <summary>
    /// 账号
    /// </summary>
    public class Account : Entity, IAggregateRoot
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 账户状态
        /// </summary>
        public int? State { get; set; }
    }
}
