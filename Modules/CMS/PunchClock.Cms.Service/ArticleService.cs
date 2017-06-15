using System;
using System.Linq;
using PunchClock.Cms.Contract;
using PunchClock.Cms.Model;
using PunchClock.Core.DataAccess;

namespace PunchClock.Cms.Service
{
    public class ArticleService:IArticle
    {
        public int Add(Article article)
        {
            using (var context = new PunchClockDbContext())
            {
                article.CreatedDate=DateTime.Now;
                context.Articles.Add(article);
                context.SaveChanges();
            }
            return article.Id;
        }

        public int Update(Article article)
        {
            using (var context = new PunchClockDbContext())
            {
                article.ModifiedDate = DateTime.Now;
                context.SaveChanges();
            }
            return article.Id;
        }

        public bool Delete(int articleId)
        {
            using (var context = new PunchClockDbContext())
            {
                var article = context.Articles.FirstOrDefault(x => x.Id == articleId);
                if (article == null) return false;
                article.ModifiedDate = DateTime.Now;
                article.IsDeleted = true;
                context.SaveChanges();
                return true;
            }
        }
    }
}
