using Identity.Application.Dtos;
using InfrastructureBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application
{
    public interface IUserService
    {
        /// <summary>
        /// 新增用户账号
        /// </summary>
        /// <returns></returns>
        Task<ResponseData> AddUserAccountDataAsync(RegisterAccountInput registerAccountInput);

        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <returns></returns>
        Task<ResponseData> LoginUserDataAsync(AccountLoginInput accountLoginInput);

        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <returns></returns>
        Task<ResponseData> GetUserInfoDataAsync(Guid userId);

        /// <summary>
        /// 获取人员用户列表
        /// </summary>
        /// <returns></returns>
        Task<ResponseData> GetPaingUserListDataAsync(UserQo userQo);
    }
}
