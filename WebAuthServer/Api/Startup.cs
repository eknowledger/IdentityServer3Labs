using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using IdentityServer3.AccessTokenValidation;
using Owin;

namespace Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = "https://localhost:44300/identity",
                RequiredScopes = new[] {"SampleApi"}
            });
            // web api configuration
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            app.UseWebApi(config);
        }
    }
}