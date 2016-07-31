using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageShop.Models
{
    public class TreeViewModel
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public int NumContent { get; set; }
        public List<TreeViewModel> Child = new List<TreeViewModel>();
    }
}