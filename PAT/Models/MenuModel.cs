using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageShop.Models
{
    
    public class MenuItem
    {
        public int ID { get; set; }
        public int ParentID { get; set; }
        public String MenuName { get; set; }
        public String URL { get; set; }
        public String AWE { get; set; }
        public List<SubMenuItem> SubMenuItems { get; private set; }
        public MenuItem()
        {
            SubMenuItems = new List<SubMenuItem>();
        }
    }

    public class SubMenuItem
    {
        public int ID { get; set; }
        public int ParentID { get; set; }
        public String MenuName { get; set; }
        public String Action { get; set; }
    }

    public class MenuModel
    {
        public int ID { get; set; }
        public String MenuName { get; set; }
        public List<MenuItem> MenuItems { get; private set; }
        public MenuModel()
        {
            MenuItems = new List<MenuItem>();
        }
    }
}