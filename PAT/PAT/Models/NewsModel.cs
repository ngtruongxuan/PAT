using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageShop.Models
{
    public class NewsModel
    {
        public long ID{get; set;}

        public string NewsName{get; set;}

        public string NewsDecription{get; set;}

        public string NewsContent{get; set;}

        public string Language{get; set;}

        public System.Nullable<System.DateTime> NewsDate{get; set;}

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