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
            AddArticleCategoryResources(category);
            return category;
        }
        private void AddArticleCategoryResources(ArticleCategory category)
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
                        LastModifiedBy = category.LastModifiedBy

                    };
                        context.ArticleCategoryResources.Add(categoryResources);
                        context.SaveChanges();
                    


                }
            }
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

        public ArticleCategory GetOneArticleCategory(int id)
        {
            using (var context = new PunchClockDbContext())
            {
                return context.ArticleCategrories.FirstOrDefault(x => x.Id == id);
            }
        }

        public List<ArticleCategory> GetAllArticleCategories()
        {
            using (var context = new PunchClockDbContext())
            {
                return context.ArticleCategrories.Where(x=>x.IsDeleted==false).ToList();
            }
        }

        public List<ArticleCategory> GetCategoriesByCompanyId(int companyId)
        {
            using (var context = new PunchClockDbContext())
            {
                return context.ArticleCategrories.Where(x => x.IsDeleted == false && x.CompanyId == companyId).ToList();
            }
        }
    }
}
