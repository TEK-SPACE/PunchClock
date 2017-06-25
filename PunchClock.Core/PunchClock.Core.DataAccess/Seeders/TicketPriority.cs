using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using PunchClock.Ticketing.Model;

namespace PunchClock.Core.DataAccess.Seeders
{
    public partial class PunchDbInitializer
    {
        private void SeedPriority(PunchClockDbContext context)
        {
            var priority = new List<TicketPriority>
            {
                new TicketPriority {Id = 1, Name = "Urgent", Description = "", DisplayOrder = 1,CompanyId =context.Companies.First().Id},
                new TicketPriority {Id = 2, Name = "High", Description = "", DisplayOrder = 2,CompanyId = context.Companies.First().Id},
                new TicketPriority {Id = 3, Name = "Normal ", Description = "", DisplayOrder = 3,CompanyId = context.Companies.First().Id},
                new TicketPriority {Id = 4, Name = "Low", Description = "", DisplayOrder = 4,CompanyId = context.Companies.First().Id},
            };
            foreach (var pri in priority)
            {
                context.TicketPriorities.AddOrUpdate(pri);
            }
        }
    }
}
