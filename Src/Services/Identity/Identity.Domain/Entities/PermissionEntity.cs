﻿using DomainBase;
using System;

namespace Identity.Domain
{
    /// <summary>
    /// 权限
    /// </summary>
    public class PermissionEntity : Entity, IAggregateRoot
    {
        /// <summary>
        /// 权限名称
        /// </summary>
        public string PermissionName { get; set; }
        /// <summary>
        /// 权限父用户组id
        /// </summary>
        public Guid? ParentId { get; set; }

        public string PermissionPath { get; set; }

        /// <summary>
        /// 权限备注
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 权限排序
        /// </summary>
        public int? SortNum { get; set; }

        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool? IsEnable { get; set; }

    }
}
    