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
    public class CategoryService: ICategoryService
    {
        public ArticleCategory Add(ArticleCategory category)
        {
            using (var context = new PunchClockDbContext())
            {
                context.ArticleCategrories.Add(category);
                context.ArticleCategoryResources.AddRange(category.Resources);
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
                existCategory.IsDeleted = false;
                existCategory.Description = category.Description;
                existCategory.Name = category.Name;
                existCategory.ModifiedById = category.ModifiedById;
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

        public List<ArticleCategory> GetArticleCategoriesByCompanyId(int companyId)
        {
            using (var context = new PunchClockDbContext())
            {
                return context.ArticleCategrories.Where(x => x.IsDeleted == false && x.CompanyId == companyId).ToList();
            }
        }
    }
}
