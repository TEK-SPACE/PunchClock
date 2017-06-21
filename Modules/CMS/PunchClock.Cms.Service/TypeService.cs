using System;
using System.Collections.Generic;
using System.Linq;
using PunchClock.Cms.Contract;
using PunchClock.Core.DataAccess;
using PunchClock.Core.Models.Common;
using PunchClock.Language.Model;
using ArticleType = PunchClock.Cms.Model.ArticleType;

namespace PunchClock.Cms.Service
{
    public class TypeService:ITypeService
    {
        public ArticleType Add(ArticleType articleType)
        {
         
                using (var context = new PunchClockDbContext())
                {
                    articleType.IsDeleted = false;
                    context.ArticleTypes.Add(articleType);
                    context.SaveChanges();
                }
            
            AddArticleTyperesources(articleType);

            return articleType;
        }

        private void AddArticleTyperesources(ArticleType articleType)
        {
            if (articleType.Id <= 0) return;
            for (var i = 1; i <= 3; i++)
            {
                var culture = Culture.English;
                if(i==(int)Culture.Spanish)
                    culture=Culture.Spanish;
                if(i==(int)Culture.Hindi)
                    culture=Culture.Hindi;

                using (var context = new PunchClockDbContext())
                {

                    var typeResource = new ArticleTypeResource
                    {
                        TypeMasterId = articleType.Id,
                        Value = articleType.Name,
                        Culture = culture,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        
                    };
                   
                    {
                        context.ArticleTypeResources.Add(typeResource);
                        context.SaveChanges();
                    }


                }
            }
        }

        public ArticleType Update(ArticleType articleType)
        {
            using (var context = new PunchClockDbContext())
            {
                var existingType = context.ArticleTypes.FirstOrDefault(x => x.Id == articleType.Id);
                if (existingType == null) return articleType;
                existingType.Name = articleType.Name;
                existingType.Description = articleType.Description;
                existingType.IsDeleted = false;
                context.SaveChanges();
            }
            return articleType;
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
                var articleType = context.ArticleTypes.FirstOrDefault(x => x.Id == id);
                if (articleType == null) return response;
                articleType.IsDeleted = true;
                response.ResponseText = "Article Type is Deleted.";
                response.Success = true;
                return response;
            }
          
        }
    }
}
