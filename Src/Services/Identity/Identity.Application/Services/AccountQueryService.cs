using InfrastructureBase;
using System.Threading.Tasks;
using Dapr.Client;
using Identity.Domain;
using Identity.Infrastructure.PersistenceObject;
using DomainBase;

namespace Identity.Application
{
    public class AccountQueryService : IAccountQueryService
    {
        private readonly IUserRepository userRepository;
        private readonly DaprClient daprClient;
        public AccountQueryService(IUserRepository _userRepository,DaprClient _daprClient)
        {
            daprClient = _daprClient;
            userRepository = _userRepository;
        }
        
        public async Task<ResponseData> GetUserInfoAsync(int Id)
        {
            UserEntity user = await userRepository.GetAsync(Id);
            return new ResponseData { MsgCode = 0, Message = "请求成功"};
        }

        public Task<ResponseData> GetUserList(PageQueryQo pageQueryInput)
        {
            throw new System.NotImplementedException();
        }
    }
}
