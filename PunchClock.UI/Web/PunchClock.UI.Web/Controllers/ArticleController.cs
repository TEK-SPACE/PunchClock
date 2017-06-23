using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PunchClock.Cms.Contract;
using PunchClock.Cms.Service;
using PunchClock.Core.Contracts;
using PunchClock.Core.Implementation;
using PunchClock.Core.Models.Common;

namespace PunchClock.UI.Web.Controllers
{
    //[Authorize]
    public class ArticleController : BaseController
    {
        private  readonly ICategoryService CategoryService;
        private  readonly ITagsService TagsService;
    
        public ArticleController()
        {
            CategoryService = new CategoryService();
            TagsService = new TagService();
       }
        

        // GET: CMS
        public ActionResult Add()
        {
            return View();
        }


        public  List<SelectListItem> GetCategoriesByCompanyList()
        {
          var categoriesByCompanyId = CategoryService.GetArticleCategoriesByCompanyId(OperatingUser.CompanyId);
            return categoriesByCompanyId.Select(category => new SelectListItem
            {
                Text = category.Name,
                Value = category.Id.ToString()
            }).ToList();
        }

        public  List<SelectListItem> GetTagsByComapnyIdList()
        {
           var articleTagByCompany = TagsService.GetArticleTagsByCompany(OperatingUser.CompanyId);
            return articleTagByCompany.Select(tag => new SelectListItem
            {
                Text = tag.Name,
                Value = tag.Id.ToString()
            }).ToList();
        }
    }
}