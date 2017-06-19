using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using PunchClock.Domain.Model;

namespace PunchClock.Core.DataAccess.Seeders
{
    public partial class PunchDbInitializer
    {
        private void SeedCompany(PunchClockDbContext context)
        {
            List<Company> companies = new List<Company>
            {
                new Company {Id=1,Name="TEKSPACE",RegisterCode="7493582" ,GlobalId=Guid.NewGuid()}
            };
            foreach (var company in companies)
            {
                context.Companies.AddOrUpdate(company);
            }
        }
    }
}