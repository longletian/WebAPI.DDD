using InfrastructureBase;
using InfrastructureBase.Data;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

namespace Identity.Application
{
    public class AuthService : IAuthService
    {
        public readonly IDaprRepository daprRepository;
        private readonly JwtConfig jwtConfig;

        public AuthService(IDaprRepository _daprRepository, IOptionsSnapshot<JwtConfig> _jwtConfig)
        {
            daprRepository = _daprRepository;
            jwtConfig = _jwtConfig.Value;
        }

        public async Task<ResponseData<string>> GetToken()
        {
            var now = DateTime.Now;
            var expires = now.Add(TimeSpan.FromMinutes(30));
            string audience = jwtConfig.Audience;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.IssuerSigningKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new Claim[]{
                  new Claim(ClaimTypes.Name,"张三")
                };
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: jwtConfig.Issuer,
                audience: audience,
                claims: claims,
                expires: expires,
                signingCredentials: creds);

            string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            await daprRepository.SetStateAsync<string>("Key_Token", token);
            await daprRepository.SetStateAsync<string>("Now_Time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            await daprRepository.SetStateAsync<string>("Key_Token", token);

            string token1 = await daprRepository.InvokeMethodAsync<string, string>(method: HttpMethod.Get, "IdentityApi", "/api/Auth/test", token);

            string tokenLast = daprRepository.GetStateAsync<string>("Key_Token").Result;

            return new ResponseData<string> { MsgCode = 0, Message = "", Data = token+ $"{token1}" };
        }

        public ResponseData<string> RefershToken(string token)
        {
            throw new System.NotImplementedException();
        }

        public ResponseData<string> TestData(string token)
        {
            return new ResponseData<string> { MsgCode = 0, Message = "", Data = "hello" };
        }
    }
}
