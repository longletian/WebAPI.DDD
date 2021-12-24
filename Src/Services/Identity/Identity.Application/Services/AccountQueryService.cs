using InfrastructureBase;
using System.Threading.Tasks;

namespace Identity.Application
{
    public class UserQueryService : IAccountQueryService
    {
        public Task<ResponseData> GetUserInfoAsync(int Id)
        {
            throw new System.NotImplementedException();
        }

        public Task<ResponseData> GetUserList(PageQueryQo pageQueryInput)
        {
            throw new System.NotImplementedException();
        }
    }
}
