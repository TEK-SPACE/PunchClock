using System.Collections.Generic;
using PunchClock.Domain.Model;
using PunchClock.Ticketing.Model;

namespace PunchClock.Ticketing.Contracts
{
    public interface ITicketPriority
    {
        TicketPriority Add(TicketPriority priority);
        TicketPriority Update(TicketPriority priority);
        AjaxResponse Delete(int id);
        TicketPriority GetTicketPriorityId(int id);
        List<TicketPriority> GetAllTicketPriority();
        List<TicketPriority> GetPriorityByCompanyIdList(int companyId);
        void Delete(TicketPriority ticketPriority);
    }
}
