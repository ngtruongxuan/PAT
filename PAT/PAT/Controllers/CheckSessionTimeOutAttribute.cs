using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManageShop.Models;

namespace ManageShop.Controllers
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class CheckSessionTimeOutAttribute : ActionFilterAttribute
    {
        public ISessionWrapper SessionWrapper { get; set; }
        public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            var context = filterContext.HttpContext;
            if (context.Session != null)
            {
                //if (context.Session.IsNewSession)
                //{
                if (SessionWrapper == null)
                {
                    SessionWrapper = new HttpContextSessionWrapper();
                }
               // string sessionCookie = Session["UserName"].ToString();
                      ManageShop.Controllers.SessionExpireFilterAttribute.SessionEntity CustomerLogin = SessionWrapper.GetFromSession<ManageShop.Controllers.SessionExpireFilterAttribute.SessionEntity>("CUSTOMERLOGIN");
                      if (CustomerLogin.UserName == null)
                    {
                        //FormsAuthentication.SignOut();
                        string redirectTo = "~/Home/Index";
                        //if (!string.IsNullOrEmpty(context.Request.RawUrl))
                        //{
                        //    redirectTo = string.Format("~/Home/Login?ReturnUrl={0}", HttpUtility.UrlEncode(context.Request.RawUrl));
                        //}
                        filterContext.HttpContext.Response.Redirect(redirectTo, true);
                    }
                //}
            }
            base.OnActionExecuting(filterContext);
        }
    }
}