using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using PunchClock.Domain.Model;
using UserType = PunchClock.Domain.Model.Enum.UserType;

namespace PunchClock.Core.DataAccess.Seeders
{
    public partial class PunchDbInitializer
    {
        private void SeedMenu(PunchClockDbContext context)
        {
            List<SiteMap> menuItems = new List<SiteMap>
            {
                new SiteMap
                {
                    Name = "User", Controller = "", Action = "", Target = "_self", IsCoreItem= true, OnAuthorizationOnly = false, Description = null, CompanyId = context.Companies.First().Id, CreatedById = context.Users.First().Id,
                    Children = new List<SiteMap>
                    {
                        new SiteMap
                        {
                            Name = "Register",Controller ="User", Action= "Register", IsCoreItem= true, Target = "_self", OnAuthorizationOnly = false, Description = null, CompanyId = context.Companies.First().Id, CreatedById = context.Users.First().Id,
                            UserAccesses = new List<MenuUserAccess>
                            {
                               new MenuUserAccess { UserRoleId = (int)UserType.Guest }
                            }
                        },
                        new SiteMap
                        {
                            Name = "Details",Controller ="User", Action= "Edit", Target = "_self", Description = null, CompanyId = context.Companies.First().Id, CreatedById = context.Users.First().Id,
                            UserAccesses = new List<MenuUserAccess>
                            {
                                new MenuUserAccess { UserRoleId = (int)UserType.SuperAdmin } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.CompanyAdmin } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.Employee } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.HumanResources } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.Manager } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.ProjectOwner }
                            }
                        }
                    }
                },
                new SiteMap
                {
                    Name = "Company",Controller ="Company", Action= "#", Target = "_self", IsCoreItem= true, OnAuthorizationOnly = false, Description = null, CompanyId = context.Companies.First().Id, CreatedById = context.Users.First().Id,
                    Children = new List<SiteMap>
                    {
                        new SiteMap
                        {
                            IsCoreItem= true, Name = "Registration",Controller ="Company", Action= "Register",OnAuthorizationOnly = false, Target = "_self", Description = null, CompanyId = context.Companies.First().Id, CreatedById = context.Users.First().Id,
                            UserAccesses = new List<MenuUserAccess>
                            {
                             new MenuUserAccess { UserRoleId = (int)UserType.Guest }
                            }
                        },
                        new SiteMap
                        {
                            Name = "Details",Controller ="Company", Action= "/Details", Target = "_self", Description = null, CompanyId = context.Companies.First().Id, CreatedById = context.Users.First().Id,
                            UserAccesses = new List<MenuUserAccess>
                            {
                                new MenuUserAccess { UserRoleId = (int)UserType.SuperAdmin } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.CompanyAdmin } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.Employee } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.HumanResources } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.Manager } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.ProjectOwner }
                            }
                        },
                        new SiteMap
                        {
                            Name = "Employees", Controller ="Company", Action= "Emploees",Target = "_self", Description = null, CompanyId = context.Companies.First().Id, CreatedById = context.Users.First().Id,
                            UserAccesses = new List<MenuUserAccess>
                            {
                                new MenuUserAccess { UserRoleId = (int)UserType.SuperAdmin } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.CompanyAdmin } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.HumanResources } 
                              
                            }
                        },
                        new SiteMap
                        {
                            Name = "Holidays", Controller ="Company", Action= "Holidays",Target = "_self", Description = null, CompanyId = context.Companies.First().Id, CreatedById = context.Users.First().Id,
                            UserAccesses = new List<MenuUserAccess>
                            {
                                new MenuUserAccess { UserRoleId = (int)UserType.SuperAdmin } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.CompanyAdmin },
                                new MenuUserAccess { UserRoleId = (int)UserType.HumanResources }
                            }
                        },
                        new SiteMap
                        {
                            Name = "Map Holidays",Controller ="Company", Action= "Holidays/Map", Target = "_self", Description = null, CompanyId = context.Companies.First().Id, CreatedById = context.Users.First().Id,
                            UserAccesses = new List<MenuUserAccess>
                            {
                                new MenuUserAccess { UserRoleId = (int)UserType.SuperAdmin } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.CompanyAdmin } ,
                            }
                        }
                    }
                },
                new SiteMap
                {
                    Name = "Time Tracker",Controller ="Punch", Action="#", Target = "_self", Description = null, CompanyId = context.Companies.First().Id, CreatedById = context.Users.First().Id,
                    UserAccesses = new List<MenuUserAccess>
                    {
                        new MenuUserAccess { UserRoleId = (int)UserType.SuperAdmin } ,
                        new MenuUserAccess { UserRoleId = (int)UserType.CompanyAdmin } ,
                        new MenuUserAccess { UserRoleId = (int)UserType.Employee } ,
                        new MenuUserAccess { UserRoleId = (int)UserType.HumanResources } ,
                        new MenuUserAccess { UserRoleId = (int)UserType.Manager } ,
                        new MenuUserAccess { UserRoleId = (int)UserType.ProjectOwner } ,

                    },
                    Children = new List<SiteMap>
                    {
                        new SiteMap
                        {
                            Name = "Punch", Controller ="Punch", Action="Index",Target = "_self", Description = null, CompanyId = context.Companies.First().Id, CreatedById = context.Users.First().Id,
                            UserAccesses = new List<MenuUserAccess>
                            {
                                new MenuUserAccess { UserRoleId = (int)UserType.SuperAdmin } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.CompanyAdmin } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.Employee } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.HumanResources } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.Manager } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.ProjectOwner }
                            }
                        },
                        new SiteMap
                        {
                            Name = "Report", Controller ="Punch", Action= "Report",Target = "_self", Description = null, CompanyId = context.Companies.First().Id, CreatedById = context.Users.First().Id,
                            UserAccesses = new List<MenuUserAccess>
                            {
                                new MenuUserAccess { UserRoleId = (int)UserType.SuperAdmin } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.CompanyAdmin } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.Employee } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.HumanResources } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.Manager } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.ProjectOwner }
                            }
                        }
                    }
                },
                new SiteMap
                {
                    Name = "CMS", Controller ="Article", Action= "#",Target = "_self", Description = null, CompanyId = context.Companies.First().Id, CreatedById = context.Users.First().Id,
                    UserAccesses = new List<MenuUserAccess>
                    {
                        new MenuUserAccess { UserRoleId = (int)UserType.SuperAdmin } ,
                        new MenuUserAccess { UserRoleId = (int)UserType.CompanyAdmin } ,
                        new MenuUserAccess { UserRoleId = (int)UserType.Employee } ,
                        new MenuUserAccess { UserRoleId = (int)UserType.HumanResources } ,
                        new MenuUserAccess { UserRoleId = (int)UserType.Manager } ,
                        new MenuUserAccess { UserRoleId = (int)UserType.ProjectOwner } ,
                        new MenuUserAccess { UserRoleId = (int)UserType.Guest }
                    },
                    Children = new List<SiteMap>
                    {
                        new SiteMap
                        {
                            Name = "Main", Controller ="Article", Action=  "Main", Target = "_self", Description = null, CompanyId = context.Companies.First().Id, CreatedById = context.Users.First().Id,
                            UserAccesses = new List<MenuUserAccess>
                            {
                                new MenuUserAccess { UserRoleId = (int)UserType.SuperAdmin } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.CompanyAdmin } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.Employee } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.HumanResources } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.Manager } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.ProjectOwner } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.Guest }
                            }
                        },
                        new SiteMap
                        {
                            Name = "List", Controller ="Article", Action=  "List", Target = "_self", Description = null, CompanyId = context.Companies.First().Id, CreatedById = context.Users.First().Id,
                            UserAccesses = new List<MenuUserAccess>
                            {
                                new MenuUserAccess { UserRoleId = (int)UserType.SuperAdmin } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.CompanyAdmin } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.Employee } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.HumanResources } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.Manager } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.ProjectOwner } 
                            }
                        },
                        new SiteMap
                        {
                            Name = "Add", Controller ="Article", Action = "Add",Target = "_self", Description = null, CompanyId = context.Companies.First().Id, CreatedById = context.Users.First().Id,
                            UserAccesses = new List<MenuUserAccess>
                            {
                                new MenuUserAccess { UserRoleId = (int)UserType.SuperAdmin } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.CompanyAdmin } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.Employee } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.HumanResources } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.Manager } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.ProjectOwner }
                            }
                        },
                        new SiteMap
                        {
                            Name = "Dashboard", Controller ="Article", Action=  "Dashboard", Target = "_self", Description = null, CompanyId = context.Companies.First().Id, CreatedById = context.Users.First().Id,
                            UserAccesses = new List<MenuUserAccess>
                            {
                                new MenuUserAccess { UserRoleId = (int)UserType.SuperAdmin } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.CompanyAdmin } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.Employee } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.HumanResources } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.Manager } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.ProjectOwner }
                            }
                        }
                    }
                },
                new SiteMap
                {
                    Name = "Ticketing", Controller ="Ticket", Action = "#",Target = "_self", Description = null, CompanyId = context.Companies.First().Id, CreatedById = context.Users.First().Id,
                    UserAccesses = new List<MenuUserAccess>
                    {
                        new MenuUserAccess { UserRoleId = (int)UserType.SuperAdmin } ,
                        new MenuUserAccess { UserRoleId = (int)UserType.CompanyAdmin } ,
                        new MenuUserAccess { UserRoleId = (int)UserType.Employee } ,
                        new MenuUserAccess { UserRoleId = (int)UserType.HumanResources } ,
                        new MenuUserAccess { UserRoleId = (int)UserType.Manager } ,
                        new MenuUserAccess { UserRoleId = (int)UserType.ProjectOwner }
                    },
                    Children = new List<SiteMap>
                    {
                        new SiteMap
                        {
                            Name = "List",  Controller ="Ticket", Action = "Index", Target = "_self", Description = null, CompanyId = context.Companies.First().Id, CreatedById = context.Users.First().Id,
                            UserAccesses = new List<MenuUserAccess>
                            {
                                new MenuUserAccess { UserRoleId = (int)UserType.SuperAdmin } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.CompanyAdmin } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.Employee } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.HumanResources } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.Manager } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.ProjectOwner }
                            }
                        },
                        new SiteMap
                        {
                            Name = "Add",  Controller ="Ticket", Action = "Add",Target = "_self", Description = null, CompanyId = context.Companies.First().Id, CreatedById = context.Users.First().Id,
                            UserAccesses = new List<MenuUserAccess>
                            {
                                new MenuUserAccess { UserRoleId = (int)UserType.SuperAdmin } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.CompanyAdmin } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.Employee } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.HumanResources } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.Manager } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.ProjectOwner }
                            }
                        },
                        new SiteMap
                        {
                            Name = "Dashboard",  Controller ="Ticket", Action = "Dashboard", Target = "_self", Description = null, CompanyId = context.Companies.First().Id, CreatedById = context.Users.First().Id,
                            UserAccesses = new List<MenuUserAccess>
                            {
                                new MenuUserAccess { UserRoleId = (int)UserType.SuperAdmin } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.CompanyAdmin } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.Employee } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.HumanResources } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.Manager } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.ProjectOwner }
                            }
                        }
                    }
                },
                new SiteMap
                {
                    Name = "Admin",  Controller ="Admin", Action = "#",Target = "_self", Description = null, CompanyId = context.Companies.First().Id, CreatedById = context.Users.First().Id,
                    UserAccesses = new List<MenuUserAccess>
                    {
                        new MenuUserAccess { UserRoleId = (int)UserType.SuperAdmin } ,
                        new MenuUserAccess { UserRoleId = (int)UserType.CompanyAdmin }
                    },
                    Children = new List<SiteMap>
                    {
                        new SiteMap
                        {
                            Name = "Site Map",  Controller ="SiteMap", Action = "Index",Target = "_self", Description = null, CompanyId = context.Companies.First().Id, CreatedById = context.Users.First().Id,
                            UserAccesses = new List<MenuUserAccess>
                            {
                                new MenuUserAccess { UserRoleId = (int)UserType.SuperAdmin } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.CompanyAdmin }
                            }
                        },
                        new SiteMap
                        {
                            Name = "App Settings",  Controller ="Configuration", Action = "Index", Target = "_self", Description = null, CompanyId = context.Companies.First().Id, CreatedById = context.Users.First().Id,
                            UserAccesses = new List<MenuUserAccess>
                            {
                                new MenuUserAccess { UserRoleId = (int)UserType.SuperAdmin } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.CompanyAdmin }
                            }
                        },
                        new SiteMap
                        {
                            Name = "Resources",  Controller ="Resources", Action = "Index",Target = "_self", Description = null, CompanyId = context.Companies.First().Id, CreatedById = context.Users.First().Id,
                            UserAccesses = new List<MenuUserAccess>
                            {
                                new MenuUserAccess { UserRoleId = (int)UserType.SuperAdmin } ,
                                new MenuUserAccess { UserRoleId = (int)UserType.CompanyAdmin }
                            }
                        }
                    }
                }
            };


            foreach (var item in menuItems)
            {
                context.SiteMenus.AddOrUpdate(x => new {x.ParentId, x.Controller, x.Action}, item);
                foreach (var userAccess in item.UserAccesses)
                {
                    context.MenuUserAccess.AddOrUpdate(userAccess);
                }
            }
        }
    }
}