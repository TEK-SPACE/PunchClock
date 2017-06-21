using PunchClock.Core.Models.Common;
using PunchClock.Ticketing.Model;

namespace PunchClock.Ticketing.Contracts
{
    public interface ITicket
    {
        Ticket Add(Ticket ticket);
        Ticket Update(Ticket ticket);
        AjaxResponse Delete(int Id);
    }
}
