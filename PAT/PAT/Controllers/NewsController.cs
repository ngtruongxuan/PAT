using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManageShop.Models;
using System.IO;

namespace ManageShop.Controllers
{
    public class NewsController:Base
    {
        public ActionResult Index()
        {
            List<TreeViewModel> list = new List<TreeViewModel>();
            PATDBDataContext db = new PATDBDataContext();
            var ls_category = db.Categories.Where(x => x.Status == "A" && x.ParentID == 0 && x.Reftype == "I").ToList();
            foreach (var item in ls_category)
            {
                TreeViewModel category = new TreeViewModel();
                category.ID = item.ID;
                category.Name = item.CategoryName;
                var ls_category_child = db.Categories.Where(x => x.Status == "A" && x.ParentID == item.ID && x.Reftype == "I").ToList();
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
                    string upFolder = Server.MapPath("~/Upload/NewsImage/");

                    if (!Directory.Exists(upFolder))
                    {
                        Directory.CreateDirectory(upFolder);
                    }
                    var fileName =  Path.GetFileName(filename.FileName);
                    var path = Path.Combine(Server.MapPath("~/Upload/NewsImage/"), fileName);
                    filename.SaveAs(path);
                    var pathsaveData = "../../Upload/NewsImage/" + fileName;
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
                    return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult Delete(NewsModel model)
        {
            PATDBDataContext db = new PATDBDataContext();
            Question question = new Question();
            if (model != null)
            {
                var news = db.News.Where(x => x.ID == model.ID && x.Status == "A").FirstOrDefault();
                news.LastUpdatedBy = Session["UserName"].ToString();
                news.LastUpdatedDateTime = DateTime.Now;
                news.Status = "I";
                db.SubmitChanges();
                question.Title = "Đã xóa tin tức";
                return Json(question);
            }
            question.Title = "Chưa xóa được tin tức";
            return Json(question);
            
        }
        public ActionResult GetCategory()
        {
            PATDBDataContext db = new PATDBDataContext();
            var ls_category = db.Categories.Where(x => x.Status == "A" && x.ParentID==31).ToList();
            return Json(ls_category);
        }
        public ActionResult Detail(string newsID)
        {
            PATDBDataContext db = new PATDBDataContext();
            NewsModel model = new NewsModel();
            int ID = 0;
            try
            {
                if (!String.IsNullOrEmpty(newsID))
                    ID = Int32.Parse(newsID);
            }
            catch (FormatException e)
            {
                ID = 0;
            }
            var news = db.News.Where(x => x.ID == ID && x.Status == "A").FirstOrDefault();
            if (news != null)
            {
                model.CategoryID = (int)news.CategoryID;
                var categroy = db.Categories.Where(x => x.ID == news.CategoryID && x.Status == "A").FirstOrDefault();
                if (categroy != null)
                {
                    model.CategoryName = categroy.CategoryName;
                }
                model.ID = news.ID;
                model.NewsContent = news.NewsContent;
                model.NewsDecription = news.NewsDecription;
                model.NewsName = news.NewsName;
                model.Status = news.Status;
                model.Language = news.Language;
                model.Image = news.Image;
            }
            return View(model);
        }
        public JsonResult SaveNews(NewsModel data)
        {
            PATDBDataContext db = new PATDBDataContext();
            var question = new Question ();
            if (data != null)
            {
                if (data.NewsName != null && data.NewsDecription != null && data.NewsContent != null && data.Language != null && data.CategoryID != 0 && data.Image != null)
                {
                    if (data.ID == 0)
                    {
                        New news = new New();
                        news.CategoryID = data.CategoryID;
                        news.Image = data.Image;
                        news.Language = data.Language;
                        news.NewsContent = data.NewsContent;
                        news.NewsDecription = data.NewsDecription;
                        news.NewsName = data.NewsName;
                        news.NewsDate = DateTime.Now;
                        news.CreatedBy = Session["UserName"].ToString();
                        news.CreatedDateTime = DateTime.Now;
                        news.LastUpdatedBy = Session["UserName"].ToString();
                        news.LastUpdatedDateTime = DateTime.Now;
                        news.Status = "A";
                        db.News.InsertOnSubmit(news);
                        db.SubmitChanges();
                        question.Title=  "Lưu dữ liệu thành công";
                        return Json(question);
                    }
                    else
                    {
                        var news = db.News.Where(x => x.ID == data.ID && x.Status == "A").FirstOrDefault();
                        if (news != null)
                        {
                            news.CategoryID = data.CategoryID;
                            news.Image = data.Image;
                            news.Language = data.Language;
                            news.NewsContent = data.NewsContent;
                            news.NewsDecription = data.NewsDecription;
                            news.NewsName = data.NewsName;
                            news.LastUpdatedBy = Session["UserName"].ToString();
                            news.LastUpdatedDateTime = DateTime.Now;
                            db.SubmitChanges();
                            question.Title = "Cập nhật dữ liệu thành công";
                            return Json(question);
                        }
                    }
                }
            }
            question.Title = "Có lỗi trong quá trình xử lý";
            return Json(question);
        }
        public JsonResult SearchNews(string CategoryID)
        {
            int ID = 0;
            try
            {
                if (!String.IsNullOrEmpty(CategoryID))
                    ID = Int32.Parse(CategoryID);
            }
            catch (FormatException e)
            {
                ID = 0;
            }
            List<New> data = new List<New>();
            PATDBDataContext db = new PATDBDataContext();
            List<NewsModel> ls_News = new List<NewsModel>();
            if (ID != 0)
            {
                data = db.News.Where(x => x.CategoryID == ID && x.Status == "A").ToList();
            }
            else
            {
                data = db.News.Where(x => x.Status == "A").ToList();
            }
            
            if (data != null)
            {
                foreach (var item in data)
                {
                    NewsModel news = new NewsModel();
                    var categroy = db.Categories.Where(x => x.ID == item.CategoryID && x.Status == "A").FirstOrDefault();
                    if (categroy != null)
                    {
                        news.CategoryName = categroy.CategoryName;
                    }
                    news.ID = item.ID;
                    news.CategoryID = (int)item.CategoryID;
                    news.NewsName = item.NewsName;
                    news.NewsContent = item.NewsContent;
                    news.NewsDate = item.NewsDate;
                    news.NewsDecription = item.NewsDecription;
                    news.Language = item.Language;
                    news.CreatedBy = item.CreatedBy;
                    news.CreatedDateTime = item.CreatedDateTime;
                    var language = db.Languages.Where(x => x.LanguageCode == item.Language && x.Status == "A").FirstOrDefault();
                    if (language != null)
                        news.LanguageName = language.LanguageName;
                    ls_News.Add(news);
                }
            }
            return Json(ls_News);
        }
    }
}