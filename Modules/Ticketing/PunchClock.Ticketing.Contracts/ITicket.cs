using System.Collections.Generic;
using PunchClock.Domain.Model;
using PunchClock.Ticketing.Model;

namespace PunchClock.Ticketing.Contracts
{
    public interface ITicket
    {
        Ticket Add(Ticket ticket);
        Ticket Update(Ticket ticket);
        AjaxResponse Delete(int id);
        List<Ticket> All();
        void Delete(Ticket ticket);

        List<TicketStatus> GetStatusus(int companyId);
        List<TicketPriority> GetPriorties(int companyId);
        List<TicketCategory> GetCategories(int companyId);
        List<TicketType> GetTypes(int companyId);
        List<TicketProject> GetProjects(int companyId);
        Ticket Details(int id);
    }
}
