using InfrastructureBase;
using System.Threading.Tasks;

namespace Identity.Application
{
    public interface IAuthService
    {
        Task<ResponseData<string>> GetToken();

        ResponseData<string> RefershToken(string token);

        ResponseData<string> TestData(string token);

    }
}
