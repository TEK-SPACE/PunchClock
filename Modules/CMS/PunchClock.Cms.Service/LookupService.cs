using System;
using System.Collections.Generic;
using System.Linq;
using PunchClock.Cms.Contract;
using PunchClock.Cms.Model;
using PunchClock.Core.DataAccess;
using ArticleType = PunchClock.Cms.Model.ArticleType;

namespace PunchClock.Cms.Service
{
   public class LookupService:ILookupService
    {
        public Article GetOneArticle(int articleId)
        {
           Article article;
            using (var context = new PunchClockDbContext())
            {
                article = context.Articles.FirstOrDefault(x => x.Id == articleId && !x.IsDeleted);
            }
            return article;
        }

        public IEnumerable<Article> GetManyArticles()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ArticleComments> GetCommentsByArticleId(int artcileId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ArticleTag> GeTags()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ArticleType> GetTypes()
        {
            throw new NotImplementedException();
        }

        public List<Category> GetAllCategories()
        {

            List<Category> categories;
             using (var context = new PunchClockDbContext())
            {
              var  categorys = from category in context.Categrories
                               where category.IsDeleted == false
                               select category;
                categories = categorys.ToList();
            }
            return categories;
        }
    }
}
