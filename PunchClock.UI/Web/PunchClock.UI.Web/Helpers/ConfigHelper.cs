using PunchClock.Configuration.Contract;
using PunchClock.Configuration.Service;
using PunchClock.Core.Models.Common.Enum;

namespace PunchClock.UI.Web.Helpers
{
    public class ConfigHelper
    {
        private static readonly IAppSetting AppSettingService;

        static ConfigHelper()
        {
            AppSettingService = new AppSettingService();
        }

        public static string DefaultTimezone()

        {
            return AppSettingService.GetByKey((int) ModuleType.Core, "CoreUserRegistrationTimeZoneDefault").Value;
        }
    }
}