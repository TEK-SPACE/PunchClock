using System.Collections.Generic;
using System.Data.Entity.Migrations;
using PunchClock.Domain.Model;

namespace PunchClock.Core.DataAccess.Seeders
{
    public partial class PunchDbInitializer
    {
        private void SeedUserType(PunchClockDbContext context)
        {
            List<UserType> userTypes = new List<UserType>
            {
                new UserType {Id = (int)Core.Models.Common.Enum.UserType.Employee, Description="Employee"},
                new UserType {Id = (int)Core.Models.Common.Enum.UserType.Manager, Description = "Manager"},
                new UserType {Id = (int)Core.Models.Common.Enum.UserType.CompanyAdmin, Description = "CompanyAdmin"},
                new UserType {Id = (int)Core.Models.Common.Enum.UserType.Admin, Description = "Admin"},
                new UserType {Id = (int)Core.Models.Common.Enum.UserType.HumanResources,Description="Human Resource" }
            };
            foreach (var userType in userTypes)
            {
                context.UserTypes.AddOrUpdate(userType);
            }
        }
    }
}