using Identity.Application;
using Identity.Application.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using InfrastructureBase;

namespace Identity.Api.Controllers
{
    /// <summary>
    /// 用户功能模块
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService _userService)
        {
            userService = _userService;
        }

        /// <summary>
        /// 新增用户账号
        /// </summary>
        /// <returns></returns>
        [HttpPost,Route("")]
        public Task<ResponseData> AddUserAccountDataAsync([FromBody]RegisterAccountInput registerAccountInput)
        {
            return this.userService.AddUserAccountDataAsync(registerAccountInput);
        }

        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <returns></returns>
        [HttpPost,Route("login")]
        public Task<ResponseData> LoginUserDataAsync([FromBody]AccountLoginInput accountLoginInput)
        {
            return this.userService.LoginUserDataAsync(accountLoginInput);
        }

        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("info")]
        public Task<ResponseData> GetUserInfoDataAsync(Guid userId)
        {
            return this.userService.GetUserInfoDataAsync(userId);
        }


    }
}
