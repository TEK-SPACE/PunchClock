using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using PunchClock.Ticketing.Model;

namespace PunchClock.Core.DataAccess.Seeders
{
    public partial class PunchDbInitializer 
    {
        private void SeedStatus(PunchClockDbContext context)
        {
            var satuses = new List<TicketStatus>
            {
                new TicketStatus {Id = 1, Name = "Planning", IsCoreItem = true,DisplayOrder =1,Description = "", CompanyId =context.Companies.First().Id},
                new TicketStatus {Id = 2, Name = "Ready", IsCoreItem = true,DisplayOrder =2, Description = "", CompanyId = context.Companies.First().Id},
                new TicketStatus {Id = 3, Name = "In Progress",IsCoreItem = true, DisplayOrder =3, Description = "", CompanyId = context.Companies.First().Id},
                new TicketStatus {Id = 4, Name = "Completed", IsCoreItem = true, DisplayOrder =4,Description = "", CompanyId = context.Companies.First().Id},
                new TicketStatus {Id = 5, Name = "Re-Opened", IsCoreItem = true, DisplayOrder =2,Description = "", CompanyId = context.Companies.First().Id},
                new TicketStatus {Id = 6, Name = "Resolved", IsCoreItem = true, DisplayOrder =6,Description = "", CompanyId = context.Companies.First().Id},
                new TicketStatus {Id = 7, Name = "Closed",IsCoreItem = true, DisplayOrder =7, Description = "", CompanyId = 1},
            };
            foreach (var satus in satuses)
            {
                context.TicketStatuses.AddOrUpdate(satus);
            }
        }
    }
}