using System;
using System.Collections.Generic;
using System.Linq;
using PunchClock.Core.DataAccess;
using PunchClock.Ticketing.Contracts;
using PunchClock.Ticketing.Model;
using PunchClock.Core.Models.Common;

namespace PunchClock.Ticketing.Services
{
   public class TicketTypeService : ITicketType
    {
        public TicketStatus Add(TicketStatus status)
        {
            throw new NotImplementedException();
        }
        public AjaxResponse Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<TicketType> GetAllTicketType()
        {
            using (var context = new PunchClockDbContext())
            {
                return context.TicketTypes.Where(x => x.IsDeleted == false).ToList();
            }
        }

        public List<TicketType> GetTickettypeByCompanyIdList(int companyId)
        {
            using (var context = new PunchClockDbContext())
            {
                return context.TicketTypes.Where(x => x.IsDeleted == false && x.CompanyId == companyId).ToList();
            }
        }

        public TicketType GetTicketTypeById(int id)
        {
            using (var context = new PunchClockDbContext())
            {
                return context.TicketTypes.FirstOrDefault(x => x.Id == id);
            }
        }

        public TicketStatus Update(TicketStatus status)
        {
            throw new NotImplementedException();
        }
    }
}
