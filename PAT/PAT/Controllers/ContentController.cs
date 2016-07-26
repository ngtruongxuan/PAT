using ManageShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageShop.Controllers
{
    public class ContentController : Base
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
        public ActionResult Detail(string categoryID)
        {
            PATDBDataContext db = new PATDBDataContext();
            int category = 0;
            string language="";
            try
            {
                if (!String.IsNullOrEmpty(categoryID))
                    category = Int32.Parse(categoryID);
                if (String.IsNullOrEmpty(language))
                {
                    language = "VN";
                }
            }
            catch (FormatException e)
            {
                category = 0;
            }
            Content data = db.Contents.Where(x =>x.CategoryID == category && x.Status=="A").FirstOrDefault();
            var categoryName = db.Categories.Where(x => x.ID == category && x.Status == "A").FirstOrDefault();
            ContentModel rep = new ContentModel();
            if(data!= null)
            {
                rep.ID = data.ID;
                rep.CategoryID = data.CategoryID;
                rep.ContentDisplay = data.ContentDisplay;
                rep.ContentName = data.ContentName;
                rep.Language = data.Language;
                if (categoryName != null)
                {
                    rep.CategoryName = categoryName.CategoryName;
                }
            }
            else
            {
                rep.CategoryID = category;
            }
            return View(rep);
        }
        public JsonResult Load(ContentModel model)
        {
            PATDBDataContext db = new PATDBDataContext();
            if (model != null)
            {
                var data = db.Contents.Where(x => x.CategoryID == model.CategoryID && x.Language == model.Language && x.Status == "A").FirstOrDefault();
                if(data!=null)
                    return Json(data);
                else
                    return Json(new { error = "Chưa có dữ liệu" });
            }
            else
            {
                return Json(new {error="Không lấy được dữ liệu" });
            }
                
            
        }
        public JsonResult GetLanguage()
        {
            PATDBDataContext db = new PATDBDataContext();
            var data = db.Languages.Where(x => x.Status == "A").ToList();
            return Json(data);
        }
        public JsonResult SaveContent(ContentModel model)
        {
            PATDBDataContext db = new PATDBDataContext();
           
            if(model.CategoryID != 0 && !String.IsNullOrEmpty(model.ContentDisplay) && !String.IsNullOrEmpty(model.Language))
            {
                var temp = db.Contents.Where(x => x.CategoryID == model.CategoryID && x.Language == model.Language && x.Status == "A").FirstOrDefault();
                if (temp == null)
                {
                    Content ct = new Content();
                    ct.CategoryID = model.CategoryID;
                    ct.ContentDisplay = model.ContentDisplay;
                    ct.ContentName = model.ContentName;
                    ct.CreatedBy = Session["UserName"].ToString();
                    ct.CreatedDateTime = DateTime.Now;
                    ct.LastUpdatedBy = Session["UserName"].ToString();
                    ct.LastUpdatedDateTime = DateTime.Now;
                    ct.Language = model.Language;
                    ct.Remark = model.Remark;
                    ct.Status = "A";
                    db.Contents.InsertOnSubmit(ct);
                    db.SubmitChanges();
                    var commend = "Lưu dữ liệu thành công";
                    var question = new Question { Title = commend };
                    return Json(question);
                }
                else
                {

                    temp.CategoryID = model.CategoryID;
                    temp.ContentDisplay = model.ContentDisplay;
                    temp.ContentName = model.ContentName;
                    temp.LastUpdatedBy = Session["UserName"].ToString();
                    temp.LastUpdatedDateTime = DateTime.Now;
                    temp.Language = model.Language;
                    temp.Remark = model.Remark;

                    db.SubmitChanges();
                    var commend = "Cập nhật dữ liệu thành công";
                    var question = new Question { Title = commend };
                    return Json(question);
                }
                
            }
            else
            {
                var commend = "Có lỗi trong quá trình xử lý";
                var question = new Question { Title = commend };
                return Json(question);
            }
          
        }
        public JsonResult DeleteContent(ContentModel content)
        {
            return Json(new Question { Title = "email đã tồn tại", Exist = "1" });
        }

    }
}