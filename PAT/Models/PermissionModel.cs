using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageShop.Models
{
    public class PermissionModel
    {
        public long GroupID { get; set; }
        public long ModuleID { get; set; }

        public string Status { get; set; }
        public List<childPermission> l_permission { get; private set; }

        public PermissionModel()
        {
            l_permission = new List<childPermission>();
        }
    }

    public class childPermission
    {
        public long ModuleID { get; set; }
    }

}