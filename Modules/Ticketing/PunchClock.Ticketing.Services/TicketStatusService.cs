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
                var oldTicketStatus = context.TicketStatuses.FirstOrDefault(x => x.Id == status.Id);
                if (oldTicketStatus == null) return status;
                oldTicketStatus.Name = status.Name;
                oldTicketStatus.Description = status.Description;
                oldTicketStatus.DisplayOrder = status.DisplayOrder;
                oldTicketStatus.ModifiedById = status.ModifiedById;
                oldTicketStatus.ModifiedDateUtc = DateTime.UtcNow;
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
    }
}
