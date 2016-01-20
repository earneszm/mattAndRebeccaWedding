using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace MattAndRebeccaWedding.Utilities.CustomAuth
{
    public class CustomPrincipal : ICustomPrincipal
    {
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role) { return false; }

        public CustomPrincipal(string email)
        {
            this.Identity = new GenericIdentity(email);
        }

        public string UserName { get; set; }

        public bool isAuthenticated { get; set; }
        


    }

    public class CustomPrincipalSerializeModel
    {
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public bool isAuthenticated { get; set; }
    }

    public class BaseController : Controller
    {
        protected virtual new CustomPrincipal User
        {
            get { return HttpContext.User as CustomPrincipal; }
        }
    }

    interface ICustomPrincipal : IPrincipal
    {
        string UserName { get; set; }
        bool isAuthenticated { get; set; }
    }

    public abstract class BaseViewPage : WebViewPage
    {
        public virtual new CustomPrincipal User
        {
            get { return base.User as CustomPrincipal; }
        }
    }

    public abstract class BaseViewPage<TModel> : WebViewPage<TModel>
    {
        public virtual new CustomPrincipal User
        {
            get { return base.User as CustomPrincipal; }
        }
    }
}