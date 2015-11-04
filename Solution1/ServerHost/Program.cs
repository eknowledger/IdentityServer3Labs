using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.InMemory;
using Microsoft.Owin.Hosting;
using Owin;
using Serilog;

namespace ServerHost
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // logging
            Log.Logger = new LoggerConfiguration()
                .WriteTo
                .LiterateConsole(outputTemplate: "{Timestamp:HH:MM} [{Level}] ({Name:l}){NewLine} {Message}{NewLine}{Exception}")
                .CreateLogger();

            // hosting identityserver
            using (WebApp.Start<Startup>("https://localhost:44333"))
            {
                Console.WriteLine("server running...");
                Console.ReadLine();
            }
        }
    }

    static class IdentityServerServiceFactoryExt
    {
        public static IdentityServerServiceFactory UseInMemoryClients(this IdentityServerServiceFactory factory, IEnumerable<Client> clients)
        {

            var clientStore = new InMemoryClientStore(clients);
            factory.ClientStore = new Registration<IClientStore>(clientStore);
            return factory;
        }

        public static IdentityServerServiceFactory UseInMemoryScopes(this IdentityServerServiceFactory factory, IEnumerable<Scope> scopes)
        {

            var scopeStore = new InMemoryScopeStore(scopes);
            factory.ScopeStore = new Registration<IScopeStore>(scopeStore);
            return factory;
        }
        public static IdentityServerServiceFactory UseInMemoryUsers(this IdentityServerServiceFactory factory, IEnumerable<InMemoryUser> users)
        {


            factory.UserService = new Registration<IUserService>();

            return factory;
        }
    }

    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var options = new IdentityServerOptions
            {
                Factory = new IdentityServerServiceFactory()
                            .UseInMemoryClients(Clients.Get())
                            .UseInMemoryScopes(Scopes.Get())
                            .UseInMemoryUsers(new List<InMemoryUser>())
            };

            
            app.UseIdentityServer(options);
        }
    }


    internal static class Scopes
    {
        public static List<Scope> Get()
        {
            return new List<Scope>
            {
                new Scope
                {
                    Name = "api1"
                }
            };
        }
    }

    internal static class Clients
    {
        public static List<Client> Get()
        {
            
            return new List<Client>
            {
                // no human involved
                new Client
                {
                    ClientName = "Silicon-only Client",
                    ClientId = "silicon",
                    Enabled = true,
                    AccessTokenType = AccessTokenType.Reference,
                    Flow = Flows.ClientCredentials,

                    
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("F621F470-9731-4A25-80EF-67A6F7C5F4B8".Sha256())
                    },

                    AllowedScopes = new List<string>
                    {
                        "api1"
                    }
                }
            };
        }
    }
}
