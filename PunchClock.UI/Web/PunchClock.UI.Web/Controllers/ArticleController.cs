using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PunchClock.Cms.Contract;
using PunchClock.Cms.Model;
using PunchClock.Cms.Service;
using PunchClock.Core.Contracts;
using PunchClock.Core.Implementation;
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
        public ActionResult Index()
        {
            ViewData["ArticleCategory"] = CategoryService.GetArticleCategoriesByCompanyId(OperatingUser.CompanyId);
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
            var articlesList = _articleService.GetArticlesByCompanyId(OperatingUser.CompanyId);
            IOrderedEnumerable<Article> articles = null;
            if (request.Sorts.Count > 0)
            {
                if (request.Sorts[0].Member.Equals("CategoryId", StringComparison.OrdinalIgnoreCase))
                    if (request.Sorts[0].SortDirection == System.ComponentModel.ListSortDirection.Ascending)
                    {
                        articles = articlesList.OrderBy(x => x.Category.Name);
                        articlesList = articles.ToList();
                        request.Sorts = new List<SortDescriptor>();

                    }
                    else
                    {
                        articles = articlesList.OrderByDescending(x => x.Category.Name);
                        articlesList = articles.ToList();
                        request.Sorts = new List<SortDescriptor>();
                    }
            }

            foreach (var article in articlesList)
            {
                article.Tags = article.Tag.Split(',');
            }

            return Json(articlesList.ToDataSourceResult(request));
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var article = _articleService.Delete(id);
           return Json(article);
        }
    
    }
}