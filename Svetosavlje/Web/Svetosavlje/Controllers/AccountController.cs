using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Web.WebPages.OAuth;
using DotNetOpenAuth.AspNet;
using System.Xml;
using System.Xml.Linq;
using System.Security.Principal;
using System.Web.Security;

namespace Svetosavlje.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logout(string returnUrl)
        {
            System.Threading.Thread.CurrentPrincipal = null;
            FormsAuthentication.SignOut();
            return Redirect(returnUrl);
        }

        public ActionResult Login(string returnUrl)
        {
            return new ExternalLoginResult("google", Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
        }

        public ActionResult ExternalLoginCallback(string returnUrl)
        {
            AuthenticationResult result = OAuthWebSecurity.VerifyAuthentication(Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
            if (!result.IsSuccessful)
            {
                return Redirect(returnUrl);
            }

            // check config file and see if user is admin
            XmlDocument xmlDoc = new XmlDocument(); //* create an xml document object.
            xmlDoc.Load(System.Web.Hosting.HostingEnvironment.MapPath("/admins.config"));

            foreach (XmlNode node in xmlDoc.ChildNodes)
            {
                if (result.UserName.ToLower() == node.InnerText.ToLower())  // user is administrator
                {
                    string[] roles = new string[0]; // maybe add roles later
                    var identity = new GenericIdentity(result.UserName);
                    var principal = new GenericPrincipal(identity, roles);
                    System.Threading.Thread.CurrentPrincipal = principal;
                    FormsAuthentication.SetAuthCookie(result.UserName, false);
                }
            }

            
            // didn't find user as administrator go back
            return Redirect(returnUrl);
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }
    }
}
