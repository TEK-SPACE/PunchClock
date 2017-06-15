using System;
using System.Linq;
using PunchClock.Cms.Contract;
using PunchClock.Cms.Model;
using PunchClock.Core.DataAccess;

namespace PunchClock.Cms.Service
{
    public class CategoryService: ICategory
    {
        public int Add(Category category)
        {
            using (var context = new PunchClockDbContext())
            {
                category.CreatedDate = DateTime.Now;
                category.IsDeleted = false;
                context.Categrories.Add(category);
                context.SaveChanges();
            }
            return category.Id;
        }

        public int Update(Category category)
        {
            using (var context = new PunchClockDbContext())
            {
                category.ModifiedDate = DateTime.Now;
                category.IsDeleted = false;
                context.SaveChanges();
            }
            return category.Id;
        }

        public bool Delete(int catgeoryId)
        {

            using (var context = new PunchClockDbContext())
            {
                var category = context.Categrories.FirstOrDefault(x => x.Id == catgeoryId);
                if (category == null) return false;
                category.ModifiedDate = DateTime.Now;
                category.IsDeleted = true;
                context.SaveChanges();
                return true;
            }
           
        }
    }
}
