using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace WebAuthServer.Controllers
{
    public class CallApiController : Controller
    {
        // GET: CallApi/ClientCredentials
        public async Task<ActionResult> ClientCredentials()
        {
            var response = await GetTokenAsync();
            var result = await CallApi(response.AccessToken);

            ViewBag.Json = result;
            return View("ShowApiResult");
        }

        // GET: CallApi/UserCredentials
        public async Task<ActionResult> UserCredentials()
        {
            var user = User as ClaimsPrincipal;
            var token = user.FindFirst("access_token").Value;
            var result = await CallApi(token);

            ViewBag.Json = result;
            return View("ShowApiResult");
        }

        private async Task<TokenResponse> GetTokenAsync()
        {
            var client = new TokenClient(
                "https://localhost:44300/identity/connect/token",
                "mvc_service",
                "secret");

            return await client.RequestClientCredentialsAsync("sampleApi");
        }

        private async Task<string> CallApi(string token)
        {
            var client = new HttpClient();
            client.SetBearerToken(token);

            var json = await client.GetStringAsync("https://localhost:44300/identity");
            return JArray.Parse(json).ToString();
        }
    }
}