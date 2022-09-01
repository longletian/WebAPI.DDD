using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Dtos
{
    public class UserGroupOutput
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 用户组名称
        /// </summary>
        public string UserGroupName { get; set; }
        /// <summary>
        /// 父级角色标识
        /// </summary>
        public Guid? ParentId { get; set; }


        public string UserGroupPath { get; set; }

        /// <summary>
        /// 角色备注
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? SortNum { get; set; }

        public List<UserGroupOutput> ChildNodes { get; set; }
    }
}
