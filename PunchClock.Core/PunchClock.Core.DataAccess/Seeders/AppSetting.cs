using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using PunchClock.Configuration.Model;
using PunchClock.Configuration.Model.Constants;
using PunchClock.Domain.Model.Enum;

namespace PunchClock.Core.DataAccess.Seeders
{
    public partial class PunchDbInitializer 
    {
        private void SeedAppSettings(PunchClockDbContext context)
        {
            List<AppSetting> appSettings = new List<AppSetting>
            {
                new AppSetting {Id =1, GlobalId = Guid.NewGuid(), IsPrivate = true,ModuleId = (int) ModuleType.Core, Key = AppKey.CoreEmailPassword, Value = "osjbvzbhjqvblbjd", ValueType = KeyValueType.String, IsEditable = false},
                new AppSetting {Id =2, GlobalId = Guid.NewGuid(), IsPrivate = true,ModuleId = (int) ModuleType.Core, Key = AppKey.CoreEmailFrom, Value = "sales@rbtekspace.com", ValueType = KeyValueType.String, IsEditable = false},
                new AppSetting {Id =3, GlobalId = Guid.NewGuid(), IsPrivate = true,ModuleId = (int) ModuleType.Core, Key = AppKey.CoreSmtpHost, Value = "smtp.gmail.com", ValueType = KeyValueType.String, IsEditable = false},
                new AppSetting {Id =4, GlobalId = Guid.NewGuid(), IsPrivate = true,ModuleId = (int) ModuleType.Core, Key = AppKey.CoreSmtpPort, Value = "587", ValueType = KeyValueType.Int, Description = "integer PORT number", IsEditable = false},
                new AppSetting {Id =5, GlobalId = Guid.NewGuid(), IsPrivate = true,ModuleId = (int) ModuleType.Core, Key = AppKey.CoreEmailFromTitle, Value = "PunchClock | RedandBlue Applied Innovations Pvt. Ltd.", ValueType = KeyValueType.String, Description = "Display name in the email title", IsEditable = true},
                new AppSetting {Id =6, GlobalId = Guid.NewGuid(), IsPrivate = true,ModuleId = (int) ModuleType.Core, Key = AppKey.CoreNotifyingList, Value = "hbopuri@rbtekspace.com", ValueType = KeyValueType.MultiString, Description = "Coma seperated emails", IsEditable = true},
                new AppSetting {Id =7, GlobalId = Guid.NewGuid(), IsPrivate = true,ModuleId = (int) ModuleType.Core, Key = AppKey.CoreNotifyingEnabled, Value = "true", ValueType = KeyValueType.Boolean,  Description = "TRUE or FALSE", IsEditable = true},
                new AppSetting {Id =8, GlobalId = Guid.NewGuid(), IsPrivate = false,ModuleId = (int) ModuleType.Core, Key = AppKey.CoreUserRegistrationTimeZoneDefault, Value = "India Standard Time", ValueType = KeyValueType.String,  Description = "Valid Time zone Ex: India Standard Time", IsEditable = true},
                new AppSetting {Id =9, GlobalId = Guid.NewGuid(), IsPrivate = false,ModuleId = (int) ModuleType.Core, Key = AppKey.CoreGeoEmailTemplate, Value = "GeoInfo.html", ValueType = KeyValueType.String,  Description = "You can edit email template from file system", IsEditable = true},
                new AppSetting {Id =10, GlobalId = Guid.NewGuid(), IsPrivate = false,ModuleId = (int) ModuleType.Core, Key = AppKey.CoreCompanyRegisteredEmailTemplate, Value = "CompanyRegistered.html", ValueType = KeyValueType.String,  Description = "You can edit email template from file system", IsEditable = true},
                new AppSetting {Id =11, GlobalId = Guid.NewGuid(), IsPrivate = false,ModuleId = (int) ModuleType.Core, Key = AppKey.CoreCompanyInviteEmployeeEmailTemplate, Value = "InviteEmployee.html", ValueType = KeyValueType.String,  Description = "You can edit email template from file system", IsEditable = true},
                new AppSetting {Id =12, GlobalId = Guid.NewGuid(), IsPrivate = false,ModuleId = (int) ModuleType.Core, Key = AppKey.CoreUserRegisteredEmailTemplate, Value = "UserRegistered.html", ValueType = KeyValueType.String,  Description = "You can edit email template from file system", IsEditable = true}

            };
            foreach (var appSetting in appSettings)
            {
                context.AppSettings.AddOrUpdate(appSetting);
            }
        }
    }
}