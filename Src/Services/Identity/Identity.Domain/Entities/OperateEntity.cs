using DomainBase;
using System;

namespace Identity.Domain
{
    /// <summary>
    /// 功能权限表(按钮操作方面)
    /// </summary>
    public class OperateEntity : Entity
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        public string OperateName { get; set; }

        /// <summary>
        /// 父级标识
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public long SortNum { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 是否允许
        /// </summary>
        public int? IsAble { get; set; }

    }
}
