using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageShop.Models
{
    public class CategoryModel
    {
        public string in_company { get; set; }
        public string in_industry { get; set; }
        public string in_land { get; set; }
        public string in_environment { get; set; }
        public string in_location { get; set; }
        public string in_map { get; set; }
        public string re_house1 { get; set; }
        public string re_house2 { get; set; }
        public string re_nexthouse { get; set; }
        public string re_workerhouse { get; set; }
        public string re_flathouse { get; set; }
        public string re_gardenhouse { get; set; }
        public string fa_factory { get; set; }
        public string fa_land { get; set; }
        public string co_cost { get; set; }
        public string co_savecost { get; set; }
        public string co_savetime { get; set; }
        public string co_develop { get; set; }
        public string ne_law { get; set; }
        public string ne_pat { get; set; }
        public string ne_picture { get; set; }
        public string ne_video { get; set; }
        public string ne_recruit { get; set; }
        public string contact_in { get; set; }

        public string sy_electric { get; set; }
        public string sy_water { get; set; }
        public string sy_pollution { get; set; }
        public string sy_garbage { get; set; }
        public string infastructure_l { get; set; }
        public string menu_info { get; set; }
        public string menu_resident { get; set; }
        public string menu_industry { get; set; }
        public string menu_infrastructure { get; set; }
        public string menu_investor { get; set; }
        public string menu_library { get; set; }
        public string menu_contact { get; set; }


        public List<CategoryCodeModel> l_code = new List<CategoryCodeModel>();
        public List<CategoryCodeModel> l_code_new = new List<CategoryCodeModel>();
    }

    public class CategoryTreeModel
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string CategoryCode { get; set; }
        public List<CategoryTreeModel> Child = new List<CategoryTreeModel>();
        
    }
    public class CategoryCodeModel
    {
        public string CategoryCode { get; set; }
    }

    public class CategoryPopupModel
    {
        public string Language { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageVN { get; set; }
        public string LanguageCurent { get; set; }
        public string ContentEdit { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryContent { get; set; }
    }
}