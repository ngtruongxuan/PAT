using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageShop.Models
{
    public class ContentModel
    {
        private long ID{ get; set;}

        private string ContentName{ get; set;}

        private long CategoryID{ get; set;}

        private string ContentDisplay{ get; set;}

        private string Language{ get; set;}

        private string Reftype{ get; set;}

        private string Refcode{ get; set;}

        private string Remark{ get; set;}

        private string Status{ get; set;}

        private string CreatedBy{ get; set;}

        private System.DateTime CreatedDateTime{ get; set;}

        private string LastUpdatedBy{ get; set;}

        private System.DateTime LastUpdatedDateTime{ get; set;}
    }
}