using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManageShop.Models;

namespace ManageShop.Controllers
{
   
    public class HomeController : Base
    {

        public ActionResult Index(string Language)
        {
              PATDBDataContext db = new PATDBDataContext();
            CategoryModel model = new CategoryModel();
            if (Language == null || Language == "")
            {
                Language = "VN";
            }
             Session["Language"]=Language;
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
            if (model.in_company==null)
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
            return View(model);
        }

        public ActionResult Admin()
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

        public ActionResult CreateContent(string id)
        {

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [CheckSessionTimeOutAttribute]
        [ChildActionOnly]
        public ActionResult Menu()
        {
            PATDBDataContext db = new PATDBDataContext();

            if(Session["UserName"]==null)
            {
                return Redirect("/");
            }

            User user = db.Users.Where(x => x.UserName == Session["UserName"].ToString()).FirstOrDefault();
            var menu = db.Permissions.Where(x => x.Status == "A" && x.GroupID == user.GroupID).ToList().OrderBy(x => x.ModuleID);
           
                      
            List<MenuItem> l_menu_item = new List<MenuItem>();
            MenuModel menu_model = new MenuModel();
            foreach (var item in menu)
            {
                MenuItem m_item = new MenuItem();
                m_item.MenuName = item.DisplayName;
                m_item.URL = item.URL;
                m_item.AWE = db.Modules.Where(x => x.ID == item.ModuleID).Select(x => x.Reftype).FirstOrDefault();

                menu_model.MenuItems.Add(m_item);
            }

            return this.PartialView("_Menu", menu_model);


        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Logout(string Username)
        {

            Session["UserName"] = null;
            ManageShop.Controllers.SessionExpireFilterAttribute.SessionEntity ssE = new ManageShop.Controllers.SessionExpireFilterAttribute.SessionEntity();
            ssE.UserName = null;
            SessionWrapper.SetInSession("CUSTOMERLOGIN", ssE);
            string logout = "logout";
            return Json(logout);




        }
    }
}