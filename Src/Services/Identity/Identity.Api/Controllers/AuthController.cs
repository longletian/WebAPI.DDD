using Identity.Application;
using InfrastructureBase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService _authService)
        {
            authService = _authService;
        }


        [HttpGet,Route("token")]
        public ResponseData<string> GetToken()
        {
            return authService.GetToken();
        }
    }
}
