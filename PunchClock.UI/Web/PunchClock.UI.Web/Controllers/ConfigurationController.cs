using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PunchClock.Configuration.Contract;
using PunchClock.Configuration.Model;
using PunchClock.Configuration.Service;

namespace PunchClock.UI.Web.Controllers
{
    public class ConfigurationController : Controller
    {
        private readonly IAppSetting _appSettingService;

        public ConfigurationController()
        {
            _appSettingService = new AppSettingService();
        }
        // GET: Configuration
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Add(AppSetting appSetting)
        {
            return Json(_appSettingService.Add(appSetting));
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(_appSettingService.All().ToDataSourceResult(request));
        }
    }
}