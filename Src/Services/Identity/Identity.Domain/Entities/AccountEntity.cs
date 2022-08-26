using DomainBase;
using Identity.Domain.Enums;

namespace Identity.Domain
{
    /// <summary>
    /// 账号
    /// </summary>
    public class AccountEntity : Entity, IAggregateRoot
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
        public AccountState State { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool? IsEnable { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? SortNum { get; set; }
    }
}
