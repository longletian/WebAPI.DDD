using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace InfrastructureBase.Base.AuthBase.CustomAuth
{
    public class CustomAuthenticationHandler : AuthenticationHandler<CustomAuthenticationOptions>
    {
        public CustomAuthenticationHandler(IOptionsMonitor<CustomAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        /// <summary>
        /// 编写自定义认证方式
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // 从请求头中获取token
            //string tokenHeader = Request.Headers["Token"];
            // 从请求参数中获取token
            //Request.Query.TryGetValue("Token", out var tokenFromQuery);

            Request.Headers.TryGetValue("Token", out var token);
            if (token.ToString() != "XXXXXX")
            {
                return await Task.FromResult(AuthenticateResult.NoResult());
            }

            // 验证成功之后组装用户信息
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,""),
                new Claim(ClaimTypes.Email,""),
                new Claim("","")
            };

            // 一组claims构成了一个identity，具有这些claims的identity就是 ClaimsIdentity
            // ClaimsIdentity 证件
            // Scheme 证件类型（这里假设是驾照）
            var identity =new ClaimsIdentity(claims,CustomAuthenticationOptions.Scheme);
            // ClaimsIdentity的持有者就是 ClaimsPrincipal
            // ClaimsPrincipal就是持有证件的人
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, CustomAuthenticationOptions.Scheme);
            return AuthenticateResult.Success(ticket);
        }
    }
}
