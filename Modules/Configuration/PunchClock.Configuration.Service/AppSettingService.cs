using System;
using System.Collections.Generic;
using System.Linq;
using PunchClock.Configuration.Contract;
using PunchClock.Configuration.Model;
using PunchClock.Core.DataAccess;

namespace PunchClock.Configuration.Service
{
    public class AppSettingService: IAppSetting
    {
        public AppSetting GetById(int id)
        {
            using (PunchClockDbContext context = new  PunchClockDbContext())
            {
                return context.AppSettings.FirstOrDefault(x => x.Id == id);
            }
        }

        public List<AppSetting> GetByModule(int moduleId)
        {
            using (PunchClockDbContext context = new PunchClockDbContext())
            {
                return context.AppSettings.Where(x => x.ModuleId == moduleId).ToList();
            }
        }

        public List<AppSetting> All()
        {
            using (PunchClockDbContext context = new PunchClockDbContext())
            {
                return context.AppSettings.ToList();
            }
        }

        public AppSetting GetByKey(int moduleId, string keyName)
        {
            using (PunchClockDbContext context = new PunchClockDbContext())
            {
                return context.AppSettings.FirstOrDefault(x => x.ModuleId == moduleId && x.Key.Equals(keyName, StringComparison.OrdinalIgnoreCase));
            }
        }
    }
}
