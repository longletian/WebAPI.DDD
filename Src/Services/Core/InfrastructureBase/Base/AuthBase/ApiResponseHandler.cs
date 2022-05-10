

using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace InfrastructureBase.Base
{

    /// <summary>
    /// 自定义授权处理
    /// </summary>
    public class ApiResponseHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public ApiResponseHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
