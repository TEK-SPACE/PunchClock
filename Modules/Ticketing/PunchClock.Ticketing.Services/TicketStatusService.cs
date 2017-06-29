using System;
using System.Collections.Generic;
using System.Linq;
using PunchClock.Core.DataAccess;
using PunchClock.Ticketing.Contracts;
using PunchClock.Ticketing.Model;
using PunchClock.Domain.Model;

namespace PunchClock.Ticketing.Services
{
    public class TicketStatusService : ITicketStatus
    {
        public TicketStatus Add(TicketStatus status)
        {
            using (var context = new PunchClockDbContext())
            {
                context.TicketStatuses.Add(status);
                context.SaveChanges();
            }
            return status;
        }

        public AjaxResponse Delete(int id)
        {
            throw new NotImplementedException();
        }

        public TicketStatus Update(TicketStatus status)
        {
            using (var context = new PunchClockDbContext())
            {
                var ticketStatus = context.TicketStatuses.FirstOrDefault(x => x.Id == status.Id);
                if (ticketStatus == null) return status;
                ticketStatus.Name = status.Name;
                ticketStatus.Description = status.Description;
                ticketStatus.DisplayOrder = status.DisplayOrder;
                ticketStatus.ModifiedById = status.ModifiedById;
                ticketStatus.ModifiedDateUtc = DateTime.UtcNow;
                context.SaveChanges();
            }
            return status;
        }

        public TicketStatus GetTicketStatusById(int id)
        {
            using (var context = new PunchClockDbContext())
            {
                return context.TicketStatuses.FirstOrDefault(x => x.Id == id);
            }
        }

        public List<TicketStatus> GetAllTicketStatuses()
        {
            using (var context = new PunchClockDbContext())
            {
                return context.TicketStatuses.Where(x => x.IsDeleted == false).ToList();
            }
        }

        public List<TicketStatus> GetStatusByCompanyIdList(int companyId)
        {
            using (var context = new PunchClockDbContext())
            {
                return context.TicketStatuses.Where(x => x.IsDeleted == false && x.CompanyId == companyId).ToList();
            }
        }

        public void Delete(TicketStatus status)
        {
            using (var context = new PunchClockDbContext())
            {
                var ticketStatus = context.TicketStatuses.FirstOrDefault(x => x.Id == status.Id);
                if (ticketStatus == null) return;
                ticketStatus.ModifiedById = status.ModifiedById;
                ticketStatus.ModifiedDateUtc = DateTime.UtcNow;
                ticketStatus.IsDeleted = true;
                context.SaveChanges();
            }
        }
    }
}