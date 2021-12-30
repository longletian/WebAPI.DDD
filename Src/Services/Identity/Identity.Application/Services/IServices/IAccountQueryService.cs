using InfrastructureBase;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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
        Task<ResponseData> GetUserInfoAsync(int Id);
    }
}
