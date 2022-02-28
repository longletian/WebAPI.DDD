namespace InfrastructureBase.Data
{
    public class JwtConfig
    {
        /// <summary>
        /// 签发人
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string IssuerSigningKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int AccessTokenExpiresMinutes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RefreshTokenAudience { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int RefreshTokenExpiresMinutes { get; set; }
    }
}
