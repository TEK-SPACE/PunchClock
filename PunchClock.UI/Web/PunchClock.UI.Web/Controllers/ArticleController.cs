using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PunchClock.Cms.Contract;
using PunchClock.Cms.Model;
using PunchClock.Cms.Service;
using PunchClock.Core.Contracts;
using PunchClock.Core.Implementation;
using PunchClock.Core.Models.Common;
using PunchClock.Domain.Model;

namespace PunchClock.UI.Web.Controllers
{
    [Authorize]
    public class ArticleController : BaseController
    {
        private  readonly ICategoryService CategoryService;
        private  readonly ITagsService TagsService;
        private readonly IArticleService _articleService;
    
        public ArticleController()
        {
            CategoryService = new CategoryService();
            TagsService = new TagService();
            _articleService=new ArticleService();
       }
        

        // GET: CMS
        public ActionResult Add()
        {
            return View();
        }
        public ActionResult Dashboard()
        {
            return View(new List<Article>());
        }
        public ActionResult Edit(int id)
        {
            Article article = _articleService.GetOneArticle(id);
            article.Tags = article.Tag.Split(',');
            return View(article);
        }

        public List<SelectListItem> GetCategoriesByCompanyList()
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
        [HttpPost]
        public ActionResult AddNewArticle(Article article)
        {
            article.CompanyId = OperatingUser.CompanyId;
            article.CreatedById = OperatingUser.Id;
            article.ModifiedById = OperatingUser.Id;
            article.Tag = string.Join(",", article.Tags);
            article = _articleService.Add(article);
           return RedirectToAction("Edit", "Article", new { id = article.Id });
      }
        [HttpPost]
        public ActionResult Update(Article article)
        {
            article.CompanyId = OperatingUser.CompanyId;
            article.ModifiedById = OperatingUser.Id;
            article.Tag = string.Join(",", article.Tags);
            article = _articleService.Update(article);
            return RedirectToAction("Edit", "Article", new { id = article.Id });
        }
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            var appsettings = _articleService.GetArticlesByCompanyId(OperatingUser.CompanyId);
            return Json(appsettings.ToDataSourceResult(request));
        }

      
    }
}