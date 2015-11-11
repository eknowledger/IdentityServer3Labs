using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Thinktecture.IdentityModel.Mvc;

namespace WebAuthServer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Auth(Roles = "geek")]
        public ActionResult About()
        {

            return View((User as ClaimsPrincipal).Claims);
        }

       [ResourceAuthorize("Read", "ContactDetails")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [ResourceAuthorize("Write", "ContactDetails")]
        [HandleForbidden]
        public ActionResult UpdateContact()
        {
            if (!HttpContext.CheckAccess("Write", "ContactDetails", "some more data"))
            {
                // either 401 or 403 based on authentication state
                return this.AccessDenied();
            }


            ViewBag.Message = "Update your contact details!";

            return View();
        }

        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut();
            return Redirect("/");
        }
    }
}