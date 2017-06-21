using System;
using System.Collections.Generic;
using System.Linq;
using PunchClock.Core.DataAccess;
using PunchClock.Ticketing.Contracts;
using PunchClock.Ticketing.Model;
using PunchClock.Core.Models.Common;

namespace PunchClock.Ticketing.Services
{
   public class TicketStatusService : ITicketStatus
    {
        public TicketStatus Add(TicketStatus status)
        {
            throw new NotImplementedException();
        }

        public AjaxResponse Delete(int id)
        {
            throw new NotImplementedException();
        }

        public TicketStatus Update(TicketStatus status)
        {
            throw new NotImplementedException();
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
                return context.TicketStatuses.Where(x => x.IsDeleted == false && x.CompanyId==companyId).ToList();
            }
        }
    }
}
