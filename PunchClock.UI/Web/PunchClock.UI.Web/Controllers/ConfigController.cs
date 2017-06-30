using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PunchClock.Configuration.Contract;
using PunchClock.Configuration.Model;
using PunchClock.Configuration.Service;
using PunchClock.Core.Contracts;
using PunchClock.Core.Implementation;
using PunchClock.Domain.Model;
using PunchClock.UI.Web.Helpers;

namespace PunchClock.UI.Web.Controllers
{
    public class ConfigController : BaseController
    {
        private readonly IAppSetting _appSettingService;
        private readonly ICompany _companyService;
        private readonly ISite _siteService;

        public ConfigController()
        {
            _appSettingService = new AppSettingService();
            _companyService = new CompanyService();
            _siteService = new SiteService();
        }
        // GET: Configuration

        #region Site Map
        public ActionResult SiteMap()
        {
            return View(new List<SiteMap>());
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SiteMapAdd([DataSourceRequest] DataSourceRequest request,
            SiteMap siteMap)
        {
            if (siteMap != null && ModelState.IsValid)
            {
                _siteService.Add(siteMap);
            }

            return Json(new[] { siteMap }.ToDataSourceResult(request, ModelState));
        }

        public PartialViewResult SiteMapDetails(int id)
        {
            SiteMap siteMap = _siteService.Details(id: id);
            return PartialView("_SiteMapDetails", siteMap);
        } 
        public JsonResult SiteMapTreeRead(int? id)
        {
            List<SiteMap> siteMaps = _siteService.All(companyId: OperatingUser.CompanyId, isAdmin: OperatingUser.IsAdmin);
            
            return Json(siteMaps.Where(x => id.HasValue ? x.ParentId == id : x.ParentId == null).Select(x => new
            {
               id =  x.Id,
                x.Action,
                x.Name,
                hasChildren = x.Children.Any(),
                x.Controller,
                x.Description,
                x.IsActive,
                x.IsCoreItem,
                x.IsMenuItem
            }),  JsonRequestBehavior.AllowGet);
        }
        public ActionResult SiteMapRead([DataSourceRequest] DataSourceRequest request)
        {
            List<SiteMap> siteMaps = _siteService.All(companyId:OperatingUser.CompanyId, isAdmin: OperatingUser.IsAdmin);
            return Json(siteMaps.Flatten(x => x.Children ?? new List<SiteMap>()).Select(x=>new
            {
                x.Id,
                x.Action,
                x.Name,
                x.Controller,
                x.Description,
                x.IsActive,
                x.IsCoreItem,
                x.IsMenuItem
            }).ToDataSourceResult(request));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SiteMapUpdate([DataSourceRequest] DataSourceRequest request,
            SiteMap siteMap)
        {
            if (siteMap != null && ModelState.IsValid)
            {
                _siteService.Update(siteMap);
            }

            return Json(new[] { siteMap }.ToDataSourceResult(request, ModelState));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SiteMapDelete([DataSourceRequest] DataSourceRequest request,
            SiteMap siteMap)
        {
            if (siteMap != null && ModelState.IsValid)
            {
                _siteService.Delete(siteMap);
            }
            return Json(new[] { siteMap }.ToDataSourceResult(request, ModelState));
        }

        #endregion
        #region App Keys
        public ActionResult AppKey()
        {
            return View(new List<AppSetting>());
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AppKeyAdd([DataSourceRequest] DataSourceRequest request,
            AppSetting appSetting)
        {
            if (appSetting != null && ModelState.IsValid)
            {
                _appSettingService.Add(appSetting);
            }

            return Json(new[] { appSetting }.ToDataSourceResult(request, ModelState));
        }
        public ActionResult AppKeyRead([DataSourceRequest] DataSourceRequest request)
        {
            var appsettings = _appSettingService.All();
            return Json(appsettings.ToDataSourceResult(request));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AppKeyUpdate([DataSourceRequest] DataSourceRequest request,
            AppSetting appSetting)
        {
            if (appSetting != null && ModelState.IsValid)
            {
                _appSettingService.Update(appSetting);
            }

            return Json(new[] { appSetting }.ToDataSourceResult(request, ModelState));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AppKeyDelete([DataSourceRequest] DataSourceRequest request,
            AppSetting appSetting)
        {
            if (appSetting != null && ModelState.IsValid)
            {
                _appSettingService.Delete(appSetting);
            }
            return Json(new[] { appSetting }.ToDataSourceResult(request, ModelState));
        }

        #endregion
    }
}