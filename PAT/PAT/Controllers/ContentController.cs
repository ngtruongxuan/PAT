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
        public ActionResult Index(string categoryID)
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
            ContentModel rep = new ContentModel();
            if(data!= null)
            {
                rep.ID = data.ID;
                rep.CategoryID = data.CategoryID;
                rep.ContentDisplay = data.ContentDisplay;
                rep.ContentName = data.ContentName;
                rep.Language = data.Language;
            }
            else
            {
                rep.CategoryID = category;
            }
            return View(rep);
        }
        public JsonResult Load(string categoryID, string language)
        {
            PATDBDataContext db = new PATDBDataContext();
            int category = 0;
            if (!String.IsNullOrEmpty(categoryID))
                category = Int32.Parse(categoryID);
            var data = db.Contents.Where(x => x.CategoryID == category && x.Language == language && x.Status == "A").FirstOrDefault();
            return Json(data);
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
           
            if(model.ID==0 && model.CategoryID != 0 && !String.IsNullOrEmpty(model.ContentDisplay) && !String.IsNullOrEmpty(model.Language))
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
                    var commend = "Dữ liệu đã có";
                    var question = new Question { Title = commend };
                    return Json(question);
                }
                
            }
            else if(model.ID != 0 && model.CategoryID != 0 && !String.IsNullOrEmpty(model.ContentDisplay) && !String.IsNullOrEmpty(model.Language))
            {
                var ct = db.Contents.Where(x => x.ID == model.ID && x.CategoryID==model.CategoryID && x.Language==model.Language).FirstOrDefault();
                if (ct != null)
                {
                    ct.CategoryID = model.CategoryID;
                    ct.ContentDisplay = model.ContentDisplay;
                    ct.ContentName = model.ContentName;
                    ct.LastUpdatedBy = Session["UserName"].ToString();
                    ct.LastUpdatedDateTime = DateTime.Now;
                    ct.Language = model.Language;
                    ct.Remark = model.Remark;

                    db.SubmitChanges();
                    var commend = "Cập nhật dữ liệu thành công";
                    var question = new Question { Title = commend };
                    return Json(question);
                }
                else
                {
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