using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManageShop.Models;

namespace ManageShop.Controllers
{
    public class CategoryController : Controller
    {
        //
        // GET: /Category/
        public ActionResult Index()
        {
            List<CategoryTreeModel> list = new List<CategoryTreeModel>();
            PATDBDataContext db = new PATDBDataContext();
            var ls_category = db.Categories.Where(x => x.Status == "A" && x.ParentID == 0 && x.Language=="VN").ToList();
            foreach (var item in ls_category)
            {
                CategoryTreeModel category = new CategoryTreeModel();
                category.ID = item.ID;
                category.Name = item.CategoryName;
                var ls_category_child = db.Categories.Where(x => x.Status == "A" && x.ParentID == item.ID && x.Language == "VN").ToList();
                foreach (var child in ls_category_child)
                {
                    CategoryTreeModel ls_child = new CategoryTreeModel();
                    ls_child.ID = child.ID;
                    ls_child.Name = child.CategoryName;
                    ls_child.CategoryCode = child.CategoryCode;
                    category.Child.Add(ls_child);
                }
                list.Add(category);
            }
            return View(list);
        }

        public JsonResult searchCategory(string Language)
        {
            PATDBDataContext db = new PATDBDataContext();
           
            CategoryModel model = new CategoryModel();
           

            List<Category> l_cate = db.Categories.Where(x => x.Status == "A" && x.Language == Language&&x.CategoryCode!=null).ToList();
            model.l_code = new List<CategoryCodeModel>();
            foreach (var item in l_cate)
            {
                CategoryCodeModel newcate = new CategoryCodeModel();
                newcate.CategoryCode = item.CategoryCode;
                model.l_code.Add(newcate);
            }

            List<Category> l_cate_vn = db.Categories.Where(x => x.Status == "A" && x.Language == "VN" && x.CategoryCode != null).ToList();
            model.l_code_new = new List<CategoryCodeModel>();
            foreach (var item in l_cate_vn)
            {
                CategoryCodeModel newcate = new CategoryCodeModel();
                newcate.CategoryCode = item.CategoryCode;
                model.l_code_new.Add(newcate);
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Detail(string language, string CategoryCode)
        {
            PATDBDataContext db = new PATDBDataContext();

            CategoryPopupModel model = new CategoryPopupModel();
            model.Language = db.Languages.Where(x => x.LanguageCode == language).Select(x => x.LanguageName).FirstOrDefault();
            model.LanguageVN = db.Categories.Where(x => x.Language == "VN" && x.CategoryCode == CategoryCode).Select(x => x.DisplayName).FirstOrDefault();
            model.LanguageCurent = db.Categories.Where(x => x.Language == language && x.CategoryCode == CategoryCode).Select(x => x.DisplayName).FirstOrDefault();
            model.CategoryCode = CategoryCode;
            model.LanguageCode = language;
            if (model.LanguageCurent == null)
                model.LanguageCurent = "Chưa có nội dung";
            return View(model);
        }

        public JsonResult saveCategory(CategoryPopupModel model)
        {
            PATDBDataContext db = new PATDBDataContext();
            Category update = db.Categories.Where(x => x.Language == model.LanguageCode && x.CategoryCode == model.CategoryCode).FirstOrDefault();
            if (update != null)
            {
                update.LastUpdatedBy = Session["UserName"].ToString();
                update.LastUpdatedDateTime = DateTime.Now;
                update.DisplayName = model.CategoryContent;
                db.SubmitChanges();
            }
            else
            {
                Category editnew = new Category();
                editnew.CategoryCode = model.CategoryCode;
                editnew.CategoryName = db.Categories.Where(x => x.Language == "VN" && x.CategoryCode == model.CategoryCode).Select(x=>x.CategoryName).FirstOrDefault();
                editnew.DisplayName = model.CategoryContent;
                editnew.Language = model.Language;
                editnew.ParentID = db.Categories.Where(x => x.Language == "VN" && x.CategoryCode == model.CategoryCode).Select(x => x.ParentID).FirstOrDefault();
                editnew.Type = "M";
                editnew.CreatedBy = Session["UserName"].ToString();
                editnew.CreatedDateTime = DateTime.Now;
                editnew.Language = model.LanguageCode;
                editnew.LastUpdatedBy = Session["UserName"].ToString();
                editnew.LastUpdatedDateTime = DateTime.Now;
                editnew.Status = "A";
                db.Categories.InsertOnSubmit(editnew);
                db.SubmitChanges();

            }
            var commend = "Lưu dữ liệu thành công";
            var question = new ManageShop.Controllers.Base.Question { Title = commend, Success = "1" };
            return Json(question);
        }
	}
}