using PunchClock.Cms.Model;

namespace PunchClock.Cms.Contract
{
   public interface IArticle
   {
       int Add(Article article);
       int Update(Article article);
       bool Delete(int articleId);
   }
}
