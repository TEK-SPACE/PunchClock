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
            var satuses = new List<Status>
            {
                new Status
                {
                    Id = 1,
                    Name = "New",
                    Description = "",
                    CreatedDate = DateTime.Now,
                    LastModifiedByGuid = null,
                    ModifiedDate = DateTime.Now
                },
                new Status
                {
                    Id = 2,
                    Name = "Active",
                    Description = "",
                    CreatedDate = DateTime.Now,
                    LastModifiedByGuid = null,
                    ModifiedDate = DateTime.Now
                }
            };
            foreach (var satus in satuses)
            {
                context.Statuses.AddOrUpdate(satus);
            }
        }
    }
}