using PunchClock.Cms.Model;
using PunchClock.Core.Models.Common;

namespace PunchClock.Cms.Contract
{
    public interface ICommentService
    {
        ArticleComment Add(ArticleComment comment);
        ArticleComment Update(ArticleComment comments);
        AjaxResponse Delete(int id);
    }
}
