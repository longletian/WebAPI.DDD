using InfrastructureBase;
using System.Threading.Tasks;

namespace Identity.Application
{
    public interface IUserQueryService
    {
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="pageQueryInput"></param>
        /// <returns></returns>
        Task<ResponseData> GetUserList(PageQueryInput pageQueryInput);

        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <returns></returns>
        Task<ResponseData> GetUserInfoAsync(int Id);
    }
}
