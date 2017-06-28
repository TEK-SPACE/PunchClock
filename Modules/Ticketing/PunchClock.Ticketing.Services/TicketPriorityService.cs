using System;
using PunchClock.Core.DataAccess;
using PunchClock.Ticketing.Contracts;
using PunchClock.Ticketing.Model;
using PunchClock.Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace PunchClock.Ticketing.Services
{
   public class TicketPriorityService : ITicketPriority
    {
        public TicketPriority Add(TicketPriority priority)
        {
            using (var context = new PunchClockDbContext())
            {
                context.TicketPriorities.Add(priority);
                context.SaveChanges();
            }
            return priority;
        }

        public AjaxResponse Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public List<TicketPriority> GetAllTicketPriority()
        {
            using (var  context=new PunchClockDbContext())
            {
                return context.TicketPriorities.Where(x => x.IsDeleted == false).ToList();
            }
        }

        public List<TicketPriority> GetPriorityByCompanyIdList(int companyId)
        {
            using (var context = new PunchClockDbContext())
            {
                return context.TicketPriorities.Where(x => x.IsDeleted == false && x.CompanyId == companyId).ToList();
            }
        }
        public TicketPriority GetTicketPriorityId(int id)
        {
            using (var context = new PunchClockDbContext())
            {
                return context.TicketPriorities.FirstOrDefault(x => x.Id == id);
            }
        }
        public TicketPriority Update(TicketPriority priority)
        {
            using (var context = new PunchClockDbContext())
            {
                var oldPriority = context.TicketPriorities.FirstOrDefault(x => x.Id == priority.Id);
                if(oldPriority==null)return priority;
                oldPriority.Name = priority.Name;
                oldPriority.Description = priority.Description;
                oldPriority.DisplayOrder = priority.DisplayOrder;
                oldPriority.ModifiedById = priority.ModifiedById;
                oldPriority.ModifiedDateUtc=DateTime.UtcNow;
               context.SaveChanges();
            }
            return priority;
        }
    }
}
