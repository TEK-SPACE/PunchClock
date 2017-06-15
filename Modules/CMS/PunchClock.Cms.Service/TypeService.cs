using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PunchClock.Cms.Contract;
using PunchClock.Cms.Model;
using PunchClock.Core.DataAccess;

namespace PunchClock.Cms.Service
{
    public class TypeService:IType
    {
        public int Add(ArticleType articleType)
        {
            using (var context = new PunchClockDbContext())
            {
                articleType.CreatedDate=DateTime.Now;
                articleType.IsDeleted = false;
                context.ArticleTypes.Add(articleType);
                context.SaveChanges();
            }
            return articleType.Id;
        }

        public int Update(ArticleType articleType)
        {
            using (var context = new PunchClockDbContext())
            {
                articleType.ModifiedDate = DateTime.Now;
                articleType.IsDeleted = false;
                context.SaveChanges();
            }
            return articleType.Id;
        }

        public bool Delete(int id)
        {
            using (var context = new PunchClockDbContext())
            {
                var articleType = context.ArticleTypes.FirstOrDefault(x => x.Id == id);
                if (articleType == null) return false;
                articleType.IsDeleted = true;
                articleType.ModifiedDate=DateTime.Now;
                return true;
            }
          
        }
    }
}
