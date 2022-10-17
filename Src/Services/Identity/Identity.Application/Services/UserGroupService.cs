using DomainBase;
using Identity.Application.Dtos;
using Identity.Domain;
using Identity.Infrastructure.PersistenceObject;
using InfrastructureBase;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application
{
    public class UserGroupService : IUserGroupService
    {
        private readonly IFreeSql freeSql;
        public UserGroupService(IFreeSql _freeSql)
        {
            freeSql = _freeSql;
        }


        /// <summary>
        /// 获取所有用户组
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseData> GetUserGroupListDataAsync()
        {
            List<UserGroup> userGroupEntities = await this.freeSql.Select<UserGroup>().ToListAsync();
            List<UserGroupOutput> cameraGroups = userGroupEntities?.Adapt<List<UserGroupOutput>>();
            List<UserGroupOutput> result = this.CreateTree(cameraGroups);
            return new ResponseData
            {
                MsgCode = 0,
                Message = "获取成功",
                Data = result
            };
        }

        /// <summary>
        /// 删除用户组
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<ResponseData> DeleteUserGroupByIdAsync(Guid Id)
        {
            this.freeSql.Delete<UserGroup>().Where((c) => c.Id == Id);
            return await Task.FromResult(new ResponseData { MsgCode = 0, Message = "请求成功" });
        }

        /// <summary>
        /// 用户组详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<ResponseData> GetUserGroupInfoDataAsync(Guid Id)
        {
            UserGroup userGroupEntity = await this.freeSql.Select<UserGroup>().Where((c) => c.Id == Id).ToOneAsync();
            return await Task.FromResult(new ResponseData { MsgCode = 0, Message = "请求成功", Data = userGroupEntity });
        }

        /// <summary>
        /// 添加人员进用户组
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseData> AddUserToGroupAsync(UserGroupAddUserInput userGroupAddUserInput)
        {
            UserGroup userGroupEntity = await this.freeSql.Select<UserGroup>().Where((c) => c.Id == userGroupAddUserInput.UserGroupId).ToOneAsync();
            if (userGroupEntity == null)
                throw new ArgumentException("参数异常");

            List<UserGroupUser> userGroupUsers = new List<UserGroupUser>();
            this.freeSql.Delete<UserGroupUser>().Where((c) => c.UserGroupId == userGroupAddUserInput.UserGroupId);
            if (userGroupAddUserInput.UserGroupAddUserDtos != null && userGroupAddUserInput.UserGroupAddUserDtos.Count() > 0)
            {
                userGroupAddUserInput.UserGroupAddUserDtos?.ForEach((c) =>
                {
                    userGroupUsers.Add(new UserGroupUser
                    {
                        UserGroupId = userGroupAddUserInput.UserGroupId,
                        UserId = c.UserId
                    });
                });
            }

            if (userGroupUsers != null && userGroupUsers.Count() > 0)
                this.freeSql.Insert<UserGroupUser>(userGroupUsers);
            return new ResponseData { MsgCode = 0, Message = "请求成功" };
        }

        /// <summary>
        /// 新增用户组
        /// </summary>
        /// <param name="addUserGroupDTO"></param>
        /// <returns></returns>
        public async Task<ResponseData> AddUserGroupDataAsync(UserGroupInput userGroupInput)
        {
            UserGroup userGroupEntity = userGroupInput.Adapt<UserGroup>();
            userGroupEntity.UserGroupPath = await CreateNewGuid(userGroupEntity);
            await this.freeSql.Insert(userGroupEntity).ExecuteAffrowsAsync();
            return await Task.FromResult(new ResponseData { MsgCode = 0, Message = "请求成功" });
        }

        /// <summary>
        /// 获取用户组用户列表
        /// </summary>
        /// <param name="userGroupUserQo"></param>
        /// <returns></returns>
        public async Task<ResponseData> GetPagingUserGroupUserListDataAsync(UserGroupUserQo userGroupUserQo)
        {
            var select = this.freeSql.Select<User, UserGroupUser, UserGroup>()
                  .LeftJoin((a, b, c) => a.Id == b.UserId)
                  .LeftJoin((a, b, c) => b.UserGroupId == c.Id)
                  .WhereIf(userGroupUserQo.UserGroupId.HasValue,(a,b,c)=>c.Id== userGroupUserQo.UserGroupId.Value)
                  .WhereIf(!string.IsNullOrWhiteSpace(userGroupUserQo.KeyWord), (a, b, c) =>
                      a.Name.Contains(userGroupUserQo.KeyWord)
                  );
            var total = await select.CountAsync();
            var list = await select.Page(userGroupUserQo.Page, userGroupUserQo.PageSize).ToListAsync();
            return new ResponseData
            {
                MsgCode = 0,
                Message = "请求成功",
                Data = new PageQueryDto<User>(list, total, userGroupUserQo.Page, userGroupUserQo.PageSize)
            };
        }

        /// <summary>
        /// 创建Pids格式
        /// 如果pid是0顶级节点，pids就是 [0];
        /// 如果pid不是顶级节点，pids就是 pid菜单的 pids + [pid] + ,
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        private async Task<string> CreateNewGuid(UserGroup userGroup)
        {
            if (userGroup.ParentId.HasValue==false)
            {
                return $"[{userGroup.Id}],";
            }
            else
            {
                var groupEntity = await this.freeSql.Select<UserGroup>().Where((c) => c.Id == userGroup.ParentId.Value).ToOneAsync();
                return groupEntity.UserGroupPath+ $"[{userGroup.Id}],";
            }
        }

        /// <summary>
        /// 修改用户组
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public async Task<ResponseData> EditUserGroupDataAsync(UserGroupEditInput userGroupEditInput)
        {
            UserGroup userGroupEntity = await this.freeSql.Select<UserGroup>().Where((c) => c.Id == userGroupEditInput.Id).ToOneAsync();
            if (userGroupEntity == null)
                throw new ArgumentException("参数异常");

            userGroupEntity = userGroupEditInput.Adapt<UserGroup>();
            userGroupEntity.UserGroupPath = await CreateNewGuid(userGroupEntity);
            this.freeSql.Update<UserGroup>(userGroupEntity);
            return await Task.FromResult(new ResponseData { MsgCode = 0, Message = "请求成功" });
        }

        /// <summary>
        /// 生成树结构数据
        /// </summary>
        /// <param name="cameraGroups"></param>
        /// <returns></returns>
        private List<UserGroupOutput> CreateTree(List<UserGroupOutput> userGroups)
        {
            List<UserGroupOutput> cameraTree = userGroups.Where(t => t.ParentId is null)
                .Select(t => new UserGroupOutput
                {
                    Id = t.Id,
                    UserGroupName = t.UserGroupName,
                    UserGroupPath = t.UserGroupPath,
                    ParentId = t.ParentId,
                    SortNum = t.SortNum,
                    Description = t.Description,
                    ChildNodes = t.ChildNodes
                })?.ToList();

            cameraTree?.ForEach(t =>
            {
                t.ChildNodes = this.GetChildNode(t, userGroups);
            });

            return cameraTree;
        }

        /// <summary>
        /// 获取子节点
        /// </summary>
        /// <param name="item"></param>
        /// <param name="userGroups"></param>
        /// <returns></returns>
        private List<UserGroupOutput> GetChildNode(UserGroupOutput item, List<UserGroupOutput> userGroups)
        {
            List<UserGroupOutput> childNodes = userGroups?.Where(t => t.ParentId == item.Id)
                ?.Select(t => new UserGroupOutput
                {
                    Id = t.Id,
                    UserGroupName = t.UserGroupName,
                    UserGroupPath = t.UserGroupPath,
                    ParentId = t.ParentId,
                    SortNum = t.SortNum,
                    Description = t.Description,
                    ChildNodes = t.ChildNodes
                })?.ToList();

            foreach (var childNode in childNodes)
            {
                List<UserGroupOutput> tempChildNode = GetChildNode(childNode, userGroups);
                if (tempChildNode.Count > 0)
                {
                    childNode.ChildNodes = tempChildNode;
                }
            }
            return childNodes;
        }
    }
}
