using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using PunchClock.Ticketing.Model;

namespace PunchClock.Core.DataAccess.Seeders
{
    public partial class PunchDbInitializer
    {
        private void SeedProjects(PunchClockDbContext context)
        {
            var projects = new List<TicketProject>
            {
                new TicketProject {Id = 1, Name = "Human Resources", IsCoreItem = true,Description = "", DisplayOrder = 1,CompanyId =context.Companies.First().Id},
                new TicketProject {Id = 2, Name = "Marketing", IsCoreItem = true,Description = "", DisplayOrder = 1,CompanyId =context.Companies.First().Id},
                new TicketProject {Id = 3, Name = "PunchClock", Description = "", DisplayOrder = 1,CompanyId =context.Companies.First().Id}
            };
            foreach (var project in projects)
            {
                context.TicketProjects.AddOrUpdate(project);
            }
        }
    }
}