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
                },
                new TicketStatus
                {
                    Id = 2,
                    Name = "Active",
                    Description = "",
                }
            };
            foreach (var satus in satuses)
            {
                context.TicketStatuses.AddOrUpdate(satus);
            }
        }
    }
}