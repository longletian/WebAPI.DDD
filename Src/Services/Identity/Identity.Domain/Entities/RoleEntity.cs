using DomainBase;
using System;

namespace Identity.Domain
{
    /// <summary>
    /// 角色
    /// </summary>
    public class RoleEntity : Entity, IAggregateRoot
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 父级角色标识
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 角色备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public long SortNum { get; set; }

    }
}
