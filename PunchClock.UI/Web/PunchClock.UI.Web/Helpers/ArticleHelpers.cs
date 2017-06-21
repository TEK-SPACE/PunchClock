using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PunchClock.Cms.Contract;
using PunchClock.Cms.Service;
using PunchClock.Core.Contracts;
using PunchClock.Core.Implementation;

namespace PunchClock.UI.Web.Helpers
{
    public static class ArticleHelpers
    {
        private static readonly ICategoryService CategoryService;
        private static readonly ITagsService TagsService;
        private static readonly IUserRepository UserRepository;

        static ArticleHelpers()
        {
            CategoryService = new CategoryService();
            TagsService = new TagService();
            UserRepository = new UserService();
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

        public static List<SelectListItem> GetCategoriesByCompanyList(string userId)
        {
            var user = UserRepository.DetailsByKey(userId);
            var categoriesByCompanyId = CategoryService.GetCategoriesByCompanyId(user.CompanyId);

            return categoriesByCompanyId.Select(category => new SelectListItem
            {
                Text = category.Name,
                Value = category.Id.ToString()
            }).ToList();
        }

        public static List<SelectListItem> GetTagsByComapnyIdList(string userId)
        {
            var user = UserRepository.DetailsByKey(userId);
            var articleTagByCompany = TagsService.GetArticleTagByCompany(user.CompanyId);

            return articleTagByCompany.Select(tag => new SelectListItem
            {
                Text = tag.Name,
                Value = tag.Id.ToString()
            }).ToList();
        }
    }
}