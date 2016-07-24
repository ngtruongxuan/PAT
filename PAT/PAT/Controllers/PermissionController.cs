using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManageShop.Models;

namespace ManageShop.Controllers
{
    public class PermissionController : Base
    {
        //
        // GET: /Permission/
        public ActionResult Index()
        {
            PATDBDataContext db = new PATDBDataContext();
            if (Session["UserName"] == null)
            {
                return Redirect("/");
            }

            User user = db.Users.Where(x => x.UserName == Session["UserName"].ToString()).FirstOrDefault();

           List< Permission> l_per = db.Permissions.Where(x => x.GroupID == user.GroupID && x.Status == "A").ToList();
           PermissionModel model = new PermissionModel();
         
           foreach (var item in l_per)
           {
               childPermission model_new = new childPermission();
                    model_new.ModuleID = item.ModuleID;
                    
                    model.l_permission.Add(model_new);
           }
           return View(model);
        }

        public JsonResult searchPermission(PermissionModel model)
        {
            PATDBDataContext db = new PATDBDataContext();

            List<Permission> l_per = db.Permissions.Where(x => x.GroupID == model.GroupID && x.Status == "A").ToList();
            List<childPermission> l_child = new List<childPermission>();
            foreach (var item in l_per)
            {
                childPermission child = new childPermission();
                child.ModuleID = item.ModuleID;
                l_child.Add(child);
            }
            return Json(l_child, JsonRequestBehavior.AllowGet);
        }
        public JsonResult setPermission(PermissionModel model)
        {
            PATDBDataContext db = new PATDBDataContext();
            Permission per = db.Permissions.Where(x => x.GroupID == model.GroupID && x.ModuleID==model.ModuleID).FirstOrDefault();
            if(per!=null)
            {
                per.LastUpdatedBy = Session["UserName"].ToString();
                per.LastUpdatedDateTime = DateTime.Now;
                per.URL = db.Modules.Where(x => x.ID == model.ModuleID).Select(x => x.URL).FirstOrDefault();
                per.Status = model.Status;
                db.SubmitChanges();
            }
              else
            {
                Permission new_per = new Permission();
                new_per.CreatedBy = Session["UserName"].ToString();
                new_per.CreatedDateTime = DateTime.Now;
                new_per.DisplayName = db.Modules.Where(x => x.ID == model.ModuleID).Select(x => x.DisplayName).FirstOrDefault();
                new_per.GroupID = model.GroupID;
                new_per.LastUpdatedBy = Session["UserName"].ToString();
                new_per.ModuleID = model.ModuleID;
                new_per.ModuleName = db.Modules.Where(x => x.ID == model.ModuleID).Select(x => x.ModuleName).FirstOrDefault();
                new_per.Status = "A";
                new_per.LastUpdatedDateTime = DateTime.Now;
                new_per.URL = db.Modules.Where(x => x.ID == model.ModuleID).Select(x => x.URL).FirstOrDefault();
                db.Permissions.InsertOnSubmit(new_per);
                db.SubmitChanges();
            }
            var commend = "Lưu dữ liệu thành công";
            var question = new Question { Title = commend, Success = "1" };
            return Json(question);
        }

        public JsonResult setFullPermission(PermissionModel model)
        {
            PATDBDataContext db = new PATDBDataContext();
            for (int i = 1; i <= 9;i++ )
            {
                Permission per = db.Permissions.Where(x => x.GroupID == model.GroupID && x.ModuleID == i).FirstOrDefault();
                if (per != null)
                {
                    per.LastUpdatedBy = Session["UserName"].ToString();
                    per.LastUpdatedDateTime = DateTime.Now;
                    per.URL = db.Modules.Where(x => x.ID == i).Select(x => x.URL).FirstOrDefault();
                    per.ModuleID = i;
                    per.Status = model.Status;
                    db.SubmitChanges();
                }
                else
                {
                    Permission new_per = new Permission();
                    new_per.CreatedBy = Session["UserName"].ToString();
                    new_per.CreatedDateTime = DateTime.Now;
                    new_per.DisplayName = db.Modules.Where(x => x.ID == i).Select(x => x.DisplayName).FirstOrDefault();
                    new_per.GroupID = model.GroupID;
                    new_per.LastUpdatedBy = Session["UserName"].ToString();
                    new_per.ModuleID = i;
                    new_per.ModuleName = db.Modules.Where(x => x.ID == i).Select(x => x.ModuleName).FirstOrDefault();
                    new_per.Status = "A";
                    new_per.LastUpdatedDateTime = DateTime.Now;
                    new_per.URL = db.Modules.Where(x => x.ID == i).Select(x => x.URL).FirstOrDefault();
                    db.Permissions.InsertOnSubmit(new_per);
                    db.SubmitChanges();
                }
            }
           
            var commend = "Lưu dữ liệu thành công";
            var question = new Question { Title = commend, Success = "1" };
            return Json(question);
        }
	}
}