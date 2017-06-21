using PunchClock.Core.Models.Common;
using PunchClock.Ticketing.Model;

namespace PunchClock.Ticketing.Contracts
{
    public interface ITicketStatus
    {
        TicketStatus Add(TicketStatus status);
        TicketStatus Update(TicketStatus status);
        AjaxResponse Delete(int id);
    }
}
