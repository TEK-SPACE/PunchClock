using System.Collections.Generic;
using System.Data.Entity.Migrations;
using PunchClock.Domain.Model;

namespace PunchClock.Core.DataAccess.Seeders
{
    public partial class PunchDbInitializer
    {
        private void SeedEmploymentType(PunchClockDbContext context)
        {
            List<EmploymentType> employementTypes = new List<EmploymentType>
            {
                new EmploymentType {Id = 1, Name = "Full Time"},
                new EmploymentType {Id = 2, Name = "Contract Hourly"},
                new EmploymentType {Id = 3, Name = "Contract Flat"},
                new EmploymentType {Id = 4, Name = "Part Time" }
            };
            foreach (var employmentType in employementTypes)
            {
                context.EmploymentTypes.AddOrUpdate(x => x.Id, employmentType);
            }
        }
    }
}