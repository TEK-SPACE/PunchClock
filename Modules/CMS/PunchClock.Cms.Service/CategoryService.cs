using System;
using System.Linq;
using PunchClock.Cms.Contract;
using PunchClock.Cms.Model;
using PunchClock.Core.DataAccess;
using PunchClock.Core.Models.Common;

namespace PunchClock.Cms.Service
{
    public class CategoryService: ICategoryService
    {
        public ArticleCategory Add(ArticleCategory category)
        {
            using (var context = new PunchClockDbContext())
            {
                category.CreatedDate = DateTime.Now;
                category.IsDeleted = false;
                context.ArticleCategrories.Add(category);
                context.SaveChanges();
            }
            return category;
        }

        public ArticleCategory Update(ArticleCategory category)
        {
            using (var context = new PunchClockDbContext())
            {
                var existCategory = context.ArticleCategrories.FirstOrDefault(x => x.Id == category.Id);
                if (existCategory == null) return category;
                existCategory.ModifiedDate = DateTime.Now;
                existCategory.IsDeleted = false;
                existCategory.Description = category.Description;
                existCategory.Name = category.Name;
                context.SaveChanges();
            }
            return category;
        }

        public AjaxResponse Delete(int catgeoryId)
        {
            var response = new AjaxResponse
            {
                ResponseId = catgeoryId,
                ResponseText = "Record is not deleted",
                Success = false
            };
            using (var context = new PunchClockDbContext())
            {
                var category = context.ArticleCategrories.FirstOrDefault(x => x.Id == catgeoryId);
                if (category == null) return response;
                category.ModifiedDate = DateTime.Now;
                category.IsDeleted = true;
                context.SaveChanges();
                response.ResponseText = "Category is Deleted.";
                response.Success = true;
                return response;
            }
           
        }
    }
}
