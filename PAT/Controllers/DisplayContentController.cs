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
        public ActionResult Index(String CategoryCode, string Language)
        {
            PATDBDataContext db = new PATDBDataContext();
            if (Language == null || Language == "")
            {
                Language = "VN";
            }
            Category cate = db.Categories.Where(x => x.Status == "A" && x.Language == Language && x.CategoryCode == CategoryCode).FirstOrDefault();
            if (cate == null)
            {
                cate = db.Categories.Where(x => x.Status == "A" && x.Language == "VN" && x.CategoryCode == CategoryCode).FirstOrDefault();
            }
            DisplayContentModel model = new DisplayContentModel();
            if(cate!=null)
            {
                model.Title = cate.DisplayName;
                model.Content = db.Contents.Where(x => x.CategoryID == cate.ID && x.Language == Language).Select(x => x.ContentDisplay).FirstOrDefault();
            }
            else
            {
                model.Title = "None Content";
            }
          

            return View(model);
        }
        public ActionResult NewsIndex(String CategoryCode, string Language)
        {
            if (Language == null || Language == "")
            {
                Language = "VN";
            }
            PATDBDataContext db = new PATDBDataContext();
            Category cate = db.Categories.Where(x => x.Status == "A" && x.Language == Language && x.CategoryCode == CategoryCode).FirstOrDefault();
            if (cate == null)
            {
                cate = db.Categories.Where(x => x.Status == "A" && x.Language == "VN" && x.CategoryCode == CategoryCode).FirstOrDefault();
            }
            DisplayNewsModel model = new DisplayNewsModel();
            if (cate != null)
            {
                model.categoryName = cate.CategoryName;
                List<New> l_news = db.News.Where(x => x.CategoryID == cate.ID && x.Status == "A").ToList();
                foreach (var item in l_news)
                {
                        DisplayNewsModel model_child = new DisplayNewsModel();
                        model_child.newsID = item.ID.ToString();
                        model_child.img = item.Image;
                        model_child.title = item.NewsName;
                        model_child.description = item.NewsDecription;
                        model_child.date = item.NewsDate.ToString();
                        model.l_news.Add(model_child);
                }
            }
            return View(model);
        }

        public ActionResult NewsDetail(long newsID, string Language)
        {
            if (Language == null || Language == "")
            {
                Language = "VN";
            }
            PATDBDataContext db = new PATDBDataContext();
            New cate = db.News.Where(x => x.Status == "A" && x.Language == Language && x.ID == newsID).FirstOrDefault();
            if (cate == null)
            {
                cate = db.News.Where(x => x.Status == "A" && x.Language == "VN" && x.ID == newsID).FirstOrDefault();
            }
            DisplayNewsModel model = new DisplayNewsModel();
            if (cate != null)
            {
                model.categoryName = db.Categories.Where(x=>x.ID==cate.CategoryID).Select(x=>x.CategoryName).FirstOrDefault();
                model.content = cate.NewsContent;
            }
            return View(model);
        }
        public ActionResult ItemIndex(String CategoryCode, string Language)
        {
            if (Language == null || Language == "")
            {
                Language = "VN";
            }
            PATDBDataContext db = new PATDBDataContext();

            Category cate = db.Categories.Where(x => x.Status == "A" && x.Language == Language && x.CategoryCode == CategoryCode).FirstOrDefault();
            if(cate==null)
            {
                cate = db.Categories.Where(x => x.Status == "A" && x.Language == "VN" && x.CategoryCode == CategoryCode).FirstOrDefault();
            }
            DisplayContentModel model = new DisplayContentModel();
            if (cate != null)
            {
                model.Title = cate.DisplayName;
                List<Category> l_cate_child = db.Categories.Where(x => x.ParentID == cate.ID && x.Status == "A").ToList();
                foreach (var item in l_cate_child)
                {
                     if(item.CategoryCode=="sell")
                     {
                         model.Sell_content = db.Contents.Where(x => x.CategoryID == item.ID && x.Language == Language).Select(x => x.ContentDisplay).FirstOrDefault();
                         model.Sell_language = languageTab(Language, "sell");
                     }
                     else if (item.CategoryCode == "transfer")
                     {
                         model.Sell_content = db.Contents.Where(x => x.CategoryID == item.ID && x.Language == Language).Select(x => x.ContentDisplay).FirstOrDefault();
                         model.Sell_language = languageTab(Language, "transfer");
                     }
                     else if (item.CategoryCode == "rent")
                     {
                         model.Rent_language = languageTab(Language, "rent");
                         model.Rent_content = db.Contents.Where(x => x.CategoryID == item.ID && x.Language == Language).Select(x => x.ContentDisplay).FirstOrDefault();
                     }
                }
              //  model.Content = db.Contents.Where(x => x.CategoryID == cate.ID && x.Language == Language).Select(x => x.ContentDisplay).FirstOrDefault();
            }
            else
            {
                model.Title = "None Content";
            }
            model.contact = languageTab(Language, "contact");
            return View(model);
        }
        public String languageTab(string language, string content)
        {
            string result = "";
             if(content=="sell")
             {
                 if(language=="VN")
                 {
                     result = "BÁN";
                 }  
                 else if(language=="EN")
                 {
                     result = "SELL";
                 }
                 else if (language == "CN")
                 {
                     result = "卖";
                 }
                 else if (language == "JP")
                 {
                     result = "販売";
                 }
                 else if (language == "KR")
                 {
                     result = "판매하다";
                 }
             }
             else if (content == "transfer")
             {
                 if (language == "VN")
                 {
                     result = "CHUYỂN NHƯỢNG";
                 }
                 else if (language == "EN")
                 {
                     result = "TRANSFER";
                 }
                 else if (language == "CN")
                 {
                     result = "转让";
                 }
                 else if (language == "JP")
                 {
                     result = "譲渡";
                 }
                 else if (language == "KR")
                 {
                     result = "양도하다";
                 }
             }
             else if (content == "rent")
             {
                 if (language == "VN")
                 {
                     result = "CHO THUÊ";
                 }
                 else if (language == "EN")
                 {
                     result = "RENT";
                 }
                 else if (language == "CN")
                 {
                     result = "出租";
                 }
                 else if (language == "JP")
                 {
                     result = "貸し";
                 }
                 else if (language == "KR")
                 {
                     result = "임대하다";
                 }
             }
             else if (content == "contact")
             {
                 if (language == "VN")
                 {
                     result = "LIÊN HỆ";
                 }
                 else if (language == "EN")
                 {
                     result = "CONTACT";
                 }
                 else if (language == "CN")
                 {
                     result = "联系";
                 }
                 else if (language == "JP")
                 {
                     result = "お問い合わせ";
                 }
                 else if (language == "KR")
                 {
                     result = "연락하다";
                 }
             }

            return result;
        }

         public ActionResult MapIndex()
        {
            return View();
        }

         public ActionResult ContactIndex(string Language)
         {
             if (Session["Language"] != null)
             {
                 Language = Session["Language"].ToString();
             }
             PATDBDataContext db = new PATDBDataContext();
             List<ContactEdit> l_edit = db.ContactEdits.Where(x => x.Language == "VN").ToList();
             ContactEditModel model = new ContactEditModel();
             model.CompanyName = db.ContactEdits.Where(x => x.ContactEditCode == "CompanyName" && x.Language == Language).Select(x => x.ContactEditContent).FirstOrDefault();
             if (model.CompanyName == null)
                 model.CompanyName = l_edit.Where(x => x.ContactEditCode == "CompanyName").Select(x => x.ContactEditContent).FirstOrDefault();
             model.Hotline = db.ContactEdits.Where(x => x.ContactEditCode == "Hotline" && x.Language == Language).Select(x => x.ContactEditContent).FirstOrDefault();
             if (model.Hotline == null)
                 model.Hotline = l_edit.Where(x => x.ContactEditCode == "Hotline").Select(x => x.ContactEditContent).FirstOrDefault(); ;
             model.HotlineContent = db.ContactEdits.Where(x => x.ContactEditCode == "HotlineContent" && x.Language == Language).Select(x => x.ContactEditContent).FirstOrDefault();
             if (model.HotlineContent == null)
                 model.HotlineContent = l_edit.Where(x => x.ContactEditCode == "HotlineContent").Select(x => x.ContactEditContent).FirstOrDefault();
             model.Address = db.ContactEdits.Where(x => x.ContactEditCode == "Address" && x.Language == Language).Select(x => x.ContactEditContent).FirstOrDefault();
             if (model.Address == null)
                 model.Address = l_edit.Where(x => x.ContactEditCode == "Address").Select(x => x.ContactEditContent).FirstOrDefault();
             model.AddressContent = db.ContactEdits.Where(x => x.ContactEditCode == "AddressContent" && x.Language == Language).Select(x => x.ContactEditContent).FirstOrDefault();
             if (model.AddressContent == null)
                 model.AddressContent = l_edit.Where(x => x.ContactEditCode == "AddressContent").Select(x => x.ContactEditContent).FirstOrDefault();
             model.Phone = db.ContactEdits.Where(x => x.ContactEditCode == "Phone" && x.Language == Language).Select(x => x.ContactEditContent).FirstOrDefault();
             if (model.Phone == null)
                 model.Phone = l_edit.Where(x => x.ContactEditCode == "Phone").Select(x => x.ContactEditContent).FirstOrDefault();
             model.PhoneContent = db.ContactEdits.Where(x => x.ContactEditCode == "PhoneContent" && x.Language == Language).Select(x => x.ContactEditContent).FirstOrDefault();
             if (model.PhoneContent == null)
                 model.PhoneContent = l_edit.Where(x => x.ContactEditCode == "PhoneContent").Select(x => x.ContactEditContent).FirstOrDefault();
             model.Fax = db.ContactEdits.Where(x => x.ContactEditCode == "Fax" && x.Language == Language).Select(x => x.ContactEditContent).FirstOrDefault();
             if (model.Fax == null)
                 model.Fax = l_edit.Where(x => x.ContactEditCode == "Fax").Select(x => x.ContactEditContent).FirstOrDefault();
             model.FaxContent = db.ContactEdits.Where(x => x.ContactEditCode == "FaxContent" && x.Language == Language).Select(x => x.ContactEditContent).FirstOrDefault();
             if (model.FaxContent == null)
                 model.FaxContent = l_edit.Where(x => x.ContactEditCode == "FaxContent").Select(x => x.ContactEditContent).FirstOrDefault();
             model.Email = db.ContactEdits.Where(x => x.ContactEditCode == "Email" && x.Language == Language).Select(x => x.ContactEditContent).FirstOrDefault();
             if (model.Email == null)
                 model.Email = l_edit.Where(x => x.ContactEditCode == "Email").Select(x => x.ContactEditContent).FirstOrDefault();
             model.EmailContent = db.ContactEdits.Where(x => x.ContactEditCode == "EmailContent" && x.Language == Language).Select(x => x.ContactEditContent).FirstOrDefault();
             if (model.EmailContent == null)
                 model.EmailContent = l_edit.Where(x => x.ContactEditCode == "EmailContent").Select(x => x.ContactEditContent).FirstOrDefault();
             model.Description = db.ContactEdits.Where(x => x.ContactEditCode == "Description" && x.Language == Language).Select(x => x.ContactEditContent).FirstOrDefault();
             if (model.Description == null)
                 model.Description = l_edit.Where(x => x.ContactEditCode == "Description").Select(x => x.ContactEditContent).FirstOrDefault();
             model.CustomerName = db.ContactEdits.Where(x => x.ContactEditCode == "CustomerName" && x.Language == Language).Select(x => x.ContactEditContent).FirstOrDefault();
             if (model.CustomerName == null)
                 model.CustomerName = l_edit.Where(x => x.ContactEditCode == "CustomerName").Select(x => x.ContactEditContent).FirstOrDefault();
             model.CustomerContent = db.ContactEdits.Where(x => x.ContactEditCode == "CustomerContent" && x.Language == Language).Select(x => x.ContactEditContent).FirstOrDefault();
             if (model.CustomerContent == null)
                 model.CustomerContent = l_edit.Where(x => x.ContactEditCode == "CustomerContent").Select(x => x.ContactEditContent).FirstOrDefault();
             model.CustomerMail = db.ContactEdits.Where(x => x.ContactEditCode == "CustomerMail" && x.Language == Language).Select(x => x.ContactEditContent).FirstOrDefault();
             if (model.CustomerMail == null)
                 model.CustomerMail = l_edit.Where(x => x.ContactEditCode == "CustomerMail").Select(x => x.ContactEditContent).FirstOrDefault();
             model.CustomerPhone = db.ContactEdits.Where(x => x.ContactEditCode == "CustomerPhone" && x.Language == Language).Select(x => x.ContactEditContent).FirstOrDefault();
             if (model.CustomerPhone == null)
                 model.CustomerPhone = l_edit.Where(x => x.ContactEditCode == "CustomerPhone").Select(x => x.ContactEditContent).FirstOrDefault();
             model.CustomerSubject = db.ContactEdits.Where(x => x.ContactEditCode == "CustomerSubject" && x.Language == Language).Select(x => x.ContactEditContent).FirstOrDefault();
             if (model.CustomerSubject == null)
                 model.CustomerSubject = l_edit.Where(x => x.ContactEditCode == "CustomerSubject").Select(x => x.ContactEditContent).FirstOrDefault();
             model.Send = db.ContactEdits.Where(x => x.ContactEditCode == "Send" && x.Language == Language).Select(x => x.ContactEditContent).FirstOrDefault();
             if (model.Send == null)
                 model.Send = l_edit.Where(x => x.ContactEditCode == "Send").Select(x => x.ContactEditContent).FirstOrDefault();
             model.Refresh = db.ContactEdits.Where(x => x.ContactEditCode == "Refresh" && x.Language == Language).Select(x => x.ContactEditContent).FirstOrDefault();
             if (model.Refresh == null)
                 model.Refresh = l_edit.Where(x => x.ContactEditCode == "Refresh").Select(x => x.ContactEditContent).FirstOrDefault();
             return View(model);
         }
	}
}