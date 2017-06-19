using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using PunchClock.Configuration.Model;

namespace PunchClock.Core.DataAccess.Seeders
{
    public partial class PunchDbInitializer 
    {
        private void SeedAppSettings(PunchClockDbContext context)
        {
            List<AppSetting> appSettings = new List<AppSetting>
            {

                new AppSetting {Id =1, GlobalId = Guid.NewGuid(), IsPrivate = true,ModuleId = 1, Key = "CoreEmailPassword", Value = "osjbvzbhjqvblbjd", ValueType = KeyValueType.String},
                new AppSetting {Id =2, GlobalId = Guid.NewGuid(), IsPrivate = true,ModuleId = 1, Key = "CoreEmailFrom", Value = "sales@rbtekspace.com", ValueType = KeyValueType.String},
                new AppSetting {Id =3, GlobalId = Guid.NewGuid(), IsPrivate = true,ModuleId = 1, Key = "CoreEmailFromTitle", Value = "PunchClock | RedandBlue Applied Innovations Pvt. Ltd.", ValueType = KeyValueType.String},
                new AppSetting {Id =4, GlobalId = Guid.NewGuid(), IsPrivate = true,ModuleId = 1, Key = "CoreNotifyingList", Value = "hbopuri@rbtekspace.com", ValueType = KeyValueType.MultiString},
                new AppSetting {Id =5, GlobalId = Guid.NewGuid(), IsPrivate = true,ModuleId = 1, Key = "CoreNotifyingEnabled", Value = "true", ValueType = KeyValueType.Boolean}

            };
            foreach (var appSetting in appSettings)
            {
                context.AppSettings.AddOrUpdate(appSetting);
            }
        }
    }
}