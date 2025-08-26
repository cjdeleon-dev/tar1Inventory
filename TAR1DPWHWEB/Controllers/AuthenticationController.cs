using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TAR1DPWHDATA.DataModels;
using TAR1DPWHDATA.DataServices.AuthenticationService;
using TAR1DPWHDATA.Enums;
using TAR1DPWHDATA.Globals;

namespace TAR1DPWHWEB.Controllers
{
    public class AuthenticationController : Controller
    {
        private IAuthenticationService iauservice;

        // GET: Authentication
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserLoginModel ulm, string returnUrl)
        {
            iauservice = new AuthenticationService();

            if (ModelState.IsValid)
            {
                UserLoginViewModel ulvm = new UserLoginViewModel();
                ulvm = iauservice.GetUserByUserCredentials(ulm.Username, ulm.Password);
                
                if(!ulvm.IsError && ulvm.UserLogged != null)
                {
                    //GlobalVars.IdLogged = ulvm.UserLogged.Id;

                    System.Web.HttpContext.Current.Session["Id"] = ulvm.UserLogged.Id;

                    FormsAuthentication.SetAuthCookie(ulm.Username, false);

                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Dashboard", "Account");
                    }

                }
                else
                {
                    ModelState.AddModelError("CredentialError", ulvm.ProcessMessage);
                    return View("Login");
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();

            System.Web.HttpContext.Current.Session.RemoveAll();

            ViewBag.Title = "Login";

            return RedirectToAction("Login", "Authentication");
        }
    }
}