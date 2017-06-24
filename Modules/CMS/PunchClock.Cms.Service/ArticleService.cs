using System;
using System.Collections.Generic;
using System.Linq;
using PunchClock.Cms.Contract;
using PunchClock.Cms.Model;
using PunchClock.Core.DataAccess;
using PunchClock.Core.Models.Common;

namespace PunchClock.Cms.Service
{
    public class ArticleService:IArticleService
    {
        public Article Add(Article article)
        {
            try
            {

         
             using (var context = new PunchClockDbContext())
            {
                  context.Articles.Add(article);
                context.SaveChanges();
            }
            }
            catch (Exception e)
            {
                var es = e;
            }
            return article;
        }

        public Article Update(Article article)
        {
            using (var context = new PunchClockDbContext())
            {
                var oldArticle = context.Articles.FirstOrDefault(x => x.Id == article.Id);
                if (oldArticle == null) return article;
                oldArticle.Title = article.Title;
                oldArticle.Description = article.Description;
                oldArticle.IsPublished = article.IsPublished;
                oldArticle.IsDeleted = false;
                oldArticle.CategoryId = article.CategoryId;
                oldArticle.ModifiedById = article.ModifiedById;
                oldArticle.Tag = article.Tag;
                oldArticle.IsPrivate = article.IsPrivate;
                context.SaveChanges();
            }
            return article;
        }

        public AjaxResponse Delete(int articleId)
        {
            var response = new AjaxResponse
            {
                ResponseId = articleId,
                ResponseText = "Record is not deleted",
                Success = false
            };
            using (var context = new PunchClockDbContext())
            {
                var article = context.Articles.FirstOrDefault(x => x.Id == articleId);
                if (article == null) return response;
                article.IsDeleted = true;
                context.SaveChanges();
                response.ResponseText = "Article is Deleted.";
                response.Success = true;
                return response;
            }
        }

        public Article GetOneArticle(int id)
        {
            using (var context = new PunchClockDbContext())
            {
                return context.Articles.FirstOrDefault(x => x.Id == id);
            }
        }

        public List<Article> GetAllArticles()
        {
            using (var context = new PunchClockDbContext())
            {
                return context.Articles.Where(x => x.IsDeleted == false).ToList();
            }
        }

        public List<Article> GetArticlesByCompanyId(int companyId)
        {
            using (var context = new PunchClockDbContext())
            {
                return context.Articles.Where(x => x.IsDeleted == false && x.CompanyId==companyId).ToList();
            }
        }
    }
}
