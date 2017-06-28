using System;
using System.Collections.Generic;
using System.Linq;
using PunchClock.Core.DataAccess;
using PunchClock.Ticketing.Contracts;
using PunchClock.Ticketing.Model;
using PunchClock.Domain.Model;

namespace PunchClock.Ticketing.Services
{
   public class TicketTypeService : ITicketType
    {
        public TicketType Add(TicketType type)
        {

            using (var context = new PunchClockDbContext())
            {
                context.TicketTypes.Add(type);
                context.SaveChanges();
            }
            return type;
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

        public TicketType Update(TicketType type)
        {
            using (var context = new PunchClockDbContext())
            {
                var oldTicketType = context.TicketTypes.FirstOrDefault(x => x.Id == type.Id);
                if (oldTicketType == null) return type;
                oldTicketType.Name = type.Name;
                oldTicketType.Description = type.Description;
                oldTicketType.DisplayOrder = type.DisplayOrder;
                oldTicketType.ModifiedById = type.ModifiedById;
                oldTicketType.ModifiedDateUtc = DateTime.UtcNow;
                context.SaveChanges();
            }
            return type;
        }
    }
}
