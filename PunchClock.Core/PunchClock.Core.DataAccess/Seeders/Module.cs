using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using PunchClock.Core.Models.Common;

namespace PunchClock.Core.DataAccess.Seeders
{
    public partial class PunchDbInitializer
    {
        private void SeedModules(PunchClockDbContext context)
        {
            List<Module> modules = new List<Module>
            {
                new Module {Id =1, Name  = "Core", Description = "Core application component", GlobalId = Guid.NewGuid(), IsActive = true, LicenseKey = null, IsEditable = false},
                new Module {Id =2, Name  = "Configuration", Description = "Ability to Manage Configurations from Admin page", GlobalId = Guid.NewGuid(), IsActive = true, LicenseKey = null, IsEditable = false},
                new Module {Id =3, Name  = "CMS", Description = "Content Management System", GlobalId = Guid.NewGuid(), IsActive = true, LicenseKey = null, IsEditable = true},
                new Module {Id =4, Name  = "Ticketing", Description = "Ticketing System", GlobalId = Guid.NewGuid(), IsActive = true, LicenseKey = null, IsEditable = true},
                new Module {Id =5, Name  = "Time Tracker", Description = "Time Tracker (Punch Clock)", GlobalId = Guid.NewGuid(), IsActive = true, LicenseKey = null, IsEditable = true},
                new Module {Id =6, Name  = "Languages", Description = "Support Multiple Language Resources", GlobalId = Guid.NewGuid(), IsActive = true, LicenseKey = null, IsEditable = true}
            };
            foreach (var module in modules)
            {
                context.Modules.AddOrUpdate(module);
            }
        }
    }
}