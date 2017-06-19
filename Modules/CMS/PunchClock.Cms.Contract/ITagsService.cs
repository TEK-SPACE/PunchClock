using PunchClock.Cms.Model;
using PunchClock.Core.Models.Common;

namespace PunchClock.Cms.Contract
{
    public interface ITagsService
    {
        ArticleTag Add(ArticleTag articleTag);
        ArticleTag Update(ArticleTag articleTag);
        AjaxResponse Delete(int id);
    }
}
