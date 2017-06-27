using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
                UserTypeId = (int) Domain.Model.Enum.UserType.SuperAdmin,

                PhoneNumber = "5167493582",
                RegisteredTimeZone = "India Standard Time",
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

           var  companyadmin = new User
            {
                FirstName = "Company",
                LastName = "Admin",
                Email = "CompanyAdmin@rbtekspace.com",
                UserName = "companyadmin",
                UserTypeId = (int)Domain.Model.Enum.UserType.CompanyAdmin,

                PhoneNumber = "5167493582",
                RegisteredTimeZone = "India Standard Time",
                EmploymentTypeId = (int)EmploymentType.FullTime,
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
           
           

            var companyemployee = new User
            {
                FirstName = "Company",
                LastName = "Employee",
                Email = "CompanyEmployee@rbtekspace.com",
                UserName = "companyemployee",
                UserTypeId = (int)Domain.Model.Enum.UserType.Employee,

                PhoneNumber = "5167493582",
                RegisteredTimeZone = "India Standard Time",
                EmploymentTypeId = (int)EmploymentType.FullTime,
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
         

            var companymanager = new User
            {
                FirstName = "Company",
                LastName = "Manager",
                Email = "CompanyManager@rbtekspace.com",
                UserName = "companymanager",
                UserTypeId = (int)Domain.Model.Enum.UserType.Manager,

                PhoneNumber = "5167493582",
                RegisteredTimeZone = "India Standard Time",
                EmploymentTypeId = (int)EmploymentType.FullTime,
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

            var companyHr = new User
            {
                FirstName = "Company",
                LastName = "Hr",
                Email = "CompanyHr@rbtekspace.com",
                UserName = "CompanyHr",
                UserTypeId = (int)Domain.Model.Enum.UserType.HumanResources,

                PhoneNumber = "5167493582",
                RegisteredTimeZone = "India Standard Time",
                EmploymentTypeId = (int)EmploymentType.FullTime,
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
           
            var projectOwner = new User
            {
                FirstName = "Project",
                LastName = "Owner",
                Email = "projectowner@rbtekspace.com",
                UserName = "projectowner",
                UserTypeId = (int)Domain.Model.Enum.UserType.ProjectOwner,

                PhoneNumber = "5167493582",
                RegisteredTimeZone = "India Standard Time",
                EmploymentTypeId = (int)EmploymentType.FullTime,
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
                companyHr,
                projectOwner
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