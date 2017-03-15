using System.Collections.Generic;
using System.Data.Entity.Migrations;
using PunchClock.Domain.Model;

namespace PunchClock.DAL
{
    public class PunchDbInitializer : DbMigrationsConfiguration<PunchClockDbContext>
    {

        public PunchDbInitializer()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
        protected override void Seed(PunchClockDbContext context)
        {
            SeedEmploymentType(context);
            base.Seed(context);
        }

        private void SeedEmploymentType(PunchClockDbContext context)
        {
            List<EmploymentType> employementTypes = new List<EmploymentType>
            {
                new EmploymentType {Id = 1, Name = "Full Time"},
                new EmploymentType {Id = 2, Name = "Full Time Contractor"},
                new EmploymentType {Id = 1, Name = "Part Time"}
            };
            foreach (var employmentType in employementTypes)
            {
                context.EmploymentTypes.Add(employmentType);
            }
        }
    }
}
