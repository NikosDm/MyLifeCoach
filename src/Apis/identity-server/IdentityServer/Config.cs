using System.Collections.Generic;

using Duende.IdentityServer.Models;

namespace IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        [
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        ];

    public static IEnumerable<ApiScope> ApiScopes =>
        [
            new ApiScope("goals-api", "Goals API access"),
            new ApiScope("profile-api", "Profile API access"),
        ];

    public static IEnumerable<Client> Clients =>
        [
            new Client
            {
                ClientId = "postman",
                ClientName = "Postman Client",
                ClientSecrets = { new Secret("NotABigSecretHere".Sha256()) },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                AllowedScopes = { "openid", "profile", "goals-api", "profile-api" },
                RedirectUris = { "https://www.getpostman.com/oath2/callback" }
            },

            new Client
            {
                ClientId = "interactive",
                ClientSecrets = { new Secret("ThisIsASecretToBeKeptHere123".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = { "https://localhost:44300/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" },

                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "goals-api", "profile-api" },
                AllowAccessTokensViaBrowser = true
            },
        ];
}
