using IdentityModel;
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
                    new ApiResource("roles"){
                        Name = "dashboard",
                        DisplayName = "Sigma Dashboard",
                        UserClaims  = { "role", "admin" }
                        //ApiSecrets = new List<Secret> {new Secret("secret".Sha256()) }
                    }
                };
        }
        public static IEnumerable<IdentityResource> IdentityResources()
        {
            return new List<IdentityResource> {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Address(),
                new IdentityResource("roles", "My Roles", new [] {"role","admin"})
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
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "roles",
                        "role"
                    },
                    // AlwaysIncludeUserClaimsInIdToken = true,
                    AllowOfflineAccess = true
                }
            };
        }
        public static IEnumerable<TestUser> Users()
        {
            return new List<TestUser> {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "mail@mtzlosa.es",
                    Password = "password",
                    Claims = new List<Claim>
                    {
                        new Claim("given_name","Andy"),
                        new Claim("family_name","Kat"),
                        new Claim("address","1, LaCroix"),
                        new Claim(JwtClaimTypes.Role, "admin")
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "mail@kat.se",
                    Password = "password",
                    Claims = new List<Claim>
                    {
                        new Claim("given_name","Kat"),
                        new Claim("family_name","Woman"),
                        new Claim("address","2, Franc"),
                        new Claim(JwtClaimTypes.Role,"Consult")
                    }

                }

            };
        }
    }
}
