using System.Collections.Generic;
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
            return View(new List<AppSetting>());
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Add([DataSourceRequest] DataSourceRequest request,
            AppSetting appSetting)
        {
            if (appSetting != null && ModelState.IsValid)
            {
                _appSettingService.Add(appSetting);
            }

            return Json(new[] { appSetting }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            var appsettings = _appSettingService.All();
            return Json(appsettings.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request,
            AppSetting appSetting)
        {
            if (appSetting != null && ModelState.IsValid)
            {
                _appSettingService.Update(appSetting);
            }

            return Json(new[] { appSetting }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete([DataSourceRequest] DataSourceRequest request,
            AppSetting appSetting)
        {
            if (appSetting != null && ModelState.IsValid)
            {
                _appSettingService.Delete(appSetting);
            }
            return Json(new[] { appSetting }.ToDataSourceResult(request, ModelState));
        }
    }
}