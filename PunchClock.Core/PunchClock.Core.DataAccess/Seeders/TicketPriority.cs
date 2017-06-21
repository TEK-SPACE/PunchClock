using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using PunchClock.Ticketing.Model;

namespace PunchClock.Core.DataAccess.Seeders
{
    public partial class PunchDbInitializer
    {
        private void SeedPriority(PunchClockDbContext context)
        {
            var priority = new List<TicketPriority>
            {
                new TicketPriority {Id = 1, Name = "Urgent", Description = "", DisplayOrder = 1, CompanyId = 1},
                new TicketPriority {Id = 2, Name = "High", Description = "", DisplayOrder = 2, CompanyId = 1},
                new TicketPriority {Id = 3, Name = "Normal ", Description = "", DisplayOrder = 3, CompanyId = 1},
                new TicketPriority {Id = 4, Name = "Low", Description = "", DisplayOrder = 4, CompanyId = 1},
            };
            foreach (var pri in priority)
            {
                context.TicketPrioritys.AddOrUpdate(pri);
            }
        }
    }
}
