using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using PunchClock.Configuration.Model;
using PunchClock.Core.Models.Common.Enum;

namespace PunchClock.Core.DataAccess.Seeders
{
    public partial class PunchDbInitializer 
    {
        private void SeedAppSettings(PunchClockDbContext context)
        {
            List<AppSetting> appSettings = new List<AppSetting>
            {

                new AppSetting {Id =1, GlobalId = Guid.NewGuid(), IsPrivate = true,ModuleId = (int) ModuleType.Core, Key = "CoreEmailPassword", Value = "osjbvzbhjqvblbjd", ValueType = KeyValueType.String, IsEditable = false},
                new AppSetting {Id =2, GlobalId = Guid.NewGuid(), IsPrivate = true,ModuleId = (int) ModuleType.Core, Key = "CoreEmailFrom", Value = "sales@rbtekspace.com", ValueType = KeyValueType.String, IsEditable = false},
                new AppSetting {Id =3, GlobalId = Guid.NewGuid(), IsPrivate = true,ModuleId = (int) ModuleType.Core, Key = "CoreSmtpHost", Value = "smtp.gmail.com", ValueType = KeyValueType.String, IsEditable = false},
                new AppSetting {Id =4, GlobalId = Guid.NewGuid(), IsPrivate = true,ModuleId = (int) ModuleType.Core, Key = "CoreSmtpPort", Value = "587", ValueType = KeyValueType.Int, Description = "integer PORT number", IsEditable = false},
                new AppSetting {Id =5, GlobalId = Guid.NewGuid(), IsPrivate = true,ModuleId = (int) ModuleType.Core, Key = "CoreEmailFromTitle", Value = "PunchClock | RedandBlue Applied Innovations Pvt. Ltd.", ValueType = KeyValueType.String, Description = "Display name in the email title", IsEditable = true},
                new AppSetting {Id =6, GlobalId = Guid.NewGuid(), IsPrivate = true,ModuleId = (int) ModuleType.Core, Key = "CoreNotifyingList", Value = "hbopuri@rbtekspace.com", ValueType = KeyValueType.MultiString, Description = "Coma seperated emails", IsEditable = true},
                new AppSetting {Id =7, GlobalId = Guid.NewGuid(), IsPrivate = true,ModuleId = (int) ModuleType.Core, Key = "CoreNotifyingEnabled", Value = "true", ValueType = KeyValueType.Boolean,  Description = "TRUE or FALSE", IsEditable = true}
            };
            foreach (var appSetting in appSettings)
            {
                context.AppSettings.AddOrUpdate(appSetting);
            }
        }
    }
}