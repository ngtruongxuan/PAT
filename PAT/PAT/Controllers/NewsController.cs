﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManageShop.Models;

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
                model.CategoryID = news.CategoryID;
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
    }
}