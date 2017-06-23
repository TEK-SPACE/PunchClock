using System;
using System.Linq;
using PunchClock.Core.DataAccess;
using PunchClock.Ticketing.Contracts;
using PunchClock.Ticketing.Model;
using PunchClock.Core.Models.Common;
using System.Collections.Generic;

namespace PunchClock.Ticketing.Services
{
    public class TicketCategoryService : ITicketCategory
    {
        //    public TicketCategory Add(TicketCategory category)
        //    {
        //        using (var context = new PunchClockDbContext())
        //        {
        //            category.CreatedDateUtc = DateTime.UtcNow;
        //            context.TicketCategories.Add(category);
        //            context.SaveChanges();
        //        }
        //        return category;
        //    }

        //    public AjaxResponse Delete(int id)
        //    {
        //        var response = new AjaxResponse
        //        {
        //            ResponseId = id,
        //            ResponseText = "Record is not deleted",
        //            Success = false
        //        };
        //        using (var context = new PunchClockDbContext())
        //        {
        //            var ticketcatagory = context.TicketCategories.FirstOrDefault(x => x.Id == id);
        //            if (ticketcatagory == null) return response;
        //            response.Success = true;
        //            response.ResponseText = "Data Deleted Successfully";
        //            return response;
        //        }
        //    }

        //   public TicketCategory Update(TicketCategory category)
        //    {
        //        using (var context = new PunchClockDbContext())
        //        {
        //            var oldCategory = context.TicketCategories.FirstOrDefault(x => x.Id == category.Id);
        //            if (oldCategory == null) return category;
        //            category.IsDeleted = false;
        //            category.ModifiedById = "ABC";
        //            category.CreatedDateUtc = DateTime.UtcNow;
        //            category.ModifiedDateUtc = DateTime.UtcNow;
        //            context.SaveChanges();
        //        }
        //        return category;
        //    }
        //}
        public TicketCategory Add(TicketCategory category)
        {
            throw new NotImplementedException();
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
                return context.TicketCategories.Where(x => x.IsDeleted == false && x.CompanyId == companyId).ToList();
            }
        }

        public TicketCategory GetTicketCategoryId(int id)
        {
            using (var context = new PunchClockDbContext())
            {
                return context.TicketCategories.FirstOrDefault(x => x.Id==id);
            }
        }

        public TicketCategory Update(TicketCategory category)
        {
            throw new NotImplementedException();
        }
    }
}
