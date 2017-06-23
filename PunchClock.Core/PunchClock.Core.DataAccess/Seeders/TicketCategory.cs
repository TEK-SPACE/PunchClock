using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using PunchClock.Ticketing.Model;

namespace PunchClock.Core.DataAccess.Seeders
{
    public partial class PunchDbInitializer
    {
        private void SeedTicketCategory(PunchClockDbContext context)
        {   
            var ticketcat = new List<TicketCategory>
            {
                new TicketCategory {Id = 1, Name = "American Greetings", Description ="",CompanyId =context.Companies.First().Id,DisplayOrder = 1},
                new TicketCategory {Id = 2, Name = "GerMedUSA", Description ="",CompanyId =context.Companies.First().Id,DisplayOrder = 2},
                new TicketCategory {Id = 3, Name = "OpenAir", Description ="",CompanyId =context.Companies.First().Id,DisplayOrder = 3},
                new TicketCategory {Id = 4, Name = "Ametek", Description ="",CompanyId =context.Companies.First().Id,DisplayOrder = 4},
                new TicketCategory {Id = 5, Name = "ACCO Brands", Description ="",CompanyId =context.Companies.First().Id,DisplayOrder = 5},
                new TicketCategory {Id = 6, Name = "Cape Air", Description ="",CompanyId =context.Companies.First().Id,DisplayOrder = 6},
            };
            foreach (var category in ticketcat)
            {
                context.TicketCategories.AddOrUpdate(category);
            }
        }
    }
}