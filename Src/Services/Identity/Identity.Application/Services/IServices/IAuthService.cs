using InfrastructureBase;

namespace Identity.Application
{
    public interface IAuthService
    {
        ResponseData<string> GetToken();

        ResponseData<string> RefershToken(string token);

        ResponseData<string> TestData();

    }
}
