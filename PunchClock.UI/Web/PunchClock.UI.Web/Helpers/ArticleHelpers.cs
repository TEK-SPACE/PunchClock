using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PunchClock.Cms.Contract;
using PunchClock.Cms.Service;

namespace PunchClock.UI.Web.Helpers
{
    public static class ArticleHelpers
    {
         private static readonly ICategoryService CategoryService;
        private static readonly ITagsService TagsService;


        static ArticleHelpers()
        {
             CategoryService=new CategoryService();
            TagsService=new TagService();
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

        public static List<SelectListItem> GetCategoriesByCompanyList(int companyId)
        {
            var categoriesByCompanyId = CategoryService.GetCategoriesByCompanyId(companyId);

            var returnList = new List<SelectListItem>();
            var defaultValue = new SelectListItem
            {
                Text = "Select Category",
                Value = "0"
            };

            returnList.Add(defaultValue);
            if (categoriesByCompanyId != null)
                returnList.AddRange(categoriesByCompanyId.Select(category => new SelectListItem
                {
                    Text = category.Name,
                    Value = category.Id.ToString()
                }));

            return returnList;
        }
        public static List<SelectListItem> GetTagsByComapnyIdList(int companyId)
        {
            var articleTagByCompany = TagsService.GetArticleTagByCompany(companyId);

            var returnList = new List<SelectListItem>();
            var defaultValue = new SelectListItem
            {
                Text = "Select Tags",
                Value = "0"
            };

            returnList.Add(defaultValue);
            if (articleTagByCompany != null)
                returnList.AddRange(articleTagByCompany.Select(tag => new SelectListItem
                {
                    Text = tag.Name,
                    Value = tag.Id.ToString()
                }));

            return returnList;
        }
    }
}