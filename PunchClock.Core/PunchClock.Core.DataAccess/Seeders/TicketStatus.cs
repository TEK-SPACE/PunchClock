using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using PunchClock.Ticketing.Model;

namespace PunchClock.Core.DataAccess.Seeders
{
    public partial class PunchDbInitializer 
    {
        private void SeedStatus(PunchClockDbContext context)
        {
            var satuses = new List<TicketStatus>
            {
                new TicketStatus {Id = 1, Name = "Planned", DisplayOrder =1,Description = "", CompanyId = 1},
                new TicketStatus {Id = 2, Name = "Ready", DisplayOrder =2, Description = "", CompanyId = 1},
                new TicketStatus {Id = 3, Name = "In Progress", DisplayOrder =3, Description = "", CompanyId = 1},
                new TicketStatus {Id = 4, Name = "Completed",  DisplayOrder =4,Description = "", CompanyId = 1},
                new TicketStatus {Id = 5, Name = "Re-Opened",  DisplayOrder =2,Description = "", CompanyId = 1},
                new TicketStatus {Id = 6, Name = "Resolved",  DisplayOrder =6,Description = "", CompanyId = 1},
                new TicketStatus {Id = 7, Name = "Closed", DisplayOrder =7, Description = "", CompanyId = 1},
            };
            foreach (var satus in satuses)
            {
                context.TicketStatuses.AddOrUpdate(satus);
            }
        }
    }
}