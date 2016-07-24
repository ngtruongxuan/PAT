using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageShop.Models
{
    public class ContactEditModel
    {
        public string CompanyName { get; set; }
        public string Hotline { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string HotlineContent { get; set; }
        public string PhoneContent { get; set; }
        public string AddressContent { get; set; }
        public string FaxContent { get; set; }
        public string EmailContent { get; set; }
        public string Description { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMail { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerSubject { get; set; }
        //public string EmailContent { get; set; }
        public string CustomerContent { get; set; }
        public string Send { get; set; }
        public string Refresh { get; set; }

       

    }
    public class ContactEditPopupNodel
    {
        public string Language { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageVN { get; set; }
        public string LanguageCurent { get; set; }
        public string ContentEdit { get; set; }
        public string ContactEditCode { get; set; }
        public string ContactEditContent { get; set; }
    }
}