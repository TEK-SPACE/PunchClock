using PunchClock.Ticketing.Model;
using PunchClock.Domain.Model;

namespace PunchClock.Ticketing.Contracts
{
    public interface ITicketAttachment
    {
        TicketAttachment Add(TicketAttachment attachment);
        TicketAttachment Update(TicketAttachment atachment);
        AjaxResponse Delete(int Id);
    }
}
