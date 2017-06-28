using System.Collections.Generic;
using PunchClock.Configuration.Model;

namespace PunchClock.Configuration.Contract
{
    public interface IAppSetting
    {
        AppSetting GetById(int id);
        List<AppSetting> GetByModules(params int[] moduleIds);
        List<AppSetting> All();
        AppSetting GetByKey(int moduleId, string keyName);
        int Add(AppSetting appSetting);
        bool Update(AppSetting appSetting);
        bool Delete(int id);
        bool Delete(AppSetting appSetting);
    }
}
