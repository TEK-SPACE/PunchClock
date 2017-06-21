using PunchClock.Core.Models.Common;
using PunchClock.Ticketing.Model;

namespace PunchClock.Ticketing.Contracts
{
    public interface ITicketPriority
    {
        TicketPriority Add(TicketPriority priority);
        TicketPriority Update(TicketPriority priority);
        AjaxResponse Delete(int id);
    }
}
