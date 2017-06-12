using System;
using PunchClock.Core.DataAccess;
using PunchClock.Ticketing.Contracts;
using PunchClock.Ticketing.Model;

namespace PunchClock.Ticketing.Services
{
    public class TicketService : ITicket
    {
        public int Add(Ticket ticket)
        {
            using (PunchClockDbContext context = new PunchClockDbContext())
            {
                context.Tickets.Add(ticket);
                context.SaveChanges();
            }
            return ticket.Id;
        }

        public bool Update(Ticket ticket)
        {
            throw new NotImplementedException();
        }
    }
}
