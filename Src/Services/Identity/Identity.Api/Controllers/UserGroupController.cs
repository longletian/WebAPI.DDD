using Identity.Application;
using Identity.Application.Dtos;
using InfrastructureBase;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Identity.Api.Controllers
{

    /// <summary>
    /// 用户组模块
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserGroupController : ControllerBase
    {
        private readonly IUserGroupService userGroupService;
        public UserGroupController(IUserGroupService userGroupService)
        {
            this.userGroupService = userGroupService;
        }

        /// <summary>
        /// 获取所有用户组
        /// </summary>
        /// <returns></returns>
        [HttpGet,Route("list")]
        public Task<ResponseData> GetUserGroupListDataAsync()
        {
            return this.userGroupService.GetUserGroupListDataAsync();
        }

        /// <summary>
        /// 删除用户组
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete, Route("{Id}")]
        public Task<ResponseData> DeleteUserGroupByIdAsync(Guid Id)
        {
            return this.userGroupService.DeleteUserGroupByIdAsync(Id);
        }

        /// <summary>
        /// 用户组详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet, Route("{Id}")]
        public Task<ResponseData> GetUserGroupInfoDataAsync(Guid Id)
        {
            return this.userGroupService.GetUserGroupInfoDataAsync(Id);
        }

        /// <summary>
        /// 添加人员进用户组
        /// </summary>
        /// <param name="userGroupAddUserInput"></param>
        /// <returns></returns>
        [HttpPost, Route("ry")]
        public Task<ResponseData> AddUserToGroupAsync([FromBody] UserGroupAddUserInput userGroupAddUserInput)
        {
            return this.userGroupService.AddUserToGroupAsync(userGroupAddUserInput);
        }

        /// <summary>
        /// 新增用户组
        /// </summary>
        /// <param name="userGroupInput"></param>
        /// <returns></returns>
        [HttpPost, Route("")]
        public Task<ResponseData> AddUserGroupDataAsync([FromBody] UserGroupInput userGroupInput)
        {
            return this.userGroupService.AddUserGroupDataAsync(userGroupInput);
        }

        /// <summary>
        /// 修改用户组
        /// </summary>
        /// <param name="userGroupEditInput"></param>
        /// <returns></returns>
        [HttpPut,Route("")]
        public Task<ResponseData> EditUserGroupDataAsync([FromBody]UserGroupEditInput userGroupEditInput)
        {
            return this.userGroupService.EditUserGroupDataAsync(userGroupEditInput);
        }

    }
}
