using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PunchClock.Domain.Model;
using EmploymentType = PunchClock.Core.Models.Common.Enum.EmploymentType;

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
                new UserType {Id = (int)Core.Models.Common.Enum.UserType.HumanResources,Description="Human Resource"}
            };
            foreach (var userType in userTypes)
            {
                context.UserTypes.AddOrUpdate(userType);
            }
        }

        private void SeedUsers(PunchClockDbContext context)
        {

            string[] roles = new string[] { "Administrator", "Manager", "Editor", "Buyer", "Business", "Seller", "Subscriber" };

            foreach (string role in roles)
            {
                var roleStore = new RoleStore<IdentityRole>(context);

                if (!context.Roles.Any(r => r.Name == role))
                {
                    roleStore.CreateAsync(new IdentityRole(role)).Wait();
                }
            }


            List<User> users = new List<User>
            {
                new User { FirstName = "Super",
                    LastName = "Admin",
                    Email = "superadmin@rbtekspace.com",
                    UserName = "superadmin",
                    PhoneNumber = "5167493582",
                    RegisteredTimeZone = "India Standard Time",
                    UserTypeId = (int)Core.Models.Common.Enum.UserType.Admin,
                    EmploymentTypeId = (int)EmploymentType.FullTime,
                    CompanyId = context.Companies.First().Id,
                    IsActive = true,
                    PasswordLastChanged = DateTime.Now,
                    PasswordDisabled = false,
                    IsDeleted = false,
                    DateCreatedUtc = DateTime.UtcNow,
                    LastUpdatedUtc = DateTime.UtcNow,
                    LastActivityDateUtc = DateTime.UtcNow,
                    RegistrationCode = context.Companies.First()?.RegisterCode,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    TwoFactorEnabled = false,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    SecurityStamp = Guid.NewGuid().ToString("D")}
            };
            

            foreach (var user in users)
            {
                //System.Diagnostics.Debugger.Launch();
               
                try
                {
                    if (!context.Users.Any(u => u.UserName == user.UserName))
                    {
                        var password = new PasswordHasher();
                        var hashed = password.HashPassword("TEK@Ind516");
                        user.PasswordHash = hashed;

                        var userStore = new UserStore<User>(context);
                        userStore.CreateAsync(user).Wait();
                        userStore.AddToRoleAsync(user, "Administrator").Wait();

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}