using System.Collections.Generic;
using PunchClock.Cms.Model;
using PunchClock.Domain.Model;

namespace PunchClock.Cms.Contract
{
   public interface IArticleService
   {
       Article Add(Article article);
        Article Update(Article article);
       AjaxResponse Delete(int articleId);
       Article GetOneArticle(int id);
       List<Article> GetAllArticles();
       List<Article> GetArticlesByCompanyId(int companyId);
   }
}
