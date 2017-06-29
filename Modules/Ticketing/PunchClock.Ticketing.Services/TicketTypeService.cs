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

        public void Delete(TicketType type)
        {
            using (var context = new PunchClockDbContext())
            {
                var ticketType = context.TicketTypes.FirstOrDefault(x => x.Id == type.Id);
                if (ticketType == null) return;
                ticketType.Name = type.Name;
                ticketType.ModifiedById = type.ModifiedById;
                ticketType.ModifiedDateUtc = DateTime.UtcNow;
                ticketType.IsDeleted = true;
                context.SaveChanges();
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
                var ticketType = context.TicketTypes.FirstOrDefault(x => x.Id == type.Id);
                if (ticketType == null) return type;
                ticketType.Name = type.Name;
                ticketType.Description = type.Description;
                ticketType.DisplayOrder = type.DisplayOrder;
                ticketType.ModifiedById = type.ModifiedById;
                ticketType.ModifiedDateUtc = DateTime.UtcNow;
                context.SaveChanges();
            }
            return type;
        }
    }
}