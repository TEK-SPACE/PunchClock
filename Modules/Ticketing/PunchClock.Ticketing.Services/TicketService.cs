using System;
using System.Collections.Generic;
using System.Linq;
using PunchClock.Core.DataAccess;
using PunchClock.Ticketing.Contracts;
using PunchClock.Ticketing.Model;
using PunchClock.Core.Models.Common;

namespace PunchClock.Ticketing.Services
{
    public class TicketService : ITicket
    {
        public Ticket Add(Ticket ticket)
        {
            using (PunchClockDbContext context = new PunchClockDbContext())
            {
                context.Tickets.Add(ticket);
                context.SaveChanges();
            }
            return ticket;
        }

        public List<Ticket> All()
        {
            using (var context = new PunchClockDbContext())
            {
                return context.Tickets.Where(x => !x.IsDeleted).ToList();
            }
        }
        

        public void Delete(Ticket ticket)
        {
            using (var context = new PunchClockDbContext())
            {
                var entity = context.Tickets.FirstOrDefault(x => x.Id == ticket.Id);
                if (ticket == null) return;
                if (entity != null) entity.IsDeleted = true;
                context.SaveChanges();
            }
        }

        public AjaxResponse Delete(int ticketId)
        {
            var response = new AjaxResponse
            {
                ResponseId = ticketId,
                ResponseText = "Record is not deleted",
                Success = false
            };
            using (var context = new PunchClockDbContext())
            {
                var ticket = context.Tickets.FirstOrDefault(x => x.Id == ticketId);
                if (ticket == null) return response;
                response.Success = true;
                response.ResponseText = "Data Deleted Successfully";
                return response;
            }
        }

        public List<TicketCategory> GetCategory()
        {
            using (var context = new PunchClockDbContext())
            {
                return context.TicketCategories.OrderBy(x => x.DisplayOrder).ToList();
            }
        }

        public List<TicketType> GeTicketType()
        {
            using (var context = new PunchClockDbContext())
            {
                return context.TicketTypes.OrderBy(x => x.DisplayOrder).ToList();
            }
        }

        public List<TicketPriority> GetPriortie()
        {
            using (var context = new PunchClockDbContext())
            {
                return context.TicketPriorities.OrderBy(x => x.DisplayOrder).ToList();
            }
        }

        public List<TicketStatus> GetStatus()
        {
            using (var context = new PunchClockDbContext())
            {
                return context.TicketStatuses.OrderBy(x => x.DisplayOrder).ToList();
            }
        }

        public Ticket Update(Ticket ticket)
        {
            using (var context = new PunchClockDbContext())
            {
                var oldTicket = context.Tickets.FirstOrDefault(x => x.Id == ticket.Id);
                if (oldTicket == null)
                    return ticket;
                oldTicket.StatusId = ticket.StatusId;
                oldTicket.Description = ticket.Description;
                oldTicket.Title = ticket.Title;
                oldTicket.Comments = ticket.Comments;
                context.SaveChanges();
            }
            return ticket;
        }
    }
}