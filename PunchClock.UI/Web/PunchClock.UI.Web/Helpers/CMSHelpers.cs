using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PunchClock.Cms.Contract;
using PunchClock.Cms.Service;

namespace PunchClock.UI.Web.Helpers
{
    public static class CmsHelpers
    {
        private static readonly ILookupService LookupService;

        static CmsHelpers()
        {
            LookupService=new LookupService();
        }
        public static List<SelectListItem> IntiCategoryDropDown()
        {
            var returnList = new List<SelectListItem>();
            var defaultValue = new SelectListItem
            {
                Text = "Select Category",
                Value = "0"
            };
            returnList.Add(defaultValue);
            var asc = new SelectListItem
            {
                Text = "Admin",
                Value = "1"
            };
            returnList.Add(asc);

            var dsc = new SelectListItem
            {
                Text = "Ticketing",
                Value = "2"
            };
            returnList.Add(dsc);

            return returnList;
        }

        public static List<SelectListItem> GettCategories()
        {
            var categories = LookupService.GetAllCategories();

            var returnList = new List<SelectListItem>();
            var defaultValue = new SelectListItem
            {
                Text = "Select Category",
                Value = "0"
            };

            returnList.Add(defaultValue);
            if (categories != null)
                returnList.AddRange(categories.Select(category => new SelectListItem
                {
                    Text = category.Name,
                    Value = category.Id.ToString()
                }));

            return returnList;
        }
    }
}