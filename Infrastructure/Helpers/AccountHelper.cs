using PDV.API.Infrastructure.Authentication;
using System.Security.Claims;

namespace PDV.API.Infrastructure.Helpers
{
    public class AccountHelper
    {
        public static string CurrentUserId(ClaimsPrincipal claims)
        {
            return claims.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public static string CurrentStoreId(ClaimsPrincipal claims)
        {
            return claims.FindFirstValue(CustomClaimsTypes.StoreId);
        }
    }
}
