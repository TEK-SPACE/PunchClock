using System.Collections.Generic;
using PunchClock.Cms.Model;
using ArticleType = PunchClock.Cms.Model.ArticleType;

namespace PunchClock.Cms.Contract
{
  public  interface ILookupService
  {
      Article GetOneArticle(int articleId);
      IEnumerable<Article> GetManyArticles();
      IEnumerable<ArticleComments> GetCommentsByArticleId(int artcileId);
      IEnumerable<ArticleTag> GeTags();
      IEnumerable<ArticleType> GetTypes();
      List<Category> GetAllCategories();
  }
}
