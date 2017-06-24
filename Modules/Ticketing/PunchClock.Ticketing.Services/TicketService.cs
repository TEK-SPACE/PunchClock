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

        public List<TicketCategory> GetCategories(int companyId)
        {
            using (var context = new PunchClockDbContext())
            {
                return context.TicketCategories.Where(x => x.CompanyId == companyId).OrderBy(x => x.DisplayOrder)
                    .ToList();
            }
        }

        public List<TicketProject> GetProjects(int companyId)
        {
            using (var context = new PunchClockDbContext())
            {
                return context.TicketProjects.Where(x => x.CompanyId == companyId).OrderBy(x => x.DisplayOrder)
                    .ToList();
            }
        }

        public List<TicketType> GetTypes(int companyId)
        {
            using (var context = new PunchClockDbContext())
            {
                return context.TicketTypes.Where(x => x.CompanyId == companyId).OrderBy(x => x.DisplayOrder).ToList();
            }
        }

        public List<TicketPriority> GetPriorties(int companyId)
        {
            using (var context = new PunchClockDbContext())
            {
                return context.TicketPriorities.Where(x => x.CompanyId == companyId).OrderBy(x => x.DisplayOrder).ToList();
            }
        }

        public List<TicketStatus> GetStatusus(int companyId)
        {
            using (var context = new PunchClockDbContext())
            {
                return context.TicketStatuses.Where(x=>x.CompanyId == companyId ).OrderBy(x => x.DisplayOrder).ToList();
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