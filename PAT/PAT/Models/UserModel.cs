using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageShop.Models
{
    public class UserModel
    {
    }

    public class UserDetail
    {
        public int GroupID { get; set; }
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }

        public string Status { get; set; }
       
    }

    public class UserSearch
    {
        public int GroupID { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }

    }

    public class UserResult
    {
        public int GroupID { get; set; }

        public string GroupName { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string StatusName { get; set; }
        public long ID { get; set; }

    }
}