using System.Collections.Generic;
using PunchClock.Cms.Model;
using PunchClock.Core.Models.Common;

namespace PunchClock.Cms.Contract
{
   public interface ICategoryService
   {
        ArticleCategory Add(ArticleCategory category);
        ArticleCategory Update(ArticleCategory category);
       AjaxResponse Delete(int catgeoryId);
       ArticleCategory GetOneArticleCategory(int id);
       List<ArticleCategory> GetAllArticleCategories();
       List<ArticleCategory> GetArticleCategoriesByCompanyId(int companyId);
   }
}
