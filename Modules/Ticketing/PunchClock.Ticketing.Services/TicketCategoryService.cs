using System;
using System.Linq;
using PunchClock.Core.DataAccess;
using PunchClock.Ticketing.Contracts;
using PunchClock.Ticketing.Model;
using PunchClock.Domain.Model;
using System.Collections.Generic;
using System.Data.Entity;

namespace PunchClock.Ticketing.Services
{
    public class TicketCategoryService : ITicketCategory
    {
        public TicketCategory Add(TicketCategory category)
        {
            using (var context = new PunchClockDbContext())
            {
                context.TicketCategories.Add(category);
                context.SaveChanges();
                category = context.TicketCategories.Include(x => x.TicketProject).FirstOrDefault(x => x.Id == category.Id); ;
            }
            return category;
        }

        public AjaxResponse Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<TicketCategory> GetAllTicketCategry()
        {
            using (var context = new PunchClockDbContext())
            {
                return context.TicketCategories.Where(x => x.IsDeleted == false).ToList();
            }
        }

        public List<TicketCategory> GetCategoryByCompanyIdList(int companyId)
        {
            using (var context = new PunchClockDbContext())
            {
                return
                    context.TicketCategories.Where(x => x.IsDeleted == false && x.CompanyId == companyId)
                        .Include(x => x.TicketProject)
                        .ToList();
            }
        }

        public void Delete(TicketCategory category)
        {
            using (var context = new PunchClockDbContext())
            {
                var ticketCategory = context.TicketCategories.FirstOrDefault(x => x.Id == category.Id);
                if (ticketCategory == null) return;
                ticketCategory.ModifiedById = category.ModifiedById;
               ticketCategory.ModifiedDateUtc = DateTime.UtcNow;
                ticketCategory.IsDeleted = true;
                context.SaveChanges();
            }
        }

        public TicketCategory GetTicketCategoryId(int id)
        {
            using (var context = new PunchClockDbContext())
            {
                return context.TicketCategories.FirstOrDefault(x => x.Id == id);
            }
        }

        public TicketCategory Update(TicketCategory category)
        {
            using (var context = new PunchClockDbContext())
            {
                var ticketCategory = context.TicketCategories.Include(x=>x.TicketProject).FirstOrDefault(x => x.Id == category.Id);
                if (ticketCategory == null) return category;
                ticketCategory.Name = category.Name;
                ticketCategory.Description = category.Description;
                ticketCategory.ModifiedById = category.ModifiedById;
                ticketCategory.ProjectId = category.ProjectId;
                 ticketCategory.ModifiedDateUtc = DateTime.UtcNow;
                context.SaveChanges();
                category = context.TicketCategories.Include(x => x.TicketProject).FirstOrDefault(x => x.Id == category.Id); ;
            }
            return category;
        }
    }
}