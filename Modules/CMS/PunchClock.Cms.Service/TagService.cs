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
   public class TagService:ITags
    {
        public int Add(ArticleTag articleTag)
        {
            using (var context = new PunchClockDbContext())
            {
                articleTag.CreatedDate=DateTime.Now;;
                articleTag.IsDeleted = false;
                context.ArticleTags.Add(articleTag);
                context.SaveChanges();

            }
            return articleTag.Id;
        }

        public int Update(ArticleTag articleTag)
        {
            using (var context = new PunchClockDbContext())
            {
                articleTag.ModifiedDate = DateTime.Now; ;
                articleTag.IsDeleted = false;
                context.SaveChanges();

            }
            return articleTag.Id;
        }

        public bool Delete(int id)
        {
            using (var context = new PunchClockDbContext())
            {
                var tags = context.ArticleTags.FirstOrDefault(x => x.Id == id);
                if (tags == null) return false;
                tags.IsDeleted = true;
                tags.ModifiedDate = DateTime.Now;
                context.SaveChanges();
                return true;
            }
        }
    }
}
