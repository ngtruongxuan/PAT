using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageShop.Models
{
    public class PictureModel 
    {
        public long ID{get; set;}

        public long CategoryID{get; set;}
        public string CategoryName { get; set; }

        public string URL{get; set;}

        public string Type{get; set;}

        public string Reftype{get; set;}

        public string Refcode{get; set;}

        public string Remark{get; set;}

        public string Status{get; set;}

        public string CreatedBy{get; set;}

        public System.DateTime CreatedDateTime{get; set;}

        public string LastUpdatedBy{get; set;}

        public System.DateTime LastUpdatedDateTime{get; set;}
    }
}