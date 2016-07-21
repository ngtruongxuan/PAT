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

        public ActionResult Index()
        {
            return View();
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