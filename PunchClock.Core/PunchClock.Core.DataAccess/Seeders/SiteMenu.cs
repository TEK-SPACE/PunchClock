using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using PunchClock.Domain.Model;

namespace PunchClock.Core.DataAccess.Seeders
{
    public partial class PunchDbInitializer
    {
        private void SeedMenu(PunchClockDbContext context)
        {
            List<SiteMenu> menuItems = new List<SiteMenu>
            {
                new SiteMenu
                {
                    Name = "User", Controller = "", Action = "", Target = "_self",OnAuthorizationOnly = false, Description = null, CompanyId = context.Companies.First().Id, CreatedById = context.Users.First().Id,
                    Children = new List<SiteMenu>
                    {
                        new SiteMenu { Name = "Register",Controller ="User", Action= "Register", Target = "_self", OnAuthorizationOnly = false, Description = null, CompanyId = context.Companies.First().Id, CreatedById = context.Users.First().Id },
                        new SiteMenu { Name = "Details",Controller ="User", Action= "Edit", Target = "_self", Description = null, CompanyId = context.Companies.First().Id, CreatedById = context.Users.First().Id }
                    }
                },
                new SiteMenu
                {
                    Name = "Company",Controller ="Company", Action= "#", Target = "_self", OnAuthorizationOnly = false, Description = null, CompanyId = context.Companies.First().Id, CreatedById = context.Users.First().Id,
                    Children = new List<SiteMenu>
                    {
                        new SiteMenu { Name = "Registration",Controller ="Company", Action= "Register",OnAuthorizationOnly = false, Target = "_self", Description = null, CompanyId = context.Companies.First().Id, CreatedById = context.Users.First().Id },
                        new SiteMenu { Name = "Details",Controller ="Company", Action= "/Details", Target = "_self", Description = null, CompanyId = context.Companies.First().Id, CreatedById = context.Users.First().Id },
                        new SiteMenu { Name = "Employees", Controller ="Company", Action= "Emploees",Target = "_self", Description = null, CompanyId = context.Companies.First().Id, CreatedById = context.Users.First().Id },
                        new SiteMenu { Name = "Holidays", Controller ="Company", Action= "Holidays",Target = "_self", Description = null, CompanyId = context.Companies.First().Id, CreatedById = context.Users.First().Id },
                        new SiteMenu { Name = "Map Holidays",Controller ="Company", Action= "Holidays/Map", Target = "_self", Description = null, CompanyId = context.Companies.First().Id, CreatedById = context.Users.First().Id }
                    }
                },
                new SiteMenu
                {
                    Name = "Time Tracker",Controller ="Punch", Action="#", Target = "_self", Description = null, CompanyId = context.Companies.First().Id, CreatedById = context.Users.First().Id,
                    Children = new List<SiteMenu>
                    {
                        new SiteMenu { Name = "Punch", Controller ="Punch", Action="Index",Target = "_self", Description = null, CompanyId = context.Companies.First().Id, CreatedById = context.Users.First().Id },
                        new SiteMenu { Name = "Report", Controller ="Punch", Action= "Report",Target = "_self", Description = null, CompanyId = context.Companies.First().Id, CreatedById = context.Users.First().Id }
                    }
                },
                new SiteMenu
                {
                    Name = "CMS", Controller ="Article", Action= "#",Target = "_self", Description = null, CompanyId = context.Companies.First().Id, CreatedById = context.Users.First().Id,
                    Children = new List<SiteMenu>
                    {
                        new SiteMenu { Name = "Dashboard", Controller ="Article", Action=  "Index", Target = "_self", Description = null, CompanyId = context.Companies.First().Id, CreatedById = context.Users.First().Id },
                        new SiteMenu { Name = "Add", Controller ="Article", Action = "Add",Target = "_self", Description = null, CompanyId = context.Companies.First().Id, CreatedById = context.Users.First().Id }
                    }
                },
                new SiteMenu
                {
                    Name = "Ticketing", Controller ="Ticket", Action = "#",Target = "_self", Description = null, CompanyId = context.Companies.First().Id, CreatedById = context.Users.First().Id,
                    Children = new List<SiteMenu>
                    {
                        new SiteMenu { Name = "Dashboard",  Controller ="Ticket", Action = "Index", Target = "_self", Description = null, CompanyId = context.Companies.First().Id, CreatedById = context.Users.First().Id },
                        new SiteMenu { Name = "Add",  Controller ="Ticket", Action = "Add",Target = "_self", Description = null, CompanyId = context.Companies.First().Id, CreatedById = context.Users.First().Id }
                    }
                },
                new SiteMenu
                {
                    Name = "Admin",  Controller ="Admin", Action = "#",Target = "_self", Description = null, CompanyId = context.Companies.First().Id, CreatedById = context.Users.First().Id,
                    Children = new List<SiteMenu>
                    {
                        new SiteMenu { Name = "Site Map",  Controller ="SiteMap", Action = "Index",Target = "_self", Description = null, CompanyId = context.Companies.First().Id, CreatedById = context.Users.First().Id },
                        new SiteMenu { Name = "App Settings",  Controller ="Configuration", Action = "Index", Target = "_self", Description = null, CompanyId = context.Companies.First().Id, CreatedById = context.Users.First().Id },
                        new SiteMenu { Name = "Resources",  Controller ="Resources", Action = "Index",Target = "_self", Description = null, CompanyId = context.Companies.First().Id, CreatedById = context.Users.First().Id }
                    }
                }
            };


            foreach (var item in menuItems)
            {
                context.SiteMenus.AddOrUpdate(x=>new { x.ParentId, x.Controller, x.Action } ,item);
            }
        }
    }
}