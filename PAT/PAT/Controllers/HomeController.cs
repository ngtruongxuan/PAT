using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManageShop.Models;

namespace ManageShop.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Admin()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Menu()
        {
            PATDBDataContext db = new PATDBDataContext();

            if(Session["UserName"]==null)
            {
                return Redirect("/");
            }

            User user = db.Users.Where(x => x.UserName == Session["UserName"].ToString()).FirstOrDefault();
            var menu = db.Permissions.Where(x => x.Status == "A" && x.GroupID==user.GroupID ).ToList();
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
    }
}