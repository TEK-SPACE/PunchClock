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

        public static List<SelectListItem> GetCategoriesByCompanyList(string userId)
        {
            var user = UserRepository.DetailsByKey(userId);
            var categoriesByCompanyId = CategoryService.GetArticleCategoriesByCompanyId(user.CompanyId);

            return categoriesByCompanyId.Select(category => new SelectListItem
            {
                Text = category.Name,
                Value = category.Id.ToString()
            }).ToList();
        }

        public static List<SelectListItem> GetTagsByComapnyIdList(string userId)
        {
            var user = UserRepository.DetailsByKey(userId);
            var articleTagByCompany = TagsService.GetArticleTagsByCompany(user.CompanyId);

            return articleTagByCompany.Select(tag => new SelectListItem
            {
                Text = tag.Name,
                Value = tag.Id.ToString()
            }).ToList();
        }
    }
}