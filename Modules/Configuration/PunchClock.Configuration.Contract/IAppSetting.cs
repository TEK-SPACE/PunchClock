using System.Collections.Generic;
using PunchClock.Configuration.Model;

namespace PunchClock.Configuration.Contract
{
    public interface IAppSetting
    {
        AppSetting GetById(int id);
        List<AppSetting> GetByModule(int moduleId);
        List<AppSetting> All();
        AppSetting GetByKey(int moduleId, string keyName);
        int Add(AppSetting appSetting);
        bool Update(AppSetting appSetting);
        bool Delete(int id);
    }
}
