using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using PunchClock.Core.Contracts;
using PunchClock.Core.Implementation;
using PunchClock.Domain.Model;
using PunchClock.Domain.Model.Enum;
using PunchClock.View.Model;
using EmploymentType = PunchClock.Domain.Model.Enum.EmploymentType;
using UserType = PunchClock.Domain.Model.Enum.UserType;

namespace PunchClock.UI.Web.Controllers
{
    public class CompanyController : BaseController
    {
        private readonly UserService _userService;
        private readonly ICompany _companyService;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public CompanyController()
        {
            _userService = new UserService();
            _companyService = new CompanyService();
        }
        public CompanyController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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
            CompanyRegister model = new CompanyRegister { Company = new Company(), CreatedBy = new User()};
            if (User.Identity.IsAuthenticated)
            {
                var eligibleUsers = new List<int>
                {
                    (int) UserType.CompanyAdmin,
                    (int) UserType.SuperAdmin
                };
                if (eligibleUsers.Any(x=>x.Equals(OperatingUser.UserTypeId) ))
                {
                    return RedirectToAction("Details", "Company", new { id = OperatingUser.CompanyId });
                }
                return RedirectToAction("Index", "Home", null);
            }
            var systemTimeZones = TimeZoneInfo.GetSystemTimeZones();
            model.CreatedBy.TimezonesList = (from t in systemTimeZones
                                  orderby t.Id
                                  select new SelectListItem
                                  {
                                      Value = t.Id,
                                      Text = t.Id
                                  }).ToList();
            model.CreatedBy.TimezonesList.Single(x => x.Value == "US Eastern Standard Time").Selected = true;
            ViewBag.Message = "Please enter your company information";
            return View(model);
        }

        [HttpPost]
        public ActionResult Register(CompanyRegister companyRegister, HttpPostedFileBase logo)
        {

            if (_userService.Get(userName: companyRegister.CreatedBy.UserName).Any())
            {
                ModelState.AddModelError("",
                    $"Username {companyRegister.CreatedBy.UserName} is already in use. Please try with a different username");
                return View(companyRegister);
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
                    companyRegister.Company.LogoUrl = logo.FileName;
                    companyRegister.Company.LogoBinary = memoryStream.ToArray();
                }
            }
            companyRegister.CreatedBy.UserRegisteredIp = UserSession.IpAddress;
            companyRegister.CreatedBy.RegisteredMacAddress = UserSession.MacAddress;
            companyRegister.CreatedBy.LastActivityIp = UserSession.IpAddress;
            companyRegister.CreatedBy.LastActiveMacAddress = UserSession.MacAddress;
            companyRegister.CreatedBy.EmploymentTypeId = (int) EmploymentType.FullTime;

            companyRegister.Company.Id = _companyService.Add(companyRegister.Company);

            if (companyRegister.Company.Id == (int) RegistrationStatus.DuplicateCompany)
            {
                ModelState.AddModelError("", $"Company {companyRegister.CreatedBy.UserName} is already registerted");
                return View(companyRegister);
            }
            if (companyRegister.Company.Id < 1)
            {
                ViewBag.Message = "Sorry. Failed to Add your company";
                return View(companyRegister);
            }

            companyRegister.CreatedBy.CompanyId = companyRegister.Company.Id;
            try
            {
                var result = UserManager.CreateAsync(companyRegister.CreatedBy, companyRegister.CreatedBy.Password)
                    .Result;
                if (result.Succeeded)
                {
                    //SignInManager.SignIn(companyRegister.CreatedBy, isPersistent: false, rememberBrowser: false);
                    _userService.AddAddress(companyRegister.CreatedBy.RegistrationAddress);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                    return View(companyRegister);
                }
            }
            catch
            {
                // ignored
            }

            if (companyRegister.Company.Id > 0)
            {
                _companyService.SetCreatedBy(companyId: companyRegister.Company.Id,
                    userId: companyRegister.CreatedBy.Id);
                ViewBag.Message = "Your company code <strong>" + companyRegister.Company.RegisterCode +
                                  "</strong>. Your employees need this code to sign up for their account.";
                SignInManager.SignInAsync(companyRegister.CreatedBy, isPersistent: false,
                    rememberBrowser: false).Wait();
                return RedirectToAction("Index", "Home");
            }
            return View(companyRegister);
        }

        [HttpGet]
        public ActionResult Employees(int id)
        {
            UserService userService = new UserService();
            var employees = userService.GetAllUsers(companyId: id);
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
            Company model = null;
            var eligibleUsers = new List<int>
            {
                (int) UserType.CompanyAdmin,
                (int) UserType.SuperAdmin
            };
            if (eligibleUsers.Any(x => x.Equals(OperatingUser.UserTypeId)))
            {
                model = _companyService.Get(OperatingUser.CompanyId);
            }
            ViewBag.Message = "Please enter your company information";
            return PartialView("_Edit", model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult SetHolidays(int id)
        {
            var companyHolidays = _companyService.CompanyHolidays(id);
            Dictionary<string, List<CompanyHolidayView>> obj =
                companyHolidays.GroupBy(x => x.HolidayType).ToDictionary(g => g.Key, g => g.ToList());
            ViewBag.companyId = id;
            return PartialView("_SetHolidays", obj);
        }

        [HttpPost]
        [Authorize]
        public ActionResult SetHolidays(string id, string[] holidayId)
        {
            List<CompanyHolidayView> hld = new List<CompanyHolidayView>();

            foreach (var hid in holidayId)
            {
                int tmpHid;
                if(int.TryParse(hid,out tmpHid)){
                    hld.Add(new CompanyHolidayView
                    {
                        CompanyId = Convert.ToInt32(id),
                        HolidayId = tmpHid
                    });
                }
            }
            _companyService.UpdateCompanyHolidays(hld);
            return Json(new { isUpdated = true });
        }


        //CompanyEmployeeHolidayPaidObjLibrary
        [HttpGet]
        [Authorize]
        public ActionResult PaidHolidayPkg(int id)
        {
            var paidHolidayPkg = _companyService.PaidHolidayPkg(id);
            ISite siteService = new SiteService();
            List<SelectListItem> employmentTypes = siteService.GetEmploymentTypes(id);
            ViewBag.employmentTypes = employmentTypes;
            return PartialView("_PaidHolidayPkg", paidHolidayPkg);
        }

        [HttpPost]
        [Authorize]
        public JsonResult PaidHolidayPkg(List<EmployeePaidHolidayView> pkg)
        {
            _companyService.UpdatePaidHolidayPkg(pkg);
            return Json(true);
        }

        [HttpPost][Authorize]
        public ActionResult Edit(Company company, HttpPostedFileBase logo)
        {
            ViewBag.Message = "Successfully Updated";
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
                    company.LogoUrl = logo.FileName;
                    company.LogoBinary = memoryStream.ToArray();
                }
            }
            CompanyTransaction transaction = _companyService.Update(company);
            if (transaction == CompanyTransaction.Success)
            {
                return Redirect(Url.Action("Details", "Company", new { id = company.Id }));
            }
            switch (transaction)
            {
                case CompanyTransaction.DuplicateName:
                    ModelState.AddModelError(nameof(company.Name), "is duplicate");
                    break;
                case CompanyTransaction.Error:
                    break;
                case CompanyTransaction.WrongImageType:
                    ModelState.AddModelError(nameof(company.LogoBinary), "Wrong image");
                    break;
            }
            return View("_Edit", company);
        }
    }
}
