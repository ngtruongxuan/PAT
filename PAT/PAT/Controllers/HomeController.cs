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

        public ActionResult Index(string lang)
        {
              PATDBDataContext db = new PATDBDataContext();
            CategoryModel model = new CategoryModel();
            if(lang==null)
            {
                lang = "VN";
            }
            List<Category> l_cate = db.Categories.Where(x => x.Status == "A" && x.Language == lang).ToList();
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
            PATDBDataContext db = new PATDBDataContext();
            int ID = 0;
            try
            {
                if(!String.IsNullOrEmpty(id))
                    ID = Int32.Parse(id);
            }
            catch (FormatException e)
            {
                ID = 0;
            }
            Content content = db.Contents.Where(x => x.ID == ID).FirstOrDefault();
            return View(content);
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