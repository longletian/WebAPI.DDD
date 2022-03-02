using Dapr;
using Dapr.Client;
using Identity.Application;
using InfrastructureBase;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Threading.Tasks;

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private const string PubSubName = "pubsub";
        private readonly IAuthService authService;

        public AuthController(IAuthService _authService)
        {
            authService = _authService;
        }

        [HttpGet, Route("token")]
        public async Task<ResponseData<string>> GetToken()
        {
            return await authService.GetToken();
        }

        [HttpGet, Route("test")]
        public ResponseData<string> TestData(string token)
        {
            Log.Information("进入方法");
            return authService.TestData(token);
        }

        [HttpGet, Route("pubsub")]
        public ResponseData<string> Test([FromServices] DaprClient daprClient)
        {
            daprClient.PublishEventAsync(PubSubName, "test_topic");
            return new ResponseData<string> { Message = "", MsgCode = 0 };
        }

        [Topic(PubSubName, "test_topic")]
        [HttpPost("sub")]
        public void TestPub()
        {
            Log.Information("进入pub方法");
        }
    }
}
