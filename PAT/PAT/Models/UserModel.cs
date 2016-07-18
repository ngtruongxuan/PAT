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
      
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
       
    }

    public class UserSearch
    {
        public int GroupID { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Fromdate { get; set; }
        public string Todate { get; set; }

    }
}