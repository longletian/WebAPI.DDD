using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Dtos
{
    public class UnitOutput
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// 父级标识
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string UnitPath { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? SortNum { get; set; }
    }
}
