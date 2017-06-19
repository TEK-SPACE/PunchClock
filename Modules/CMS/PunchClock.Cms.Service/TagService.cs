using System;
using System.Linq;
using PunchClock.Cms.Contract;
using PunchClock.Cms.Model;
using PunchClock.Core.DataAccess;
using PunchClock.Core.Models.Common;

namespace PunchClock.Cms.Service
{
   public class TagService: ITagsService
    {
        public Tag Add(Tag articleTag)
        {
            using (var context = new PunchClockDbContext())
            {
                articleTag.CreatedDate=DateTime.Now;
                articleTag.IsDeleted = false;
                context.ArticleTags.Add(articleTag);
                context.SaveChanges();

            }
            return articleTag;
        }

        public Tag Update(Tag articleTag)
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
    }
}
