using PunchClock.Cms.Model;
using PunchClock.Core.Models.Common;

namespace PunchClock.Cms.Contract
{
   public interface ICategoryService
   {
        Category Add(Category category);
        Category Update(Category category);
       AjaxResponse Delete(int catgeoryId);
   }
}
