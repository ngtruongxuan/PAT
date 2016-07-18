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
    public class UserController : Controller
    {
        //
        // GET: /User/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail()
        {
            return View();
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
             var email = db.Users.Where(x => x.Email == model.Email).FirstOrDefault();
            if(email!=null)
            {

                return Json(new Question { Title = "email đã tồn tại", Exist = "1" });
            }

            User user_new = new User();
            user_new.CreatedBy = "tuan_juve";
            user_new.CreatedDateTime = DateTime.Now;
            user_new.Email = model.Email;
            user_new.FullName = model.FullName;
            user_new.GroupID = model.GroupID;
            user_new.LastUpdatedBy = "tuan_juve";
            user_new.LastUpdatedDateTime = DateTime.Now;
            user_new.Password = Encrypt(model.Password);
            user_new.Status = "A";
            user_new.UserName = model.UserName;
           
            db.Users.InsertOnSubmit(user_new);
            db.SubmitChanges();
            var commend = "Lưu dữ liệu thành công";
            var question = new Question { Title = commend };
            return Json(question);
        }
        public class Question
        {
            public string Title { get; set; }
            public string Exist { get; set; }

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