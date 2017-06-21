using System;
using System.Linq;
using PunchClock.Cms.Contract;
using PunchClock.Cms.Model;
using PunchClock.Core.DataAccess;
using PunchClock.Core.Models.Common;
using PunchClock.Language.Model;

namespace PunchClock.Cms.Service
{
    public class CategoryService: ICategoryService
    {
        public ArticleCategory Add(ArticleCategory category)
        {
            using (var context = new PunchClockDbContext())
            {
                category.IsDeleted = false;
                context.ArticleCategrories.Add(category);
                context.SaveChanges();
            }
            AddArticleTyperesources(category);
            return category;
        }
        private void AddArticleTyperesources(ArticleCategory category)
        {
            if (category.Id <= 0) return;
            for (var i = 1; i <= 3; i++)
            {
                var culture = Culture.English;
                if (i == (int)Culture.Spanish)
                    culture = Culture.Spanish;
                if (i == (int)Culture.Hindi)
                    culture = Culture.Hindi;

                using (var context = new PunchClockDbContext())
                {

                    var categoryResources = new ArticleCategoryResource()
                    {
                        CategoryMasterId = category.Id,
                        Value = category.Name,
                        Culture = culture,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,

                    };

                    {
                        context.ArticleCategoryResources.Add(categoryResources);
                        context.SaveChanges();
                    }


                }
            }
        }

        public ArticleCategory Update(ArticleCategory category)
        {
            using (var context = new PunchClockDbContext())
            {
                var existCategory = context.ArticleCategrories.FirstOrDefault(x => x.Id == category.Id);
                if (existCategory == null) return category;
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
                category.IsDeleted = true;
                context.SaveChanges();
                response.ResponseText = "Category is Deleted.";
                response.Success = true;
                return response;
            }
           
        }
    }
}
