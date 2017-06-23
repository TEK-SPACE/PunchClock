using System.Collections.Generic;
using PunchClock.Core.Models.Common;
using PunchClock.Ticketing.Model;
namespace PunchClock.Ticketing.Contracts
{
   public interface ITicketType
    {
        TicketStatus Add(TicketStatus status);
        TicketStatus Update(TicketStatus status);
        AjaxResponse Delete(int id);

        TicketType GetTicketTypeById(int id);
        List<TicketType> GetAllTicketType();
        List<TicketType> GetTickettypeByCompanyIdList(int companyId);
    }
}
