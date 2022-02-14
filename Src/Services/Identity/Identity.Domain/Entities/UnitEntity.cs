using DomainBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain
{
    /// <summary>
    /// 部门
    /// </summary>
    public class UnitEntity : Entity
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// 父级标识
        /// </summary>
        public Guid ParentId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? SortNum { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
