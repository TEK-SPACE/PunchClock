using System.Collections.Generic;
using PunchClock.Cms.Model;
using PunchClock.Domain.Model;

namespace PunchClock.Cms.Contract
{
    public interface ICommentService
    {
        ArticleComment Add(ArticleComment comment);
        ArticleComment Update(ArticleComment comments);
        AjaxResponse Delete(int id);
        ArticleComment GetOneArticleComment(int id);
        List<ArticleComment> GetAllCommentsByArticleId(int articleId);
        List<ArticleComment> GetAllCommentsByCompanyId(int companyId);
    }
}
