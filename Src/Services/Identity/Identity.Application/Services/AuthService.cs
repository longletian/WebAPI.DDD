using InfrastructureBase;
using InfrastructureBase.Data;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.Application
{
    public class AuthService : IAuthService
    {
        public readonly IDaprRepository daprRepository;
        private readonly JwtConfig jwtConfig;

        public AuthService(IDaprRepository _daprRepository, IOptionsMonitor<JwtConfig> _jwtConfig)
        {
            daprRepository = _daprRepository;
            jwtConfig = _jwtConfig.CurrentValue;
        }

        public ResponseData<string> GetToken()
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

            daprRepository.SetStateAsync<string>("Key_Token", token);

            string tokenLast = daprRepository.GetStateAsync<string>("Key_Token").Result;

            return new ResponseData<string> { MsgCode = 0, Message = "", Data = token };
        }

        public ResponseData<string> RefershToken(string token)
        {
            throw new System.NotImplementedException();
        }

        public ResponseData<string> TestData()
        {
            throw new System.NotImplementedException();
        }
    }
}
