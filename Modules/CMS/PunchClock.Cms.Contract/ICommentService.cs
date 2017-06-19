using PunchClock.Cms.Model;
using PunchClock.Core.Models.Common;

namespace PunchClock.Cms.Contract
{
    public interface ICommentService
    {
        Comment Add(Comment comment);
        Comment Update(Comment comments);
        AjaxResponse Delete(int id);
    }
}
