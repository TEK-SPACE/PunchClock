using PunchClock.Cms.Model;

namespace PunchClock.Cms.Contract
{
   public interface IArticleService
   {
       Article Add(Article article);
        Article Update(Article article);
        CmsResponse Delete(int articleId);
   }
}
