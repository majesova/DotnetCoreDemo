namespace PDV.API.Infrastructure.Authentication
{
    /// <summary>
    /// Jwt setting params
    /// </summary>
     public class JwtSettings
    {
        public string Key { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public int DaysToExpiration { get; set; }
    }
}