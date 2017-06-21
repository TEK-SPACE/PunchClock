using System;
using System.Collections.Generic;
using System.Linq;
using PunchClock.Cms.Contract;
using PunchClock.Cms.Model;
using PunchClock.Core.DataAccess;
using PunchClock.Core.Models.Common;
using PunchClock.Language.Model;

namespace PunchClock.Cms.Service
{
   public class TagService: ITagsService
    {
        public ArticleTag Add(ArticleTag articleTag)
        {
            using (var context = new PunchClockDbContext())
            {
                articleTag.CreatedDate=DateTime.Now;
                articleTag.IsDeleted = false;
                context.ArticleTags.Add(articleTag);
                context.SaveChanges();

            }
            AddArticleTagResources(articleTag);
            return articleTag;
        }
        private void AddArticleTagResources(ArticleTag articleTag)
        {
            if (articleTag.Id <= 0) return;
            for (var i = 1; i <= 3; i++)
            {
                var culture = Culture.English;
                if (i == (int)Culture.Spanish)
                    culture = Culture.Spanish;
                if (i == (int)Culture.Hindi)
                    culture = Culture.Hindi;

                using (var context = new PunchClockDbContext())
                {

                    var tagResources = new ArticleTagResource()
                    {
                        TagMasterId = articleTag.Id,
                        Value = articleTag.Name,
                        Culture = culture,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        LastModifiedBy = articleTag.LastModifiedBy

                    };
                    
                        context.ArticleTagResources.Add(tagResources);
                        context.SaveChanges();
                  }
            }
        }
        public ArticleTag Update(ArticleTag articleTag)
        {
            using (var context = new PunchClockDbContext())
            {
                var existingTag = context.ArticleTags.FirstOrDefault(x => x.Id == articleTag.Id);
                if (existingTag == null) return articleTag;
                existingTag.Name = articleTag.Name;
                existingTag.Description = articleTag.Description;
                existingTag.ModifiedDate = DateTime.Now;
                existingTag.IsDeleted = false;
                context.SaveChanges();

            }
            return articleTag;
        }

        public AjaxResponse Delete(int id)
        {
            var response = new AjaxResponse
            {
                ResponseId = id,
                ResponseText = "Record is not deleted",
                Success = false
            };
            using (var context = new PunchClockDbContext())
            {
                var tags = context.ArticleTags.FirstOrDefault(x => x.Id == id);
                if (tags == null) return response;
                tags.IsDeleted = true;
                tags.ModifiedDate = DateTime.Now;
                context.SaveChanges();
                response.ResponseText = "Tag is Deleted.";
                response.Success = true;
                return response;
            }
        }

        public ArticleTag GetOneArticleTag(int id)
        {
            using (var context = new PunchClockDbContext())
            {
                return context.ArticleTags.FirstOrDefault(x => x.Id == id);
            }
        }

        public List<ArticleTag> GetAllArticleTags()
        {
            using (var context = new PunchClockDbContext())
            {
                return context.ArticleTags.Where(x => x.IsDeleted == false).ToList();
            }
        }

        public List<ArticleTag> GetArticleTagByCompany(int companyId)
        {
            using (var context = new PunchClockDbContext())
            {
                return context.ArticleTags.Where(x => x.IsDeleted == false && x.CompanyId == companyId).ToList();
            }
        }
    }
}
