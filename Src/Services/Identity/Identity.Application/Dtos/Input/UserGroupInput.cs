using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Dtos
{
    public class UserGroupInput
    {
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

    }

    public class UserGroupEditInput: UserGroupInput
    {
        public Guid Id { get; set; }

    }

    public class UserGroupAddUserInput
    {
        public Guid UserGroupId { get; set; }
        public List<UserGroupAddUserDto>  UserGroupAddUserDtos { get; set; }
    }

    public class UserGroupAddUserDto
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; }
    }
}
