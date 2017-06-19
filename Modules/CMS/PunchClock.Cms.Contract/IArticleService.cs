using PunchClock.Cms.Model;
using PunchClock.Core.Models.Common;

namespace PunchClock.Cms.Contract
{
   public interface IArticleService
   {
       Article Add(Article article);
        Article Update(Article article);
       AjaxResponse Delete(int articleId);
   }
}
