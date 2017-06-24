using System;
using System.Collections.Generic;
using System.Linq;
using PunchClock.Cms.Contract;
using PunchClock.Cms.Model;
using PunchClock.Core.DataAccess;
using PunchClock.Domain.Model;
using PunchClock.Language.Model;

namespace PunchClock.Cms.Service
{
   public class TagService: ITagsService
    {
        public ArticleTag Add(ArticleTag articleTag)
        {
            using (var context = new PunchClockDbContext())
            {
                articleTag.IsDeleted = false;
                context.ArticleTags.Add(articleTag);
                context.ArticleTagResources.AddRange(articleTag.Resources);
                context.SaveChanges();
            }
           return articleTag;
        }
 
        public ArticleTag Update(ArticleTag articleTag)
        {
            using (var context = new PunchClockDbContext())
            {
                var existingTag = context.ArticleTags.FirstOrDefault(x => x.Id == articleTag.Id);
                if (existingTag == null) return articleTag;
                existingTag.Name = articleTag.Name;
                existingTag.Description = articleTag.Description;
                existingTag.IsDeleted = false;
                existingTag.ModifiedById = articleTag.ModifiedById;
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

        public List<ArticleTag> GetArticleTagsByCompany(int companyId)
        {
            using (var context = new PunchClockDbContext())
            {
                return context.ArticleTags.Where(x => x.IsDeleted == false && x.CompanyId == companyId).ToList();
            }
        }
    }
}
