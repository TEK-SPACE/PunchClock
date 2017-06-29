using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public List<AppSetting> GetByModules(params int[] moduleIds)
        {
            using (PunchClockDbContext context = new PunchClockDbContext())
            {
                return context.AppSettings.Where(x => moduleIds.Any(m=>x.ModuleId == m)).ToList();
            }
        }

        public List<AppSetting> All()
        {
            using (PunchClockDbContext context = new PunchClockDbContext())
            {
                return context.AppSettings.Include(x=>x.Module).ToList();
            }
        }

        public AppSetting GetByKey(int moduleId, string keyName)
        {
            using (PunchClockDbContext context = new PunchClockDbContext())
            {
                return context.AppSettings.FirstOrDefault(x => x.ModuleId == moduleId && x.Key.Equals(keyName, StringComparison.OrdinalIgnoreCase));
            }
        }

        public int Add(AppSetting appSetting)
        {
            using (PunchClockDbContext context = new PunchClockDbContext())
            {
                context.AppSettings.Add(appSetting);
                context.SaveChanges();
                return appSetting.Id;
            }
        }

        public bool Update(AppSetting appSetting)
        {
            using (PunchClockDbContext context = new PunchClockDbContext())
            {
                var setting = context.AppSettings.FirstOrDefault(x => x.Id == appSetting.Id);
                if (setting == null) return false;
                setting.Key = appSetting.Key;
                setting.Value = appSetting.Value;
                setting.Description = appSetting.Description;
                setting.IsDeleted = false;
                context.SaveChanges();
                return true;
            }
        }

        public bool Delete(int id)
        {
            using (PunchClockDbContext context = new PunchClockDbContext())
            {
                var appSetting =  context.AppSettings.Where(x=> !x.IsDeleted).FirstOrDefault(x => x.Id == id);
                if (appSetting == null) return false;
                appSetting.IsDeleted = true;
                context.SaveChanges();
                return true;
            }
        }
        public bool Delete(AppSetting appSetting)
        {
           return Delete(appSetting.Id);
        }
    }
}
