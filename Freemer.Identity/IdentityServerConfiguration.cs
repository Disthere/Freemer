using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections;
using System.Collections.Generic;

namespace Freemer.Identity
{
    public static class IdentityServerConfiguration
    {
        //Сферы работы приложения, области работы
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("SwaggerAPI", "Swagger API"),
                new ApiScope("blazor", "Blazor WebAssembly"),
                //Для использования пользователями через веб-интерфейс
                new ApiScope("Freemer.Web", "Freemer Web"),
                //Для взаимодействия между Api приложения, получение токена для доступа к другому Api
                new ApiScope("Freemer.Api", "Freemer Api")
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource("SwaggerAPI"),
                new ApiResource("Freemer.Web")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                //Клиент для взаимодействия частей (Api) между собой, использует ClientId, ClienSecret и определенный Scope
                new Client
                {
                    ClientId = "test.client",
                    ClientName = "Test client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("R9J3KJN93-34JN-AU8M-32FO-AF230BK212FP".Sha256())},
                    
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "Freemer.Web",
                        "Freemer.Api"
                    }
                },

                //Клиент получает доступ при помощи имени пользователя и пароля (GrantTypes.ResourceOwnerPassword)
                new Client
                {
                    ClientId = "external",
                    ClientName = "External client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    RequireClientSecret = false,

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "Freemer.Web"
                    }
                },

                new Client
                {
                    ClientId = "Freemer.Web",
                    ClientName = "Freemer",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequirePkce = true,
                    RedirectUris = {"https://localhost:8001/authentication/login-callback"},
                    AllowedCorsOrigins = {"https://localhost:7004/index.html"},
                    PostLogoutRedirectUris = {"https://localhost:7004/index.html"},
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "Freemer.Web"
                    },
                    AllowAccessTokensViaBrowser = false
                },

                new Client
                {
                ClientId = "client_blazor_web_assembly",
                RequireClientSecret = false,
                RequireConsent = false,
                RequirePkce = true,
                AllowedGrantTypes =  GrantTypes.Code,
                AllowedCorsOrigins = { "https://localhost:8001" },
                PostLogoutRedirectUris = { "https://localhost:8001" },
                RedirectUris = { "https://localhost:8001/authentication/login-callback" },
                AllowedScopes =
                {
                    "blazor",
                    "Freemer.Web",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                }
                },
            new Client
                {
                ClientId = "client_id_js",
                RequireClientSecret = false,
                RequireConsent = false,
                RequirePkce = true,
                AllowedGrantTypes =  GrantTypes.Code,
                AllowedCorsOrigins = { "https://localhost:9001" },
                RedirectUris = { "https://localhost:9001/callback.html", "https://localhost:9001/refresh.html" },
                PostLogoutRedirectUris = { "https://localhost:9001/index.html" },
                AllowedScopes =
                {
                    "Freemer.Web",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                }
                },
            new Client
                {
                ClientId = "client_id_swagger",
                ClientSecrets = { new Secret("client_secret_swagger".ToSha256()) },
                AllowedGrantTypes =  GrantTypes.ResourceOwnerPassword,
                AllowedCorsOrigins = { "https://localhost:7001" },
                AllowedScopes =
                {
                    "SwaggerAPI",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                }
                },
            new Client
                {
                ClientId = "client_id",
                ClientSecrets = { new Secret("client_secret".ToSha256()) },

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes =
                {
                    "Freemer.Web",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                }
                },
            new Client
                {
                ClientId = "client_id_mvc",
                ClientSecrets = { new Secret("client_secret_mvc".ToSha256()) },

                AllowedGrantTypes = GrantTypes.Code,

                AllowedScopes =
                {
                    "Freemer.Web",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                },

                RedirectUris = {"https://localhost:2001/signin-oidc"},
                PostLogoutRedirectUris = {"https://localhost:2001/signout-callback-oidc"},

                RequireConsent = false,

                AccessTokenLifetime = 5,

                AllowOfflineAccess = true

                // AlwaysIncludeUserClaimsInIdToken = true
               }
            };
    }
}
