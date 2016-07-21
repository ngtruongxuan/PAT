using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Web.Routing;
using System.Configuration;
using System.Threading;
using System.Globalization;

using System.Net.Mail;
using System.Net;
using System.Text;
using Microsoft.AspNet.Identity;
namespace ManageShop.Controllers
{
    /// <summary>
    /// taipm create 01042015
    /// </summary>
    [SessionExpireFilter]
    public class PATContronller : Base
    {
        public PATContronller()
        {
            
        }
        //public RedirectToRouteResult BackToLogin(string action, string controller, string parameter)
        //{
        //    //cach viet nay khong dam bao du lieu load dung, tuy nhien trong 1 vai truong hop co the dung
        //    /* string currentpath = MessageManager.GetStringAppConfig("domain") + "/" + controller + "/" + action + parameter;
        //     HttpCookie objCookie = new HttpCookie("hpsURL", currentpath);
        //     objCookie.Expires = DateTime.Now.AddMinutes(10);
        //     Response.Cookies.Add(objCookie);
        //     return RedirectToAction("login", "admin");
        //    */
        //    //cach viet nay gon hon, va lay duoc cac tham so theo get parameter
        //    var prevUrl = Request.UrlReferrer.AbsoluteUri;
        //    return RedirectToAction("login", "Account", new { returnUrl = prevUrl });
        //}

        #region Permission
        public class ActionPermision
        {
            public const string List = "00001"; // Danh sách
            public const string Create = "00002"; // Thêm
            public const string Update = "00004";// Sửa 
            public const string Delete = "00003";// Xóa
            public const string Search = "00005";// Tìm kiếm
            public const string Detail = "00006";// Chi tiết
            public const string Cancel = "00007";// Hủy
            public const string Approve = "00008";// Duyệt
            public const string Deny = "00009";// Từ chối
            public const string Close = "00010";// Đóng
            public const string General = "00011";// Đóng

        }
        /// <summary>
        /// Permission TienNN(PA) 16-04-2015
        /// </summary>
        /// <returns></returns>

        #region Permission List
        //  Get
       // [NonAction]
      
    
       
        #endregion

        protected ActionResult AccessDeniedView()
        {
            return RedirectToAction("AccessDenied", "Role", new { returnUrl = this.Request.RawUrl });
        }
        #endregion
        ///notification
        ///
        public enum NotifyType
        {
            Success,
            Error
        }
        protected virtual void SuccessNotification(string message, bool persistForTheNextRequest = true)
        {
            AddNotification(NotifyType.Success, message, persistForTheNextRequest);
        }
        protected virtual void ErrorNotification(string message, bool persistForTheNextRequest = true)
        {
            AddNotification(NotifyType.Error, message, persistForTheNextRequest);
        }

        protected virtual void AddNotification(NotifyType type, string message, bool persistForTheNextRequest)
        {
            string dataKey = string.Format("notifications.{0}", type);
            if (persistForTheNextRequest)
            {
                if (TempData[dataKey] == null)
                    TempData[dataKey] = new List<string>();
                ((List<string>)TempData[dataKey]).Add(message);
            }
            else
            {
                if (ViewData[dataKey] == null)
                    ViewData[dataKey] = new List<string>();
                ((List<string>)ViewData[dataKey]).Add(message);
            }
        }

        

        #region Email
        //taipm
        public static string domainname = System.Web.Configuration.WebConfigurationManager.AppSettings["DomainName"].ToString();
        public static string Email_UserName = System.Web.Configuration.WebConfigurationManager.AppSettings["Email_UserName"].ToString();
        public static string Email_PassWord = System.Web.Configuration.WebConfigurationManager.AppSettings["Email_PassWord"].ToString();
        public static string Email_EmailSender = System.Web.Configuration.WebConfigurationManager.AppSettings["Email_EmailSender"].ToString();
        public static string Email_SMTP_HOST = System.Web.Configuration.WebConfigurationManager.AppSettings["Email_SMTP_HOST"].ToString();
        public static string Email_PORT = System.Web.Configuration.WebConfigurationManager.AppSettings["Email_PORT"].ToString();

        public void SendMail(string subject, string body, List<MailAddress> listMailTo, List<MailAddress> listMailCC)
        {
            try
            {
                body += "</br></br><u>Lưu ý</u>: Email được gửi tự động từ hệ thống <b>" + domainname.ToString() + "</b>, vui lòng không phản hồi lại email này.";

                string UserName = Email_UserName;
                string PassWord = Email_PassWord;
                string EmailSender = Email_EmailSender;
                string SMTP_HOST = Email_SMTP_HOST;
                string PORT = Email_PORT;
                NetworkCredential loginInfo = new NetworkCredential(UserName, PassWord);
                System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                msg.From = new MailAddress(EmailSender);
                foreach (var item in listMailTo.Select(x => x.Address).Distinct())
                {
                    msg.To.Add(item);
                }
                if (listMailCC != null)
                {
                    foreach (var item in listMailCC.Select(x => x.Address).Distinct())
                    {
                        msg.CC.Add(item);
                    }
                }
                msg.Subject = "SCM - " + subject;
                msg.Body = body;
                msg.IsBodyHtml = true;
                SmtpClient client = new SmtpClient(SMTP_HOST);
                client.Port = int.Parse(PORT);
                client.EnableSsl = false;
                client.UseDefaultCredentials = true;
                client.Credentials = loginInfo;
                client.Send(msg);
                //ControlUtil.AddEmaiLHistory(listMailTo, UserSCM);
                //ControlUtil.AddEmaiLHistory(listMailCC, UserSCM);
              //  logger.Debug("Email Class - Sent Success - Email " + msg.To.ToString());
             }
            catch (SmtpException se)
            {   
               // logger.Error("Email Class - Send Error: " + se.Message);              
            }

        }
        public  void doSendMailWithAttachment(String Subject, String content, List<string> attachmentFilename, bool isFormatHTML, List<MailAddress> listToEmail, List<MailAddress> listCC)
        {
            content += "</br></br><u>Lưu ý</u>: Email được gửi tự động từ hệ thống <b>" + domainname.ToString() + "</b>, vui lòng không phản hồi lại email này.";

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            string UserName = Email_UserName;
            string PassWord = Email_PassWord;
            string EmailSender = Email_EmailSender;
            string SMTP_HOST = Email_SMTP_HOST;
            string PORT = Email_PORT;
            mail.From = new MailAddress(EmailSender);
            NetworkCredential loginInfo = new NetworkCredential(UserName, PassWord);
            foreach (var item in listToEmail.Select(x => x.Address).Distinct())
            {
                mail.To.Add(item);
            }
            if (listCC != null)
            {
                foreach (var item in listCC.Select(x => x.Address).Distinct())
                {
                    mail.CC.Add(item);
                }
            }

            try
            {
                mail.Subject = "SCM - " + Subject;
                mail.Body = content;
                mail.IsBodyHtml = isFormatHTML;
                SmtpClient client = new SmtpClient(SMTP_HOST);
                client.Port = int.Parse(PORT);
                client.EnableSsl = false;
                client.UseDefaultCredentials = true;
                client.Credentials = loginInfo;

                //if (attachmentFilename != null)
                foreach (var url in attachmentFilename)
                {

                  //  string url=System.Web.HttpContext.Current.Server.MapPath("../../Upload/ReceiptItemScan/NK-20150722-0009-HYN/Chrysanthemum.jpg");

                    
                    mail.Attachments.Add(new Attachment(url));
                }
                client.Send(mail);
             //   logger.Debug("Email Class - Sent Success - Email " + mail.To.ToString());
                //ControlUtil.AddEmaiLHistory(listCC, UserSCM);
                //ControlUtil.AddEmaiLHistory(listToEmail, UserSCM);

            }
            catch (SmtpException se)
            {   
               // logger.Error("Email Class - Send Error - Email: " + mail.To.ToString() + " - Error: " + se.Message);              
            }
        }
        #endregion

        #region Question
        public class Question
        {
            public string Title { get; set; }
            public string Exist { get; set; }

        }
        #endregion
    }

    /// <summary>
    /// taipm
    /// Check session login expires
    /// custom
    /// </summary>
    /// 
    public class SessionExpireFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            SessionEntity objE = (SessionEntity)(filterContext.HttpContext.Session["CUSTOMERLOGIN"]);
            if (objE == null)
            {
                string returnurl = filterContext.HttpContext.Request.RawUrl.ToString();
                returnurl = System.Web.HttpUtility.UrlEncode(returnurl);
                //
                //filterContext.Controller.ViewBag.StartupScript = "alert('hello world!');";

                //StringBuilder sb = new StringBuilder();
                //sb.Append("<script type=\"text/javascript\">\n\t");
                //sb.Append("alert('Hello Injection');");
                //sb.Append("</script>\n");
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    //filterContext.HttpContext.Response.Write(sb.ToString());
                    //filterContext.HttpContext.Response.Redirect("/error/404"); 
                    //filterContext.Result = new RedirectResult("~/Account/Login?returnUrl=" + returnurl);

                    filterContext.RequestContext.HttpContext.Response.StatusCode = 401;
                    filterContext.HttpContext.Response.End();
                }
                else
                {
                   // filterContext.Controller.ViewBag.StartupScript = sb.ToString();
                    filterContext.Result = new RedirectResult("~/Account/Login?returnUrl=" + returnurl);
                }
            }
            
            //else
            //{
            //    FunctionService functionService = FunctionService.GetInstance();
            //    string _url ="/"+ filterContext.RouteData.Values["controller"].ToString()+"/"+ filterContext.RouteData.Values["action"].ToString();
            //    var obj = functionService.GetByURL(_url);
            //    if (obj != null)
            //    {   
            //        filterContext.Controller.ViewBag.PageHeader = obj.FunctionName;
            //        filterContext.Controller.ViewBag.Optionaldescription = obj.Remark;
            //    }
            //}
            base.OnActionExecuting(filterContext);
        }

        public class SessionEntity
        {
            //Language Culture: en-US/vi-VN
            public string Language_Culture { get; set; }
            public string UserName { get; set; }

            public int RoleID { get; set; }
            public string Rolename { get; set; }
            public DateTime CreatedDatetimeUserRole { get; set; }
            public string Fullname { get; set; }
            public string Email { get; set; }
            public string ProfileLinkImg { get; set; }
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //SessionEntity objE = (SessionEntity)(filterContext.HttpContext.Session[Constants.CustomerLogin]);
            //if (objE == null)
            //{
            //   // filterContext.Controller.ViewBag.StartupScript = "alert('hello world!');";

            //    StringBuilder sb = new StringBuilder();
            //    sb.Append("<script type=\"text/javascript\">\n\t");
            //    sb.Append("alert('Hello Injection');");
            //    sb.Append("</script>\n");
            //    if (filterContext.HttpContext.Request.IsAjaxRequest())
            //    {
            //        filterContext.HttpContext.Response.Write(sb.ToString());
            //    }
            //    else
            //    {
            //        filterContext.Controller.ViewBag.StartupScript = sb.ToString();
            //    }


            //}
           
        }
    }
    public class Base : Controller
    {
        public ISessionWrapper SessionWrapper { get; set; }
        public bool isLogin { get; set; }
        public string CurrentUsername { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public DateTime CreatedDatetimeUserRole { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        
        public int CurrentRegionID { get; set; }
        public string CurrentRegionName { get; set; }
        public string ProfileLinkImg { get; set; }

        public class Question
        {
            public string Title { get; set; }
            public string Exist { get; set; }
            public string Success { get; set; }
            public long ID { get; set; }

        }
        public Base()
        {
            if (SessionWrapper == null)
            {
                SessionWrapper = new HttpContextSessionWrapper();
            }
            //string lang = "";
            //try
            //{
            //    ManageShop.Controllers.SessionExpireFilterAttribute.SessionEntity objE;
            //    objE = SessionWrapper.GetFromSession<ManageShop.Controllers.SessionExpireFilterAttribute.SessionEntity>("SESSIONGROUP");
            //    if (objE == null)
            //        objE = new ManageShop.Controllers.SessionExpireFilterAttribute.SessionEntity();

            //    if (String.IsNullOrEmpty(objE.Language_Culture))
            //        objE.Language_Culture = "vi-VN";
            //    lang = objE.Language_Culture;
            //    SessionWrapper.SetInSession("CUSTOMERLOGIN", objE);
            //}
            //catch
            //{
            //    lang = "vi-VN";
            //}

            ManageShop.Controllers.SessionExpireFilterAttribute.SessionEntity CustomerLogin = SessionWrapper.GetFromSession<ManageShop.Controllers.SessionExpireFilterAttribute.SessionEntity>("CUSTOMERLOGIN");
            if (CustomerLogin != null)
            {
                isLogin = true;
                CurrentUsername = CustomerLogin.UserName;
                RoleName = CustomerLogin.Rolename;
                RoleID = CustomerLogin.RoleID;
                CreatedDatetimeUserRole = CustomerLogin.CreatedDatetimeUserRole;
                Fullname = CustomerLogin.Fullname;
                Email = CustomerLogin.Email;
                ProfileLinkImg = CustomerLogin.ProfileLinkImg;
            }
            else
            {
                isLogin = false;
               // Redirect();
                
            }
        }

        public ActionResult Redirect()
        {
            return Redirect("/Home/Admin");
        }
        public bool IsUserLogin()
        {
            return isLogin;
        }
    }

    public interface ISessionWrapper
    {
        T GetFromSession<T>(string key);
        void SetInSession(string key, object value);
    }

    public class HttpContextSessionWrapper : ISessionWrapper
    {
        public T GetFromSession<T>(string key)
        {
            return (T)HttpContext.Current.Session[key];
        }

        public void SetInSession(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }
    }
    public class HttpPostOrRedirectAttribute : ActionFilterAttribute
    {
        public string RedirectAction { get; set; }
        public string RedirectController { get; set; }
        public string[] ParametersToPassWithRedirect { get; set; }

        public HttpPostOrRedirectAttribute(string redirectAction)
            : this(redirectAction, null, new string[] { })
        {
        }

        public HttpPostOrRedirectAttribute(string redirectAction, string[] parametersToPassWithRedirect)
            : this(redirectAction, null, parametersToPassWithRedirect)
        {
        }

        public HttpPostOrRedirectAttribute(string redirectAction, string redirectController, string[] parametersToPassWithRedirect)
        {
            RedirectAction = redirectAction;
            RedirectController = redirectController;
            ParametersToPassWithRedirect = parametersToPassWithRedirect;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.HttpMethod == "POST")
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {
                string redirectUrl = GetRedirectUrl(filterContext.RequestContext);
                filterContext.Controller.TempData["Warning"] = "Your action could not be completed as your"
                    + " session had expired.  Please try again.";
                filterContext.Result = new RedirectResult(redirectUrl);
            }
        }

        public string GetRedirectUrl(RequestContext context)
        {
            RouteValueDictionary routeValues = new RouteValueDictionary();
            foreach (string parameter in ParametersToPassWithRedirect)
            {
                if (context.RouteData.Values.ContainsKey(parameter))
                    routeValues.Add(parameter, context.RouteData.Values[parameter]);
            }
            string controller = RedirectController
                ?? context.RouteData.Values["controller"].ToString();
            UrlHelper urlHelper = new UrlHelper(context);
            return urlHelper.Action(RedirectAction, controller, routeValues);
        }
    }
}

public class MessageManager
{
    private static global::System.Resources.ResourceManager _ResourceManager;
    public static global::System.Resources.ResourceManager ResourceManager
    {
        get
        {
            return _ResourceManager;
        }
        set
        {
            _ResourceManager = value;
        }
    }

    public static String GetMessage(String key)
    {
        if (ResourceManager == null)
            return String.Empty;
        else
        {
            String str = ResourceManager.GetString(key);
            return (String.IsNullOrEmpty(str) ? String.Empty : str);
        }
    }
    public static String GetStringAppConfig(String key)
    {
        return ConfigurationManager.AppSettings[key];
    }
    public static String GetMessage2(String key, params Object[] args)
    {
        Object[] args2 = new Object[args.Length];
        String msg = GetMessage(key);
        for (int i = 0; i < args.Length; i++)
        {
            args2[i] = args[i];
        }
        String str = String.Format(msg, args2);
        return str;
    }

    public static String GetMessage(String key, params Object[] args)
    {
        Object[] args2 = new Object[args.Length];
        String msg = GetMessage(key);
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i].GetType() == typeof(String))
                args2[i] = GetMessage((String)args[i]);
            else
                args2[i] = args[i];
            if (args2[i] == null || args2[i].ToString() == String.Empty)
                args2[i] = args[i];

        }
        String str = String.Format(msg, args2);
        return str;
    }

    public static String GetMessage(String key, Object arg1)
    {
        Object[] args = new Object[] { arg1 };
        return GetMessage(key, args);
    }

    public static String GetMessage(String key, Object arg1, Object arg2)
    {
        Object[] args = new Object[] { arg1, arg2 };
        return GetMessage(key, args);
    }

    public static String GetMessage(String key, Object arg1, Object arg2, Object arg3)
    {
        Object[] args = new Object[] { arg1, arg2, arg3 };
        return GetMessage(key, args);
    }
}