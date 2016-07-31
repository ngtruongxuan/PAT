using ManageShop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageShop.Controllers
{
    public class PictureController : Base
    {
        public ActionResult Index()
        {
            List<TreeViewModel> list = new List<TreeViewModel>();
            PATDBDataContext db = new PATDBDataContext();
            var ls_category = db.Categories.Where(x => x.Status == "A" && x.ParentID == 0 && x.Reftype == "C").ToList();
            foreach (var item in ls_category)
            {
                TreeViewModel category = new TreeViewModel();
                category.ID = item.ID;
                category.Name = item.CategoryName;
                var ls_category_child = db.Categories.Where(x => x.Status == "A" && x.ParentID == item.ID && x.Reftype == "C").ToList();
                foreach (var child in ls_category_child)
                {
                    TreeViewModel ls_child = new TreeViewModel();
                    ls_child.ID = child.ID;
                    ls_child.Name = child.CategoryName;
                    category.Child.Add(ls_child);
                }
                list.Add(category);
            }
            return View(list);
        }
        public ActionResult UploadImage(HttpPostedFileBase filename)
        {
            try
            {
                if (filename != null && filename.ContentLength > 0)
                {
                    string upFolder = Server.MapPath("~/Upload/CategoryImage/");

                    if (!Directory.Exists(upFolder))
                    {
                        Directory.CreateDirectory(upFolder);
                    }
                    var fileName = Path.GetFileName(filename.FileName);
                    var path = Path.Combine(Server.MapPath("~/Upload/CategoryImage/"), fileName);
                    filename.SaveAs(path);
                    var pathsaveData = "../../Upload/CategoryImage/" + fileName;
                    //_username = this.CurrentUsername;
                    //userService.SaveImage(_username, pathsaveData);
                    //SessionEntity CustomerLogin = SessionWrapper.GetFromSession<SessionEntity>(Constants.CustomerLogin);
                    //if (CustomerLogin != null)
                    //{
                    //    CustomerLogin.ProfileLinkImg = pathsaveData;
                    //    SessionWrapper.SetInSession(Constants.CustomerLogin, CustomerLogin);
                    //}
                    return Json(new { data = pathsaveData, mess = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                    return RedirectToAction("Detail");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Detail");
            }
        }
        public JsonResult SearchPictureLanguage(string Language)
        {
            List<TreeViewModel> list = new List<TreeViewModel>();
            PATDBDataContext db = new PATDBDataContext();
            var ls_category = db.Categories.Where(x => x.Status == "A" && x.ParentID == 0 && x.Reftype == "C").ToList();
            foreach (var item in ls_category)
            {
                TreeViewModel category = new TreeViewModel();
                category.ID = item.ID;
                category.Name = item.CategoryName;
                var ls_category_child = db.Categories.Where(x => x.Status == "A" && x.ParentID == item.ID && x.Reftype == "C").ToList();
                foreach (var child in ls_category_child)
                {
                    TreeViewModel ls_child = new TreeViewModel();
                    ls_child.ID = child.ID;
                    ls_child.Name = child.CategoryName;
                    ls_child.NumContent = db.Images.Where(x =>x.CategoryID == child.ID && x.Status == "A").Count();
                    category.Child.Add(ls_child);
                }
                list.Add(category);
            }
            return Json(list);
        }
        public ActionResult Detail(string categoryID)
        {

            int CategoryID = 0;
            if (!string.IsNullOrEmpty(categoryID))
            {
                CategoryID = Int32.Parse(categoryID);
            }
            PATDBDataContext db = new PATDBDataContext();
            var check = db.Images.Where(x => x.CategoryID == CategoryID && x.Status == "A").FirstOrDefault();
            var category = db.Categories.Where(x => x.ID == CategoryID && x.Status == "A").FirstOrDefault();
            PictureModel pic = new PictureModel();
            if (check != null)
            {
                pic.ID = check.ID;
                pic.CategoryID = check.CategoryID;
                if (category != null)
                {
                    pic.CategoryName = category.CategoryName;
                }
                    
                pic.URL = check.URL;
            }
            else
            {
                if (category != null)
                {
                    pic.CategoryName = category.CategoryName;
                }
                pic.CategoryID = CategoryID;
            }
            return View(pic);
        }
        public JsonResult SavePicture(PictureModel model)
        {
            Question question = new Question();
            if (model != null)
            {
                PATDBDataContext db = new PATDBDataContext();
                var check = db.Images.Where(x => x.CategoryID == model.CategoryID && x.Status == "A").FirstOrDefault();
                if (check == null )
                {
                    if(model.CategoryID != 0)
                    {
                        Image img = new Image();
                    img.CategoryID = model.CategoryID;
                    img.URL = model.URL;
                    img.CreatedBy = Session["UserName"].ToString();
                    img.CreatedDateTime = DateTime.Now;
                    img.LastUpdatedBy = Session["UserName"].ToString();
                    img.LastUpdatedDateTime = DateTime.Now;
                    img.Remark = model.Remark;
                    img.Status = "A";
                    db.Images.InsertOnSubmit(img);
                    db.SubmitChanges();
                    question.Title = "Lưu dữ liệu thành công";
                    }
                    else
                    {
                        question.Title = "Có lỗi trong quá trình xử lý";
                    }
                    
                }
                else
                {
                    check.URL = model.URL;
                    check.LastUpdatedBy = Session["UserName"].ToString();
                    check.LastUpdatedDateTime = DateTime.Now;
                    check.Remark = model.Remark;
                    db.SubmitChanges();
                    question.Title = "Cập nhật dữ liệu thành công";
                }
            }
            else
            {
                question.Title = "Chưa lưu được dữ liệu";
            }
            return Json(question);

        }
    }
}