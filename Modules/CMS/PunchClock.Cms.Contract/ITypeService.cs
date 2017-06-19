using PunchClock.Core.Models.Common;
using Type = PunchClock.Cms.Model.Type;

namespace PunchClock.Cms.Contract
{
    public interface ITypeService
    {
        Type Add(Model.Type articleType);
        Type Update(Model.Type articleType);
        AjaxResponse Delete(int id);
    }
}