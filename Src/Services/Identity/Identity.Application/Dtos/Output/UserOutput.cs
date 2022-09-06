using Identity.Domain.Enums;
using Identity.Infrastructure.PersistenceObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Dtos
{
    public class UserOutput
    {
        public Guid AccountId { get; set; }

        public Guid Id { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// 账户状态
        /// </summary>
        public AccountState State { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool? IsEnable { get; set; }

        /// 用户姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string ImagePath { get; set; }

        public string RoleIds { get; set; }

        public string RoleNames { get; set; }

        public List<Permission> Permissions { get; set; }

    }
}
