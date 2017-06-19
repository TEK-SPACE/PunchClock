using System.Collections.Generic;
using PunchClock.Cms.Model;
using ArticleType = PunchClock.Cms.Model.ArticleType;

namespace PunchClock.Cms.Contract
{
    public interface ILookupService
    {
        Article GetOneArticle(int articleId);
        IEnumerable<Article> GetManyArticles();
        IEnumerable<ArticleComment> GetCommentsByArticleId(int artcileId);

        IEnumerable<ArticleTag> GetAllTags();
        IEnumerable<ArticleTag> GetTagsByCompany(int id);
        IEnumerable<ArticleTag> GetTagById(int id);

        IEnumerable<ArticleType> GetTypes();
        List<ArticleCategory> GetAllCategories();
    }
}
