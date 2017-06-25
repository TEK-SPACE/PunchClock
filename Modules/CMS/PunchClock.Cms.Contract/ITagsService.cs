using System.Collections.Generic;
using PunchClock.Cms.Model;
using PunchClock.Domain.Model;

namespace PunchClock.Cms.Contract
{
    public interface ITagsService
    {
        ArticleTag Add(ArticleTag articleTag);
        ArticleTag Update(ArticleTag articleTag);
        AjaxResponse Delete(int id);
        ArticleTag GetOneArticleTag(int id);
        List<ArticleTag> GetAllArticleTags();
        List<ArticleTag> GetArticleTagsByCompany(int companyId);
    }
}
