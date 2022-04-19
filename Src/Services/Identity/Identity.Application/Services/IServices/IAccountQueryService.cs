using System.Collections.Generic;
using InfrastructureBase;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DomainBase.Attributes;
using System.Net.Http;

namespace Identity.Application
{
    public interface IAccountQueryService
    {
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="pageQueryInput"></param>
        /// <returns></returns>
        Task<ResponseData> GetUserList(PageQueryQo pageQueryInput);

        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <returns></returns>
        [RemoteFun()]
        Task<ResponseData> GetUserInfoAsync(int Id);
    }
}
