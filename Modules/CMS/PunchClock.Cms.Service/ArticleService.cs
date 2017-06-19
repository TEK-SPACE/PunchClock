using System;
using System.Linq;
using PunchClock.Cms.Contract;
using PunchClock.Cms.Model;
using PunchClock.Core.DataAccess;

namespace PunchClock.Cms.Service
{
    public class ArticleService:IArticleService
    {
        public Article Add(Article article)
        {
           
             using (var context = new PunchClockDbContext())
            {
              article.CreatedDate=DateTime.Now.ToLocalTime();
                  context.Articles.Add(article);
                context.SaveChanges();
            }
          
            return article;
        }

        public Article Update(Article article)
        {
            using (var context = new PunchClockDbContext())
            {
                var oldArticle = context.Articles.FirstOrDefault(x => x.Id == article.Id);
                if (oldArticle == null) return article;
                oldArticle.ModifiedDate = DateTime.Now.ToLocalTime();
                oldArticle.Title = article.Title;
                oldArticle.Description = article.Description;
                oldArticle.IsPublished = article.IsPublished;
                oldArticle.IsDeleted = false;
                oldArticle.LastModifiedBy = article.LastModifiedBy;
                oldArticle.CategoryId = article.CategoryId;
                context.SaveChanges();
            }
            return article;
        }

        public CmsResponse Delete(int articleId)
        {
            var response = new CmsResponse
            {
                ResponseId = articleId,
                ResponseText = "Record is not deleted",
                Success = false
            };
            using (var context = new PunchClockDbContext())
            {
                var article = context.Articles.FirstOrDefault(x => x.Id == articleId);
                if (article == null) return response;
                article.ModifiedDate = DateTime.Now;
                article.IsDeleted = true;
                context.SaveChanges();
                response.ResponseText = "Article is Deleted.";
                response.Success = true;
                return response;
            }
        }
    }
}
