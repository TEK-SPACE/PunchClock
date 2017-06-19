using System;
using System.Linq;
using PunchClock.Cms.Contract;
using PunchClock.Cms.Model;
using PunchClock.Core.DataAccess;

namespace PunchClock.Cms.Service
{
    public class TypeService:ITypeService
    {
        public ArticleType Add(ArticleType articleType)
        {
            using (var context = new PunchClockDbContext())
            {
                articleType.CreatedDate=DateTime.Now;
                articleType.IsDeleted = false;
                context.ArticleTypes.Add(articleType);
                context.SaveChanges();
            }
            return articleType;
        }

        public ArticleType Update(ArticleType articleType)
        {
            using (var context = new PunchClockDbContext())
            {
                var existingType = context.ArticleTypes.FirstOrDefault(x => x.Id == articleType.Id);
                if (existingType == null) return articleType;
                existingType.Name = articleType.Name;
                existingType.Description = articleType.Description;
                existingType.ModifiedDate = DateTime.Now;
                existingType.IsDeleted = false;
                context.SaveChanges();
            }
            return articleType;
        }

        public CmsResponse Delete(int id)
        {
            var response = new CmsResponse
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
                articleType.ModifiedDate=DateTime.Now;
                response.ResponseText = "Article Type is Deleted.";
                response.Success = true;
                return response;
            }
          
        }
    }
}
