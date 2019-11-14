using System.ComponentModel.DataAnnotations;

namespace PDV.API.Infrastructure.Authentication
{
    public class CustomClaimsTypes {
        public static string StoreId => "storeId";
    }

    /// <summary>
    /// Claims for authenticated user
    /// </summary>
    public class AuthenticatedUserClaim
    {

        [Required()]
        public string ClaimType { get; set; }

        [Required()]
        public string ClaimValue { get; set; }
    }
}