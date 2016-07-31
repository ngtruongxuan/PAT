using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManageShop.Models;

namespace ManageShop.Controllers
{
    public class ContactEditController : Controller
    {
        //
        // GET: /ContactEdit/
        public ActionResult Index()
        {
            PATDBDataContext db = new PATDBDataContext();
            List<ContactEdit> l_edit = db.ContactEdits.Where(x => x.Language == "VN").ToList();
            ContactEditModel model = new ContactEditModel();
            model.CompanyName = l_edit.Where(x => x.ContactEditCode == "CompanyName").Select(x => x.ContactEditContent).FirstOrDefault();
            model.Hotline = l_edit.Where(x => x.ContactEditCode == "Hotline").Select(x => x.ContactEditContent).FirstOrDefault(); ;
            model.HotlineContent = l_edit.Where(x => x.ContactEditCode == "HotlineContent").Select(x => x.ContactEditContent).FirstOrDefault(); 
            model.Address = l_edit.Where(x => x.ContactEditCode == "Address").Select(x => x.ContactEditContent).FirstOrDefault();
            model.AddressContent = l_edit.Where(x => x.ContactEditCode == "AddressContent").Select(x => x.ContactEditContent).FirstOrDefault();
            model.Phone = l_edit.Where(x => x.ContactEditCode == "Phone").Select(x => x.ContactEditContent).FirstOrDefault();
            model.PhoneContent = l_edit.Where(x => x.ContactEditCode == "PhoneContent").Select(x => x.ContactEditContent).FirstOrDefault();
            model.Fax = l_edit.Where(x => x.ContactEditCode == "Fax").Select(x => x.ContactEditContent).FirstOrDefault();
            model.FaxContent = l_edit.Where(x => x.ContactEditCode == "FaxContent").Select(x => x.ContactEditContent).FirstOrDefault();
            model.Email = l_edit.Where(x => x.ContactEditCode == "Email").Select(x => x.ContactEditContent).FirstOrDefault();
            model.EmailContent = l_edit.Where(x => x.ContactEditCode == "EmailContent").Select(x => x.ContactEditContent).FirstOrDefault();
            model.Description = l_edit.Where(x => x.ContactEditCode == "Description").Select(x => x.ContactEditContent).FirstOrDefault();
            model.CustomerName = l_edit.Where(x => x.ContactEditCode == "CustomerName").Select(x => x.ContactEditContent).FirstOrDefault();
            model.CustomerContent = l_edit.Where(x => x.ContactEditCode == "CustomerContent").Select(x => x.ContactEditContent).FirstOrDefault();
            model.CustomerMail = l_edit.Where(x => x.ContactEditCode == "CustomerMail").Select(x => x.ContactEditContent).FirstOrDefault();
            model.CustomerPhone = l_edit.Where(x => x.ContactEditCode == "CustomerPhone").Select(x => x.ContactEditContent).FirstOrDefault();
            model.CustomerSubject = l_edit.Where(x => x.ContactEditCode == "CustomerSubject").Select(x => x.ContactEditContent).FirstOrDefault();
            model.Send = l_edit.Where(x => x.ContactEditCode == "Send").Select(x => x.ContactEditContent).FirstOrDefault();
            model.Refresh = l_edit.Where(x => x.ContactEditCode == "Refresh").Select(x => x.ContactEditContent).FirstOrDefault(); 
            return View(model);
        }

        public ActionResult Edit(string language, string ContactEditCode)
        {
            PATDBDataContext db = new PATDBDataContext();
           
            ContactEditPopupNodel model = new ContactEditPopupNodel();
            model.Language = db.Languages.Where(x => x.LanguageCode == language).Select(x => x.LanguageName).FirstOrDefault();
            model.LanguageVN = db.ContactEdits.Where(x => x.Language == "VN" && x.ContactEditCode == ContactEditCode).Select(x => x.ContactEditContent).FirstOrDefault();
            model.LanguageCurent = db.ContactEdits.Where(x => x.Language == language && x.ContactEditCode == ContactEditCode).Select(x => x.ContactEditContent).FirstOrDefault();
            model.ContactEditCode = ContactEditCode;
            model.LanguageCode = language;
            if (model.LanguageCurent == null)
                model.LanguageCurent = "Chưa có nội dung";
            return View(model);
        }

        public JsonResult getLanguage()
        {
            PATDBDataContext db = new PATDBDataContext();

            var obj = db.Languages.Where(x => x.Status == "A").ToList();
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public JsonResult searchContactEdit(string Language)
        {
            PATDBDataContext db = new PATDBDataContext();
            List<ContactEdit> l_edit = db.ContactEdits.Where(x => x.Language == "VN").ToList();
            ContactEditModel model = new ContactEditModel();
            model.CompanyName = db.ContactEdits.Where(x => x.ContactEditCode == "CompanyName"&&x.Language==Language).Select(x => x.ContactEditContent).FirstOrDefault();
            if (model.CompanyName==null)
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
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult saveContactEdit(ContactEditPopupNodel model)
        {
            PATDBDataContext db = new PATDBDataContext();
            ContactEdit update = db.ContactEdits.Where(x => x.Language == model.LanguageCode && x.ContactEditCode == model.ContactEditCode).FirstOrDefault();
            if(update!=null)
            {
                update.LastUpdatedBy = Session["UserName"].ToString();
                update.LastUpdatedDateTime = DateTime.Now;
                update.ContactEditContent = model.ContactEditContent;
                db.SubmitChanges();
            }
            else
            {
                ContactEdit editnew = new ContactEdit();
                editnew.ContactEditCode = model.ContactEditCode;
                editnew.ContactEditContent = model.ContactEditContent;
                editnew.CreatedBy = Session["UserName"].ToString();
                editnew.CreatedDateTime = DateTime.Now;
                editnew.Language = model.LanguageCode;
                editnew.LastUpdatedBy = Session["UserName"].ToString();
                editnew.LastUpdatedDateTime = DateTime.Now;
                editnew.Status = "A";
                db.ContactEdits.InsertOnSubmit(editnew);
                db.SubmitChanges();

            }
            var commend = "Lưu dữ liệu thành công";
            var question = new ManageShop.Controllers.Base.Question { Title = commend, Success = "1" };
            return Json(question);
        }
	}
}