using PunchClock.Common;
using PunchClock.Implementation;
using PunchClock.Objects.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PunchClock.Objects.Core.Enum;

namespace PunchClock.UI.Web.Controllers
{
    public class EmployerController : BaseController
    {
        //
        // GET: /Company/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            CompanyObjLibrary model = new CompanyObjLibrary();
            model.User = new UserObjLibrary();
            if (User.Identity.IsAuthenticated)
            {
                if (operatingUser.UserTypeId == (int)UserType.CompanyAdmin)
                {
                    return RedirectToAction("Details", "Employer", new { id = operatingUser.CompanyId });
                }
                else
                {
                    return RedirectToAction("Index", "Home", null);
                }
            }

            ViewBag.Message = "Please enter your company information";
            return View(model);
        }

        [HttpPost]
        public ActionResult Register(CompanyObjLibrary obj, HttpPostedFileBase logo)
        {
            obj.RegisterCode = Get.RandomNumber().ToString();
            obj.User.UserRegistered_ip = userSession.IpAddress;
            obj.User.Registered_MAC_address = userSession.MacAddress;
            obj.User.LastActivity_ip = userSession.IpAddress;
            obj.User.LastActive_MAC_address = userSession.MacAddress;
            obj.User.RegistrationCode = obj.RegisterCode;
            ViewBag.Message = "Successfully Added";
            CompanyService cb = new CompanyService();
            // need to handle the file size
            if (logo != null && logo.ContentLength > 0 && logo.ContentType.Contains("image"))
            {
                using (Stream inputStream = logo.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    obj.LogoUrl = logo.FileName;
                    obj.LogoBinary = memoryStream.ToArray();
                }
            }
            obj.CompanyId = cb.Add(obj);
            if (obj.User.UserId == (int)RegistrationStatus.UserNameNotAvailable)
                ViewBag.Message = "Username is already in use. Please try with a different username";
            else if (obj.CompanyId == (int)RegistrationStatus.DuplicateCompany)
            {
                ViewBag.Message = "This company is already in system. Please contact admin to get access to managge";
            }
            else
            {
                if (obj.CompanyId < 1)
                    ViewBag.Message = "Sorry. Failed to Add your company";
                if (obj.CompanyId > 0)
                {
                    FormsAuthentication.SetAuthCookie(obj.User.UserName,true);
                    ViewBag.Message = "Your company code <strong>" + obj.RegisterCode + "</strong>. Your employees need this code to sign up for their account.";
                }
            }
            return View(obj);
        }

        [HttpGet]
        public ActionResult Employees(string id)
        {
            ReadOnlyCollection<TimeZoneInfo> tz;
            tz = TimeZoneInfo.GetSystemTimeZones();
            //var _timezones = (from t in tz
            //                 orderby t.Id
            //                 select new SelectListItem
            //                 {
            //                     Value = t.Id,
            //                     Text = t.Id
            //                 }).ToList();
            List<UserObjLibrary> _employees = new List<UserObjLibrary>();
            //foreach (var u in _employees)
            //{
            //    u.timezonesList = _timezones;
            //    u.timezonesList.Where(x => x.Value == u.registeredTimeZone).Single().Selected = true;
            //}
            UserService UB = new UserService();
            _employees = UB.GetAllUsers(companyId: Convert.ToInt32(id));
            return PartialView("_Employees", _employees);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Details(int id)
        {
            ViewBag.CompanyId = id;
            ViewBag.Message = "view/edit your company information";
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(string id)
        {
            CompanyObjLibrary model = new CompanyObjLibrary();
            model.User = new UserObjLibrary();
            if (operatingUser.UserTypeId == (int)UserType.CompanyAdmin)
            {
                CompanyService cb = new CompanyService();
                model = cb.Details(operatingUser.CompanyId);
            }
            else
            {
                return RedirectToAction("Index", "Home", null);
            }
            ViewBag.Message = "Please enter your company information";
            return PartialView("_Edit", model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult SetHolidays(string id)
        {
            List<CompanyHolidayObjLibrary> hld = new List<CompanyHolidayObjLibrary>();
            CompanyService cb = new CompanyService();
            hld = cb.CompanyHolidays(Convert.ToInt32(id));
            Dictionary<string, List<CompanyHolidayObjLibrary>> obj = hld.GroupBy(x => x.HolidayType).ToDictionary(g => g.Key, g => g.ToList());
           //var retObj = hld.GroupBy(x => x.HolidayType, x => x, (key, g) => new { HolidayType = key, CompanyHolidayObjLibrary = g.ToList() }).ToList();
            ViewBag.companyId = id;
            return PartialView("_SetHolidays", obj);
        }

        [HttpPost]
        [Authorize]
        public ActionResult SetHolidays(string id, string[] holidayId)
        {
            List<CompanyHolidayObjLibrary> hld = new List<CompanyHolidayObjLibrary>();

            foreach (var hid in holidayId)
            {
                int tmpHid;
                if(int.TryParse(hid,out tmpHid)){
                    hld.Add(new CompanyHolidayObjLibrary
                    {
                        CompanyId = Convert.ToInt32(id),
                        HolidayId = tmpHid
                    });
                }
            }
            CompanyService cb = new CompanyService();
            cb.UpdateCompanyHolidays(hld);
            return Json(new { isUpdated = true });
        }


        //CompanyEmployeeHolidayPaidObjLibrary
        [HttpGet]
        [Authorize]
        public ActionResult PaidHolidayPkg(string id)
        {
            List<CompanyEmployeeHolidayPaidObjLibrary> pkg = new List<CompanyEmployeeHolidayPaidObjLibrary>();
            CompanyService cb = new CompanyService();
            pkg = cb.PaidHolidayPkg(Convert.ToInt32(id));
            SiteService sb = new SiteService();
            List<SelectListItem> employmentTypes = sb.GetEmploymentTypes(Convert.ToInt32(id));
            ViewBag.employmentTypes = employmentTypes;
            return PartialView("_PaidHolidayPkg", pkg);
        }

        [HttpPost]
        [Authorize]
        public JsonResult PaidHolidayPkg(List<CompanyEmployeeHolidayPaidObjLibrary> pkg)
        {
            CompanyService cb = new CompanyService();
            cb.UpdatePaidHolidayPkg(pkg);
            return Json(true);
        }

        [HttpPost][Authorize]
        public RedirectResult Edit(CompanyObjLibrary obj, HttpPostedFileBase logo)
        {
            ViewBag.Message = "Successfully Updated";
            CompanyService cb = new CompanyService();
            // need to handle the file size
            if (logo != null && logo.ContentLength > 0 && logo.ContentType.Contains("image"))
            {
                using (Stream inputStream = logo.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    obj.LogoUrl = logo.FileName;
                    obj.LogoBinary = memoryStream.ToArray();
                }
            }
            obj.CompanyId = cb.Update(obj);
            return Redirect(Url.Action("Details", "Employer", new { id = obj.CompanyId }));
        }
    }
}
