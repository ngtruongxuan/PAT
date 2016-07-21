using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManageShop.Models;
using System.Security.Cryptography;
using System.Text;

namespace ManageShop.Controllers
{
    public class UserController : Base
    {
         [CheckSessionTimeOutAttribute]
        //
        // GET: /User/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail(string UserName)
        {
            PATDBDataContext db = new PATDBDataContext();
            UserDetail model = new UserDetail();
            if(UserName!=null)
            {
                var user = db.Users.Where(x => x.UserName == UserName).FirstOrDefault();

             
                model.UserName = user.UserName;
                model.GroupID = Convert.ToInt16(user.GroupID);
                model.FullName = user.FullName;
                model.Email = user.Email;
                model.ID =Convert.ToInt16(user.ID);
                model.Status = user.Status;
                return View(model);
            }
            else
            {

                model.GroupID = 1;

                return View(model);
            }
          
           
        }

        public JsonResult getGroup()
        {
            PATDBDataContext db = new PATDBDataContext();

            var obj = db.Groups.Where(x=>x.Status=="A").ToList() ;
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public JsonResult saveUser(UserDetail model)
        {

             PATDBDataContext db = new PATDBDataContext();
           
            if(model.ID==0)
            {
                var email = db.Users.Where(x => x.Email == model.Email).FirstOrDefault();
                if (email != null)
                {

                    return Json(new Question { Title = "email đã tồn tại", Exist = "1" });
                }
                User user_new = new User();
                user_new.CreatedBy = Session["UserName"].ToString();
                user_new.CreatedDateTime = DateTime.Now;
                user_new.Email = model.Email;
                user_new.FullName = model.FullName;
                user_new.GroupID = model.GroupID;
                user_new.LastUpdatedBy = Session["UserName"].ToString();
                user_new.LastUpdatedDateTime = DateTime.Now;
                user_new.Password = Encrypt(model.Password);
                user_new.Status = "A";
                user_new.UserName = model.UserName;

                db.Users.InsertOnSubmit(user_new);
                db.SubmitChanges();
                var commend = "Lưu dữ liệu thành công";
                var question = new Question { Title = commend,ID=db.Users.Where(x=>x.Email==model.Email).Select(x=>x.ID).FirstOrDefault() };
                return Json(question);
            }
            else
            {
                var email = db.Users.Where(x => x.Email == model.Email).FirstOrDefault();
                if(email.ID!=model.ID)
                {
                    return Json(new Question { Title = "email đã tồn tại", Exist = "1" });
                }
                User update = db.Users.Where(x => x.ID == model.ID).FirstOrDefault();
                update.Email = model.Email;
                update.FullName = model.FullName;
                update.GroupID = model.GroupID;
                update.LastUpdatedBy = Session["UserName"].ToString();
                update.LastUpdatedDateTime = DateTime.Now;
                if(model.Password!=null)
                {
                    update.Password = Encrypt(model.Password);
                }
                db.SubmitChanges();
                var commend = "Lưu dữ liệu thành công";
                var question = new Question { Title = commend };
                return Json(question);
            }
            
            //db.SubmitChanges();
           
        }
        public JsonResult deleteUser(UserDetail model)
        {
            PATDBDataContext db = new PATDBDataContext();
            User update = db.Users.Where(x => x.ID == model.ID).FirstOrDefault();
            update.LastUpdatedBy = Session["UserName"].ToString();
            update.LastUpdatedDateTime = DateTime.Now;
            update.Status = "I";
            db.SubmitChanges();
            var commend = "Lưu dữ liệu thành công";
            var question = new Question { Title = commend, Success = "1" };
            return Json(question);
        }

        public JsonResult ActiveUser(UserDetail model)
        {
            PATDBDataContext db = new PATDBDataContext();
            User update = db.Users.Where(x => x.ID == model.ID).FirstOrDefault();
            update.LastUpdatedBy = Session["UserName"].ToString();
            update.LastUpdatedDateTime = DateTime.Now;
            update.Status = "A";
            db.SubmitChanges();
            var commend = "Lưu dữ liệu thành công";
            var question = new Question { Title = commend, Success ="1"};
            return Json(question);
        }
        public JsonResult searchUser(UserSearch model)
        {
            PATDBDataContext db = new PATDBDataContext();
            DateTime fromdate = Convert.ToDateTime("01/01/2000");
            DateTime todate = Convert.ToDateTime("01/12/2050");
            if(model.DateFrom!=null)
            {
                   fromdate=  Convert.ToDateTime(model.DateFrom);
            }
            if (model.DateTo != null)
            {
                todate = Convert.ToDateTime(model.DateTo);
            }
            var obj = (from i in db.Users
                       where (i.UserName.Contains(model.UserName) || (model.UserName == null))
                            && (i.FullName.Contains (model.FullName) || model.FullName == null)
                            && (i.Email.Contains(model.Email) || model.Email == null)
                            && (i.GroupID==model.GroupID || model.Email == null)
                            && (i.CreatedDateTime >= fromdate && i.CreatedDateTime <= todate)
                      select i).ToList();

            List<UserResult> l_result = new List<UserResult>();
            foreach (var item in obj)
            {
                UserResult result = new UserResult();
                result.ID = item.ID;
                result.GroupName = db.Groups.Where(x => x.ID == item.GroupID).Select(x => x.GroupName).FirstOrDefault();
                result.GroupID = Convert.ToInt16(item.GroupID);
                result.FullName = item.FullName;
                result.Email = item.Email;
                result.UserName = item.UserName;
                if(item.Status=="A")
                {
                    result.StatusName = "Có hiệu lực";
                }
                else
                {
                    result.StatusName = "Hết hiệu lực";
                }

                l_result.Add(result);
            }

            return Json(l_result, JsonRequestBehavior.AllowGet);
        }
      

        public static string Encrypt(string original)
        {
            return Encrypt(original, "!@#$%^&*()~_+|");
        }

        public static string Encrypt(string original, string key)
        {
            TripleDESCryptoServiceProvider objDESProvider;
            MD5CryptoServiceProvider objHashMD5Provider;
            byte[] keyhash;
            byte[] buffer;
            try
            {
                objHashMD5Provider = new MD5CryptoServiceProvider();
                keyhash = objHashMD5Provider.ComputeHash(UnicodeEncoding.Unicode.GetBytes(key));
                objHashMD5Provider = null;

                objDESProvider = new TripleDESCryptoServiceProvider();
                objDESProvider.Key = keyhash;
                objDESProvider.Mode = CipherMode.ECB;

                buffer = UnicodeEncoding.Unicode.GetBytes(original);
                return Convert.ToBase64String(objDESProvider.CreateEncryptor().TransformFinalBlock(buffer, 0, buffer.Length));
            }
            catch (Exception ex)
            {
                //Utils.WriteLogError("Utils Encrypt ", ex.Message);
                return string.Empty;
            }
        }



        public static string Decrypt(string encrypted)
        {
            TripleDESCryptoServiceProvider objDESProvider;
            MD5CryptoServiceProvider objHashMD5Provider;

            byte[] buffer;

            try
            {
                objHashMD5Provider = new MD5CryptoServiceProvider();
                //  keyhash = objHashMD5Provider.ComputeHash(UnicodeEncoding.Unicode.GetBytes(key));
                objHashMD5Provider = null;

                objDESProvider = new TripleDESCryptoServiceProvider();
                // objDESProvider.Key = keyhash;
                objDESProvider.Mode = CipherMode.ECB;

                buffer = Convert.FromBase64String(encrypted);
                return UnicodeEncoding.Unicode.GetString(objDESProvider.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length));
            }
            catch (Exception ex)
            {
                //Utils.WriteLogError("Utils Decrypt ", ex.Message);
                return string.Empty;
            }
        }
        
	}
}