using DomainBase;
using System;

namespace Identity.Domain.Entities
{
    public class MenuEntity : Entity
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// 父级菜单标识
        /// </summary>

        public Guid? ParentId { get; set; }

        /// <summary>
        /// 菜单路径
        /// </summary>
        public string MenuPath { get; set; }

        /// <summary>
        /// 菜单跳转url
        /// </summary>
        public string MenuUrl { get; set; }
        /// <summary>
        /// 菜单备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? SortNum { get; set; }

    }
}
