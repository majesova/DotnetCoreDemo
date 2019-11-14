using System;
using System.Collections.Generic;

namespace PDV.API.Infrastructure.Authentication
{
    /// <summary>
    /// Class for authenticated user information
    /// </summary>
    public class AuthenticatedUser
    {
        public AuthenticatedUser() : base()
        {
            UserName = "Not authorized";
            AccessToken = string.Empty;
        }
        public string UserName { get; set; }

        public bool IsAuthenticated { get; set; }

        public List<AuthenticatedUserClaim> Claims { get; set; }

        public string AccessToken { get; set; }

        public Guid StoreId { get; set; }

        public string StoreName { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string AccountId { get; set; }
    }
}