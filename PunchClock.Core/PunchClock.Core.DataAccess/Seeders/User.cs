using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PunchClock.Domain.Model;
using EmploymentType = PunchClock.Domain.Model.Enum.EmploymentType;

namespace PunchClock.Core.DataAccess.Seeders
{
    public partial class PunchDbInitializer
    {
        private void SeedUserType(PunchClockDbContext context)
        {
            List<UserType> userTypes = new List<UserType>
            {
                new UserType {Id = (int)Domain.Model.Enum.UserType.Employee, Description="Employee"},
                new UserType {Id = (int)Domain.Model.Enum.UserType.Manager, Description = "Manager"},
                new UserType {Id = (int)Domain.Model.Enum.UserType.CompanyAdmin, Description = "CompanyAdmin"},
                new UserType {Id = (int)Domain.Model.Enum.UserType.SuperAdmin, Description = "SuperAdmin"},
                new UserType {Id = (int)Domain.Model.Enum.UserType.HumanResources,Description="Human Resource"},
                new UserType {Id = (int)Domain.Model.Enum.UserType.ProjectOwner,Description="Project Owner"},
                new UserType {Id = (int)Domain.Model.Enum.UserType.Guest,Description="Anonymous User"}


            };
            foreach (var userType in userTypes)
            {
                context.UserTypes.AddOrUpdate(userType);
            }
        }

        private void SeedUsers(PunchClockDbContext context)
        {
            var superUser = new User
            {
                FirstName = "Super",
                LastName = "Admin",
                Email = "superadmin@rbtekspace.com",
                UserName = "superadmin",
                PhoneNumber = "5167493582",
                RegisteredTimeZone = "India Standard Time",
                UserTypeId = (int) Domain.Model.Enum.UserType.SuperAdmin,
                EmploymentTypeId = (int) EmploymentType.FullTime,
                CompanyId = _firstCompany.Id,
                IsActive = true,
                PasswordLastChanged = DateTime.Now,
                PasswordDisabled = false,
                IsDeleted = false,
                DateCreatedUtc = DateTime.UtcNow,
                LastUpdatedUtc = DateTime.UtcNow,
                LastActivityDateUtc = DateTime.UtcNow,
                RegistrationCode = _firstCompany.RegisterCode,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

           var  companyadmin = superUser;
            companyadmin.FirstName = "Company";
            companyadmin.LastName = "Admin";
            companyadmin.Email = "CompanyAdmin@rbtekspace.com";
            companyadmin.UserName = "companyadmin";
            companyadmin.UserTypeId = (int) Domain.Model.Enum.UserType.CompanyAdmin;
            companyadmin.SecurityStamp = Guid.NewGuid().ToString("D");

            var companyemployee = superUser;
            companyemployee.FirstName = "Company";
            companyemployee.LastName = "Employee";
            companyemployee.Email = "CompanyEmployee@rbtekspace.com";
            companyemployee.UserName = "companyemployee";
            companyemployee.UserTypeId = (int)Domain.Model.Enum.UserType.Employee;
            companyemployee.SecurityStamp = Guid.NewGuid().ToString("D");
            companyemployee.SecurityStamp = Guid.NewGuid().ToString("D");

            var companymanager = superUser;
            companymanager.FirstName = "Company";
            companymanager.LastName = "Manager";
            companymanager.Email = "CompanyManager@rbtekspace.com";
            companymanager.UserName = "companymanager";
            companymanager.UserTypeId = (int)Domain.Model.Enum.UserType.Manager;
            companymanager.SecurityStamp = Guid.NewGuid().ToString("D");

            var companyHr = superUser;
            companyHr.FirstName = "Company";
            companyHr.LastName = "Hr";
            companyHr.Email = "CompanyHr@rbtekspace.com";
            companyHr.UserName = "CompanyHr";
            companyHr.UserTypeId = (int)Domain.Model.Enum.UserType.HumanResources;
            companyHr.SecurityStamp = Guid.NewGuid().ToString("D");

            var projectOwner = superUser;
            projectOwner.FirstName = "Project";
            projectOwner.LastName = "Owner";
            projectOwner.Email = "projectowner@rbtekspace.com";
            projectOwner.UserName = "projectowner";
            projectOwner.UserTypeId = (int)Domain.Model.Enum.UserType.ProjectOwner;
            projectOwner.SecurityStamp = Guid.NewGuid().ToString("D");

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
                superUser,
                companyadmin,
                companyemployee,
                companymanager,
                companyHr
            };
            

            foreach (var user in users)
            {
                user.CompanyId = context.Companies.First().Id;
                try
                {
                    if (!context.Users.Any(u => u.UserName == user.UserName))
                    {
                        var password = new PasswordHasher();
                        var hashed = password.HashPassword(_userPasswrod);
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