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
        /// 菜单编码
        /// </summary
        public string Path { get; set; }

        /// <summary>
        /// 菜单跳转url
        /// </summary>
        public string MenuUrl { get; set; }

        /// <summary>
        /// 菜单备注
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 类型：0导航菜单；1操作按钮。
        /// </summary>

        public int? Type { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? SortNum { get; set; }

    }
}
