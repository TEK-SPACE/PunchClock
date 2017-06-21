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
                new TicketStatus
                {
                    Id = 1,
                    Name = "New",
                    Description = "",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new TicketStatus
                {
                    Id = 2,
                    Name = "Active",
                    Description = "",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                }
            };
            foreach (var satus in satuses)
            {
                context.TicketStatuses.AddOrUpdate(satus);
            }
        }
    }
}