using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IdentityServer3.Core.Models;

namespace WebAuthServer.IdentityServer
{
    public static class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new[]
            {
            new Client
            {
                ClientName = "MVC Client",
                ClientId = "mvc",
                Flow = Flows.Implicit,

                RedirectUris = new List<string>
                {
                    "https://localhost:44300/"
                },
                PostLogoutRedirectUris = new List<string>
                {
                    "https://localhost:44300/"
                },
                AllowedScopes = new List<string>
                {
                    "openid",
                    "profile",
                    "roles",
                    "sampleApi"
                }
            },
            new Client
            {
                ClientName = "MVC Client (service communication)",
                ClientId = "mvc_service",
                Flow = Flows.ClientCredentials,

                ClientSecrets = new List<Secret>
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes = new List<string>
                {
                    "sampleApi"
                }
            }
        };
        }
    }
}