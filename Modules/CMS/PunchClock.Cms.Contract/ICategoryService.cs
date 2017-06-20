using PunchClock.Cms.Model;
using PunchClock.Core.Models.Common;

namespace PunchClock.Cms.Contract
{
   public interface ICategoryService
   {
        ArticleCategory Add(ArticleCategory category);
        ArticleCategory Update(ArticleCategory category);
        AjaxResponse Delete(int catgeoryId);
   }
}
