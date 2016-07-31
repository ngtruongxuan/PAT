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
        public JsonResult searchCategoryLanguage(string Language)
        {
            PATDBDataContext db = new PATDBDataContext();
            CategoryModel model = new CategoryModel();
            if (Language == null || Language == "")
            {
                Language = "VN";
            }
            Session["Language"] = Language;
            List<Category> l_cate_vn = db.Categories.Where(x => x.Status == "A" && x.Language == "VN").ToList();
            List<Category> l_cate = db.Categories.Where(x => x.Status == "A" && x.Language == Language).ToList();
            //gioi thieu
            model.in_company = l_cate.Where(x => x.CategoryCode == "in_company").Select(x => x.DisplayName).FirstOrDefault();
            model.in_environment = l_cate.Where(x => x.CategoryCode == "in_environment").Select(x => x.DisplayName).FirstOrDefault();
            model.in_industry = l_cate.Where(x => x.CategoryCode == "in_industry").Select(x => x.DisplayName).FirstOrDefault();
            model.in_land = l_cate.Where(x => x.CategoryCode == "in_land").Select(x => x.DisplayName).FirstOrDefault();
            model.in_location = l_cate.Where(x => x.CategoryCode == "in_location").Select(x => x.DisplayName).FirstOrDefault();
            model.in_map = l_cate.Where(x => x.CategoryCode == "in_map").Select(x => x.DisplayName).FirstOrDefault();
            //loi ich nha dau tu
            model.co_cost = l_cate.Where(x => x.CategoryCode == "co_cost").Select(x => x.DisplayName).FirstOrDefault();
            model.co_develop = l_cate.Where(x => x.CategoryCode == "co_develop").Select(x => x.DisplayName).FirstOrDefault();
            model.co_savecost = l_cate.Where(x => x.CategoryCode == "co_savecost").Select(x => x.DisplayName).FirstOrDefault();
            model.co_savetime = l_cate.Where(x => x.CategoryCode == "co_savetime").Select(x => x.DisplayName).FirstOrDefault();
            //khu cong nghiep
            model.fa_factory = l_cate.Where(x => x.CategoryCode == "fa_factory").Select(x => x.DisplayName).FirstOrDefault();
            model.fa_land = l_cate.Where(x => x.CategoryCode == "fa_land").Select(x => x.DisplayName).FirstOrDefault();
            // thu vien thong tin
            model.ne_law = l_cate.Where(x => x.CategoryCode == "ne_law").Select(x => x.DisplayName).FirstOrDefault();
            model.ne_pat = l_cate.Where(x => x.CategoryCode == "ne_pat").Select(x => x.DisplayName).FirstOrDefault();
            model.ne_picture = l_cate.Where(x => x.CategoryCode == "ne_picture").Select(x => x.DisplayName).FirstOrDefault();
            model.ne_recruit = l_cate.Where(x => x.CategoryCode == "ne_recruit").Select(x => x.DisplayName).FirstOrDefault();
            model.ne_video = l_cate.Where(x => x.CategoryCode == "ne_video").Select(x => x.DisplayName).FirstOrDefault();
            //khu dan cu
            model.re_flathouse = l_cate.Where(x => x.CategoryCode == "re_flathouse").Select(x => x.DisplayName).FirstOrDefault();
            model.re_gardenhouse = l_cate.Where(x => x.CategoryCode == "re_gardenhouse").Select(x => x.DisplayName).FirstOrDefault();
            model.re_house1 = l_cate.Where(x => x.CategoryCode == "re_house1").Select(x => x.DisplayName).FirstOrDefault();
            model.re_house2 = l_cate.Where(x => x.CategoryCode == "re_house2").Select(x => x.DisplayName).FirstOrDefault();
            model.re_nexthouse = l_cate.Where(x => x.CategoryCode == "re_nexthouse").Select(x => x.DisplayName).FirstOrDefault();
            model.re_workerhouse = l_cate.Where(x => x.CategoryCode == "re_workerhouse").Select(x => x.DisplayName).FirstOrDefault();
            //LIEN HE
            model.contact_in = l_cate.Where(x => x.CategoryCode == "contact_in").Select(x => x.DisplayName).FirstOrDefault();
            //ha tang tien ich
            model.sy_electric = l_cate.Where(x => x.CategoryCode == "sy_electric").Select(x => x.DisplayName).FirstOrDefault();
            model.sy_water = l_cate.Where(x => x.CategoryCode == "sy_water").Select(x => x.DisplayName).FirstOrDefault();
            model.sy_pollution = l_cate.Where(x => x.CategoryCode == "sy_pollution").Select(x => x.DisplayName).FirstOrDefault();
            model.sy_garbage = l_cate.Where(x => x.CategoryCode == "sy_garbage").Select(x => x.DisplayName).FirstOrDefault();

            //gioi thieu
            if (model.in_company == null)
                model.in_company = l_cate_vn.Where(x => x.CategoryCode == "in_company").Select(x => x.DisplayName).FirstOrDefault();
            if (model.in_environment == null)
                model.in_environment = l_cate_vn.Where(x => x.CategoryCode == "in_environment").Select(x => x.DisplayName).FirstOrDefault();
            if (model.in_industry == null)
                model.in_industry = l_cate_vn.Where(x => x.CategoryCode == "in_industry").Select(x => x.DisplayName).FirstOrDefault();
            if (model.in_land == null)
                model.in_land = l_cate_vn.Where(x => x.CategoryCode == "in_land").Select(x => x.DisplayName).FirstOrDefault();
            if (model.in_location == null)
                model.in_location = l_cate_vn.Where(x => x.CategoryCode == "in_location").Select(x => x.DisplayName).FirstOrDefault();
            if (model.in_map == null)
                model.in_map = l_cate_vn.Where(x => x.CategoryCode == "in_map").Select(x => x.DisplayName).FirstOrDefault();
            //loi ich nha dau tu
            if (model.co_cost == null)
                model.co_cost = l_cate_vn.Where(x => x.CategoryCode == "co_cost").Select(x => x.DisplayName).FirstOrDefault();
            if (model.co_develop == null)
                model.co_develop = l_cate_vn.Where(x => x.CategoryCode == "co_develop").Select(x => x.DisplayName).FirstOrDefault();
            if (model.co_savecost == null)
                model.co_savecost = l_cate_vn.Where(x => x.CategoryCode == "co_savecost").Select(x => x.DisplayName).FirstOrDefault();
            if (model.co_savetime == null)
                model.co_savetime = l_cate_vn.Where(x => x.CategoryCode == "co_savetime").Select(x => x.DisplayName).FirstOrDefault();
            //khu cong nghiep
            if (model.fa_factory == null)
                model.fa_factory = l_cate_vn.Where(x => x.CategoryCode == "fa_factory").Select(x => x.DisplayName).FirstOrDefault();
            if (model.fa_land == null)
                model.fa_land = l_cate_vn.Where(x => x.CategoryCode == "fa_land").Select(x => x.DisplayName).FirstOrDefault();
            // thu vien thong tin
            if (model.ne_law == null)
                model.ne_law = l_cate_vn.Where(x => x.CategoryCode == "ne_law").Select(x => x.DisplayName).FirstOrDefault();
            if (model.ne_pat == null)
                model.ne_pat = l_cate_vn.Where(x => x.CategoryCode == "ne_pat").Select(x => x.DisplayName).FirstOrDefault();
            if (model.ne_picture == null)
                model.ne_picture = l_cate_vn.Where(x => x.CategoryCode == "ne_picture").Select(x => x.DisplayName).FirstOrDefault();
            if (model.ne_recruit == null)
                model.ne_recruit = l_cate_vn.Where(x => x.CategoryCode == "ne_recruit").Select(x => x.DisplayName).FirstOrDefault();
            if (model.ne_video == null)
                model.ne_video = l_cate_vn.Where(x => x.CategoryCode == "ne_video").Select(x => x.DisplayName).FirstOrDefault();
            //khu dan cu
            if (model.re_flathouse == null)
                model.re_flathouse = l_cate_vn.Where(x => x.CategoryCode == "re_flathouse").Select(x => x.DisplayName).FirstOrDefault();
            if (model.re_gardenhouse == null)
                model.re_gardenhouse = l_cate_vn.Where(x => x.CategoryCode == "re_gardenhouse").Select(x => x.DisplayName).FirstOrDefault();
            if (model.re_house1 == null)
                model.re_house1 = l_cate_vn.Where(x => x.CategoryCode == "re_house1").Select(x => x.DisplayName).FirstOrDefault();
            if (model.re_house2 == null)
                model.re_house2 = l_cate_vn.Where(x => x.CategoryCode == "re_house2").Select(x => x.DisplayName).FirstOrDefault();
            if (model.re_nexthouse == null)
                model.re_nexthouse = l_cate_vn.Where(x => x.CategoryCode == "re_nexthouse").Select(x => x.DisplayName).FirstOrDefault();
            if (model.re_workerhouse == null)
                model.re_workerhouse = l_cate_vn.Where(x => x.CategoryCode == "re_workerhouse").Select(x => x.DisplayName).FirstOrDefault();
            //LIEN HE
            if (model.contact_in == null)
                model.contact_in = l_cate_vn.Where(x => x.CategoryCode == "contact_in").Select(x => x.DisplayName).FirstOrDefault();
            //ha tang tien ich
            if (model.sy_electric == null)
                model.sy_electric = l_cate_vn.Where(x => x.CategoryCode == "sy_electric").Select(x => x.DisplayName).FirstOrDefault();
            if (model.sy_water == null)
                model.sy_water = l_cate_vn.Where(x => x.CategoryCode == "sy_water").Select(x => x.DisplayName).FirstOrDefault();
            if (model.sy_pollution == null)
                model.sy_pollution = l_cate_vn.Where(x => x.CategoryCode == "sy_pollution").Select(x => x.DisplayName).FirstOrDefault();
            if (model.sy_garbage == null)
                model.sy_garbage = l_cate_vn.Where(x => x.CategoryCode == "sy_garbage").Select(x => x.DisplayName).FirstOrDefault();
            model.infastructure_l = db.MenuPictures.Where(x => x.MenuCode == "infastructure_l" && x.Language==Language).Select(x=>x.URL).FirstOrDefault();
            model.menu_info = db.MenuPictures.Where(x => x.MenuCode == "menu_info" && x.Language == Language).Select(x => x.URL).FirstOrDefault();
            model.menu_resident = db.MenuPictures.Where(x => x.MenuCode == "menu_resident" && x.Language == Language).Select(x => x.URL).FirstOrDefault();
            model.menu_industry = db.MenuPictures.Where(x => x.MenuCode == "menu_industry" && x.Language == Language).Select(x => x.URL).FirstOrDefault();
            model.menu_infrastructure = db.MenuPictures.Where(x => x.MenuCode == "menu_infrastructure" && x.Language == Language).Select(x => x.URL).FirstOrDefault();
            model.menu_investor = db.MenuPictures.Where(x => x.MenuCode == "menu_investor" && x.Language == Language).Select(x => x.URL).FirstOrDefault();
            model.menu_library = db.MenuPictures.Where(x => x.MenuCode == "menu_library" && x.Language == Language).Select(x => x.URL).FirstOrDefault();
            model.menu_contact = db.MenuPictures.Where(x => x.MenuCode == "menu_contact" && x.Language == Language).Select(x => x.URL).FirstOrDefault();
            model.info = db.MenuPictures.Where(x => x.MenuCode == "info" && x.Language == Language).Select(x => x.URL).FirstOrDefault();
            model.resident = db.MenuPictures.Where(x => x.MenuCode == "resident" && x.Language == Language).Select(x => x.URL).FirstOrDefault();
            model.industry = db.MenuPictures.Where(x => x.MenuCode == "industry" && x.Language == Language).Select(x => x.URL).FirstOrDefault();
            model.library = db.MenuPictures.Where(x => x.MenuCode == "library" && x.Language == Language).Select(x => x.URL).FirstOrDefault();
            model.investor = db.MenuPictures.Where(x => x.MenuCode == "investor" && x.Language == Language).Select(x => x.URL).FirstOrDefault();
            model.contact = db.MenuPictures.Where(x => x.MenuCode == "contact" && x.Language == Language).Select(x => x.URL).FirstOrDefault();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

	}
}