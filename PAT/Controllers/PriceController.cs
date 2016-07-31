using ManageShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageShop.Controllers
{
    public class PriceController : Base
    {
        public ActionResult Index()
        {
            List<TreeViewModel> list = new List<TreeViewModel>();
            PATDBDataContext db = new PATDBDataContext();
            var ls_category = db.Categories.Where(x => x.Status == "A" && x.ID == 11 || x.ID==18 && x.Reftype == "C").ToList();
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
            int CategoryID = 0;
            if (!string.IsNullOrEmpty(categoryID))
            {
                CategoryID = Int32.Parse(categoryID);
            }
            PATDBDataContext db = new PATDBDataContext();
            var item = (from ct in db.Categories
                         join it in db.Items on ct.CategoryCode equals it.CategoryCode
                         where ct.ID == CategoryID && ct.Status=="A" && it.Status=="A"
                         select new { ItemID = it.ID, CategoryID = ct.ID, CategoryCode = ct.CategoryCode }
                    ).FirstOrDefault();
            PriceModel model = new PriceModel();
            if (item != null)
            {
                var price = db.Prices.Where(x => x.ItemID == item.ItemID && x.Status == "A").FirstOrDefault();
                if (price != null)
                {
                    model.ID = price.ID;
                    model.ItemID = price.ItemID;
                    model.Price1 = price.Price1;
                }
                else
                {
                    model.ItemID = item.ItemID;
                }
                
            }
            return View(model);
        }
        public JsonResult ReloadPrice (PriceModel data)
        {
            PATDBDataContext db = new PATDBDataContext();
            PriceModel price = new PriceModel();
            var temp = db.Prices.Where(x => x.ItemID == data.ItemID && x.Status == "A" && x.Type == data.Type).FirstOrDefault();
            if(temp != null)
            {
                price.Price1 = temp.Price1;
            }
            return Json(price);
        }
        public JsonResult SavePrice(PriceModel data)
        {
            Question question = new Question();
            if (data != null)
            {
                PATDBDataContext db = new PATDBDataContext();
                var price = db.Prices.Where(x =>x.ItemID == data.ItemID && x.Status == "A" && x.Type==data.Type).FirstOrDefault();
                if (price == null)
                {
                    Price new_price = new Price();
                    new_price.ItemID = data.ItemID;
                    new_price.Price1 = data.Price1;
                    new_price.Type = data.Type;
                    new_price.CreatedBy = Session["UserName"].ToString();
                    new_price.CreatedDateTime = DateTime.Now;
                    new_price.LastUpdatedBy = Session["UserName"].ToString();
                    new_price.LastUpdatedDateTime = DateTime.Now;
                    new_price.Status = "A";
                    db.Prices.InsertOnSubmit(new_price);
                    db.SubmitChanges();
                    question.Title = "Lưu dữ liệu thành công";
                }
                else
                {
                    price.Price1 = data.Price1;
                    price.LastUpdatedBy = Session["UserName"].ToString();
                    price.LastUpdatedDateTime = DateTime.Now;
                    db.SubmitChanges();
                    question.Title = "Cập nhật dữ liệu thành công";
                }
            }
            else
            {
                question.Title = "Có lỗi trong quá trình xử lý";
            }
            return Json(question);
        }
        public JsonResult SearchPrice(string Language)
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
                    ls_child.NumContent = db.Contents.Where(x => x.Language == Language && x.CategoryID == child.ID && x.Status == "A").Count();
                    var ls_category_child2 = db.Categories.Where(x => x.Status == "A" && x.ParentID == child.ID && x.Reftype == "C").ToList();
                    foreach (var child2 in ls_category_child2)
                    {
                        TreeViewModel ls_child2 = new TreeViewModel();
                        ls_child2.ID = child2.ID;
                        ls_child2.Name = child2.CategoryName;
                        ls_child2.NumContent = db.Contents.Where(x => x.Language == Language && x.CategoryID == child2.ID && x.Status == "A").Count();
                        ls_child.Child.Add(ls_child2);
                    }
                    category.Child.Add(ls_child);
                }
                list.Add(category);
            }
            return Json(list);
        }
    }
}