using PunchClock.Cms.Model;
using PunchClock.Core.Models.Common;

namespace PunchClock.Cms.Contract
{
    public interface ITagsService
    {
        Tag Add(Tag articleTag);
        Tag Update(Tag articleTag);
        AjaxResponse Delete(int id);
    }
}
