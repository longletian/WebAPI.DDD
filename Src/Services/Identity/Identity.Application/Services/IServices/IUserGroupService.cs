using Identity.Application.Dtos;
using InfrastructureBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application
{
    public interface IUserGroupService
    {
        /// <summary>
        /// 获取所有用户组
        /// </summary>
        /// <returns></returns>
        Task<ResponseData> GetUserGroupListDataAsync();

        /// <summary>
        /// 删除用户组
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<ResponseData> DeleteUserGroupByIdAsync(Guid Id);

        /// <summary>
        /// 用户组详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<ResponseData> GetUserGroupInfoDataAsync(Guid Id);

        /// <summary>
        /// 添加人员进用户组
        /// </summary>
        /// <param name="userGroupAddUserInput"></param>
        /// <returns></returns>
        Task<ResponseData> AddUserToGroupAsync(UserGroupAddUserInput userGroupAddUserInput);

        /// <summary>
        /// 新增用户组
        /// </summary>
        /// <param name="userGroupInput"></param>
        /// <returns></returns>
        Task<ResponseData> AddUserGroupDataAsync(UserGroupInput userGroupInput);

        /// <summary>
        /// 修改用户组
        /// </summary>
        /// <param name="userGroupEditInput"></param>
        /// <returns></returns>
        Task<ResponseData> EditUserGroupDataAsync(UserGroupEditInput userGroupEditInput);

        /// <summary>
        /// 获取用户组用户列表
        /// </summary>
        /// <param name="unitUserQo"></param>
        /// <returns></returns>
        Task<ResponseData> GetPagingUserGroupUserListDataAsync(UserGroupUserQo userGroupUserQo);
    }
}
