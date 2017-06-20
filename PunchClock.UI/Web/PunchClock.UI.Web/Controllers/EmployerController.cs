using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PunchClock.View.Model;
using Microsoft.AspNet.Identity.Owin;
using PunchClock.Domain.Model;
using PunchClock.Core.Implementation;
using PunchClock.Core.Models.Common.Enum;

namespace PunchClock.UI.Web.Controllers
{
    public class EmployerController : BaseController
    {
        private readonly UserService _userService;
        private readonly CompanyService _companyService;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public EmployerController()
        {
            _userService = new UserService();
            _companyService = new CompanyService();
        }
        public EmployerController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            _userService = new UserService();
            _companyService = new CompanyService();
            UserManager = userManager;
            SignInManager = signInManager;
        }
        private ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            CompanyView model = new CompanyView {User = new User()};
            if (User.Identity.IsAuthenticated)
            {
                var eligibleUsers = new List<int>
                {
                    (int) Core.Models.Common.Enum.UserType.CompanyAdmin,
                    (int) Core.Models.Common.Enum.UserType.Admin
                };
                if (eligibleUsers.Any(x=>x.Equals(OperatingUser.UserTypeId) ))
                {
                    return RedirectToAction("Details", "Employer", new { id = OperatingUser.CompanyId });
                }
                return RedirectToAction("Index", "Home", null);
            }
            var systemTimeZones = TimeZoneInfo.GetSystemTimeZones();
            model.User.TimezonesList = (from t in systemTimeZones
                                  orderby t.Id
                                  select new SelectListItem
                                  {
                                      Value = t.Id,
                                      Text = t.Id
                                  }).ToList();
            model.User.TimezonesList.Single(x => x.Value == "US Eastern Standard Time").Selected = true;
            ViewBag.Message = "Please enter your company information";
            return View(model);
        }

        [HttpPost]
        public ActionResult Register(CompanyView companyView, HttpPostedFileBase logo)
        {

            if (_userService.Get(userName: companyView.User.UserName).Any())
            {
                ModelState.AddModelError("", $"Username { companyView.User.UserName} is already in use. Please try with a different username");
                return View(companyView);
            }

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
                    companyView.LogoUrl = logo.FileName;
                    companyView.LogoBinary = memoryStream.ToArray();
                }
            }
            companyView.CompanyId = _companyService.Add(companyView);

            if (companyView.CompanyId == (int)RegistrationStatus.DuplicateCompany)
            {
                ModelState.AddModelError("", $"Company {companyView.User.UserName} is already registerted");
                return View(companyView);
            }
            else if (companyView.CompanyId < 1)
            {
                ViewBag.Message = "Sorry. Failed to Add your company";
                return View(companyView);
            }

            companyView.User.UserRegisteredIp = UserSession.IpAddress;
            companyView.User.RegisteredMacAddress = UserSession.MacAddress;
            companyView.User.LastActivityIp = UserSession.IpAddress;
            companyView.User.LastActiveMacAddress = UserSession.MacAddress;
            companyView.User.RegistrationCode = companyView.RegisterCode;
            companyView.User.UserTypeId = (int)Core.Models.Common.Enum.UserType.CompanyAdmin;
            companyView.User.EmploymentTypeId = (int)Core.Models.Common.Enum.EmploymentType.ContractHourly;
            companyView.User.CompanyId = companyView.CompanyId;
            companyView.CompanyId = companyView.CompanyId;
         
            try
            {
                var result = UserManager.CreateAsync(companyView.User, companyView.User.Password).Result;
                if (result.Succeeded)
                {
                    SignInManager.SignIn(companyView.User, isPersistent: false, rememberBrowser: false);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            catch (Exception exception)
            {
                throw;
            }

            if (companyView.CompanyId >0)
            {
                _companyService.SetCreatedBy(companyId: companyView.CompanyId, userId: companyView.User.Uid);
                ViewBag.Message = "Your company code <strong>" + companyView.RegisterCode + "</strong>. Your employees need this code to sign up for their account.";
            }
            return View(companyView);
        }

        [HttpGet]
        public ActionResult Employees(int id)
        {
            ReadOnlyCollection<TimeZoneInfo> tz = TimeZoneInfo.GetSystemTimeZones();
            UserService ub = new UserService();
            var employees = ub.GetAllUsers(companyId: id);
            return PartialView("_Employees", employees);
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
        public ActionResult Edit(int id)
        {
            CompanyView model = null;
            var eligibleUsers = new List<int>
            {
                (int) Core.Models.Common.Enum.UserType.CompanyAdmin,
                (int) Core.Models.Common.Enum.UserType.Admin
            };
            if (eligibleUsers.Any(x => x.Equals(OperatingUser.UserTypeId)))
            {
                CompanyService cb = new CompanyService();
                model = cb.Get(OperatingUser.CompanyId);
            }
            ViewBag.Message = "Please enter your company information";
            return PartialView("_Edit", model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult SetHolidays(int id)
        {
            List<CompanyHolidayView> hld = new List<CompanyHolidayView>();
            CompanyService cb = new CompanyService();
            hld = cb.CompanyHolidays(id);
            Dictionary<string, List<CompanyHolidayView>> obj = hld.GroupBy(x => x.HolidayType).ToDictionary(g => g.Key, g => g.ToList());
           //var retObj = hld.GroupBy(x => x.HolidayType, x => x, (key, g) => new { HolidayType = key, CompanyHolidayObjLibrary = g.ToList() }).ToList();
            ViewBag.companyId = id;
            return PartialView("_SetHolidays", obj);
        }

        [HttpPost]
        [Authorize]
        public ActionResult SetHolidays(string id, string[] holidayId)
        {
            List<View.Model.CompanyHolidayView> hld = new List<View.Model.CompanyHolidayView>();

            foreach (var hid in holidayId)
            {
                int tmpHid;
                if(int.TryParse(hid,out tmpHid)){
                    hld.Add(new View.Model.CompanyHolidayView
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
        public ActionResult PaidHolidayPkg(int id)
        {
            CompanyService companyService = new CompanyService();
            var pkg = companyService.PaidHolidayPkg(id);
            SiteService siteService = new SiteService();
            List<SelectListItem> employmentTypes = siteService.GetEmploymentTypes(id);
            ViewBag.employmentTypes = employmentTypes;
            return PartialView("_PaidHolidayPkg", pkg);
        }

        [HttpPost]
        [Authorize]
        public JsonResult PaidHolidayPkg(List<View.Model.EmployeePaidHolidayView> pkg)
        {
            CompanyService cb = new CompanyService();
            cb.UpdatePaidHolidayPkg(pkg);
            return Json(true);
        }

        [HttpPost][Authorize]
        public ActionResult Edit(View.Model.CompanyView companyView, HttpPostedFileBase logo)
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
                    companyView.LogoUrl = logo.FileName;
                    companyView.LogoBinary = memoryStream.ToArray();
                }
            }
            CompanyTransaction transaction =  cb.Update(companyView);
            if (transaction == CompanyTransaction.Success)
            {
                return Redirect(Url.Action("Details", "Employer", new { id = companyView.CompanyId }));

            }
            switch (transaction)
            {
                case CompanyTransaction.DuplicateName:
                    ModelState.AddModelError(nameof(companyView.Name), "is duplicate");
                    break;
                case CompanyTransaction.Error:
                    break;
                case CompanyTransaction.WrongImageType:
                    ModelState.AddModelError(nameof(companyView.LogoBinary), "Wrong image");
                    break;
            }
            return View("_Edit", companyView);
        }
    }
}
