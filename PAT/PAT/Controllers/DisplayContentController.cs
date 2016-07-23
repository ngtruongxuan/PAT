using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManageShop.Models;

namespace ManageShop.Controllers
{
    public class DisplayContentController : Controller
    {
        //
        // GET: /DisplayContent/
        public ActionResult Index(String CategoryCode)
        {
            PATDBDataContext db = new PATDBDataContext();
            string language = Session["language"].ToString();
            Category cate = db.Categories.Where(x => x.Status == "A" && x.Language == language && x.CategoryCode == CategoryCode).FirstOrDefault();
            DisplayContentModel model = new DisplayContentModel();
            if(cate!=null)
            {
                model.Title = cate.DisplayName;
            }
            else
            {
                model.Title = "None Content";
            }
            return View(model);
        }

        public ActionResult ItemIndex(String CategoryCode)
        {
            PATDBDataContext db = new PATDBDataContext();
            string language = Session["language"].ToString();
            Category cate = db.Categories.Where(x => x.Status == "A" && x.Language == language && x.CategoryCode == CategoryCode).FirstOrDefault();
            DisplayContentModel model = new DisplayContentModel();
            if (cate != null)
            {
                model.Title = cate.DisplayName;
            }
            else
            {
                model.Title = "None Content";
            }
            return View(model);
        }

         public ActionResult MapIndex()
        {
            return View();
        }
	}
}