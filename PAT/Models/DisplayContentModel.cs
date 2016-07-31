using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageShop.Models
{
    public class DisplayContentModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string tab { get; set; }
        public double price { get; set; }

        public string Sell_language { get; set; }
        public string Rent_language { get; set; }

        public string Sell_content { get; set; }
        public string Rent_content { get; set; }
        public string Sell_price { get; set; }
        public string Rent_price { get; set; }
        public string contact { get; set; }


    }
    public class DisplayNewsModel
    {
        public string newsID { get; set; }
        public string categoryName { get; set; }
        public string img { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string date { get; set; }
        public string content { get; set; }

       public List<DisplayNewsModel> l_news = new List<DisplayNewsModel>();

    }
}