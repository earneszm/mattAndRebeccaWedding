using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using MattAndRebeccaWedding.Models;
using MattAndRebeccaWedding.Utilities.CustomAuth;

namespace MattAndRebeccaWedding.Controllers
{
    public class LoginController : BaseController
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            if (user.UserPassword == "test" && user.UserName == "test")
            {

                CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
                serializeModel.UserName = user.UserName;
                serializeModel.isAuthenticated = true;

                JavaScriptSerializer serializer = new JavaScriptSerializer();

                string userData = serializer.Serialize(serializeModel);

                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                         1,
                         user.UserName,
                         DateTime.Now,
                         DateTime.Now.AddMinutes(15),
                         false,
                         userData);

                string encTicket = FormsAuthentication.Encrypt(authTicket);
                HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                Response.Cookies.Add(faCookie);

                return RedirectToAction("RSVP", "RSVP");
            }
            else
            {
                ViewBag.FailureMessage = "Username and/or Password not valid.";
                return View();
            }
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            if(User != null)
            {
                User.isAuthenticated = false;
            }

            if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
            {
                Response.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now.AddYears(-1);
                Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
                ViewBag.LogoutMessage = "You have been logged out.";
            }
            else
            {
                ViewBag.LogoutMessage = "You were never logged in.";
            }
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();           

            return View();
        }
        public ActionResult Home()
        {
            if (User != null)
            {
                ViewBag.UserName = User.UserName;
                return View();
            }
            else
            {
                ViewBag.ErrorMessage = "You do not have access to that page.";
                return View("Error");
            }
        }
    }
}