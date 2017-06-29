using System.Collections.Generic;
using PunchClock.Domain.Model;
using PunchClock.Ticketing.Model;

namespace PunchClock.Ticketing.Contracts
{
    public interface ITicketStatus
    {
        TicketStatus Add(TicketStatus status);
        TicketStatus Update(TicketStatus status);
        AjaxResponse Delete(int id);
        TicketStatus GetTicketStatusById(int id);
        List<TicketStatus> GetAllTicketStatuses();
        List<TicketStatus> GetStatusByCompanyIdList(int companyId);
        void Delete(TicketStatus ticketStatus);
    }
}
