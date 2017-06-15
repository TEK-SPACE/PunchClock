using PunchClock.Cms.Model;

namespace PunchClock.Cms.Contract
{
   public interface ICategory
   {
       int Add(Category category);
       int Update(Category category);
       bool Delete(int catgeoryId);
   }
}
