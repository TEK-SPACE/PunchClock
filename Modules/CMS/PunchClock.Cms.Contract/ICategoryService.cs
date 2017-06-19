using PunchClock.Cms.Model;

namespace PunchClock.Cms.Contract
{
   public interface ICategoryService
   {
        Category Add(Category category);
        Category Update(Category category);
        CmsResponse Delete(int catgeoryId);
   }
}
