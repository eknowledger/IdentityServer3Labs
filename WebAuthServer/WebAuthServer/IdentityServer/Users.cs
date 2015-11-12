using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAuthServer.IdentityServer
{
    using IdentityServer3.Core;
    using IdentityServer3.Core.Services.InMemory;
    using System.Collections.Generic;
    using System.Security.Claims;

    namespace EmbeddedMvc.IdentityServer
    {
        public static class Users
        {
            public static List<InMemoryUser> Get()
            {
                return new List<InMemoryUser>
            {
                new InMemoryUser
                {
                    Username = "aelmalt",
                    Password = "12345",
                    Subject = "1",

                    Claims = new[]
                    {
                        new Claim(Constants.ClaimTypes.GivenName, "Ahmed"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Elmalt"),
                        new Claim(Constants.ClaimTypes.Role, "Geek"),
                        new Claim(Constants.ClaimTypes.Role, "Foo")
                    }
                },
                new InMemoryUser
                {
                    Username = "Jf",
                    Password = "12345",
                    Subject = "1",

                    Claims = new[]
                    {
                        new Claim(Constants.ClaimTypes.GivenName, "Ahmed"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Elmalt"),
                        new Claim(Constants.ClaimTypes.Role, "Founder"),
                        new Claim(Constants.ClaimTypes.Role, "Foo")
                    }
                }
            };
            }
        }
    }
}