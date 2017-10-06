using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dashboard.IDP.Configuration
{
    public class InMemoryConfiguration
    {
        public static IEnumerable<ApiResource> ApiResources()
        {
            return new[] {
                new ApiResource("dashboard", "Sigma Dashboard")
            };
        }
        public static IEnumerable<IdentityResource> IdentityResources()
        {
            return new List<IdentityResource> {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }
        public static IEnumerable<Client> Clients()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "dashboard",
                    ClientName = "Sigma Dashboard",
                    ClientSecrets = new [] { new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    RedirectUris = new List<string>()
                    {
                        "https://localhost:44395/signin-oidc"
                    },
                    PostLogoutRedirectUris = new List<string>()
                    {
                        "https://localhost:44395/signout-callback-oidc"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    },
                    // AlwaysIncludeUserClaimsInIdToken = true,
                    AllowOfflineAccess = true
                }
            };
        }
        public static IEnumerable<TestUser> Users()
        {
            return new[] {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "mail@mtzlosa.es",
                    Password = "password",
                    Claims = new List<Claim>
                    {
                        new Claim("given_name","Andy"),
                        new Claim("family_name","Kat"),
                    }
                }
            };
        }
    }
}
