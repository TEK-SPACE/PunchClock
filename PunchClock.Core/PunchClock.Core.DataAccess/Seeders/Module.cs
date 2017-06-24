using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using PunchClock.Core.Models.Common;
using PunchClock.Core.Models.Common.Enum;

namespace PunchClock.Core.DataAccess.Seeders
{
    public partial class PunchDbInitializer
    {
        private void SeedModules(PunchClockDbContext context)
        {
            List<Module> modules = new List<Module>
            {
                new Module {Id = (int)ModuleType.Core, Name  = "Core", Description = "Core application component", GlobalId = Guid.NewGuid(), IsActive = true, LicenseKey = null, IsEditable = false},
                new Module {Id =(int)ModuleType.Configuration, Name  = "Configuration", Description = "Ability to Manage Configurations from SuperAdmin page", GlobalId = Guid.NewGuid(), IsActive = true, LicenseKey = null, IsEditable = false},
                new Module {Id =(int)ModuleType.Cms, Name  = "CMS", Description = "Content Management System", GlobalId = Guid.NewGuid(), IsActive = true, LicenseKey = null, IsEditable = true},
                new Module {Id =(int)ModuleType.Ticketing, Name  = "Ticketing", Description = "Ticketing System", GlobalId = Guid.NewGuid(), IsActive = true, LicenseKey = null, IsEditable = true},
                new Module {Id =(int)ModuleType.TimeTracker, Name  = "Time Tracker", Description = "Time Tracker (Punch Clock)", GlobalId = Guid.NewGuid(), IsActive = true, LicenseKey = null, IsEditable = true},
                new Module {Id =(int)ModuleType.Languages, Name  = "Languages", Description = "Support Multiple Language Resources", GlobalId = Guid.NewGuid(), IsActive = true, LicenseKey = null, IsEditable = true}
            };
            foreach (var module in modules)
            {
                context.Modules.AddOrUpdate(module);
            }
        }
    }
}