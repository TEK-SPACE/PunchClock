using System.Collections.Generic;
using PunchClock.Domain.Model;
using PunchClock.Ticketing.Model;
namespace PunchClock.Ticketing.Contracts
{
   public interface ITicketType
    {
        TicketType Add(TicketType status);
        TicketType Update(TicketType status);
        AjaxResponse Delete(int id);

        TicketType GetTicketTypeById(int id);
        List<TicketType> GetAllTicketType();
        List<TicketType> GetTickettypeByCompanyIdList(int companyId);
    }
}
