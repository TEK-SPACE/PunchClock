using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using PunchClock.Core.Contracts;
using PunchClock.Core.Implementation;
using PunchClock.Domain.Model;
using PunchClock.Helper.Common;
using PunchClock.UI.Web.Models;
using EmploymentType = PunchClock.Domain.Model.Enum.EmploymentType;

namespace PunchClock.UI.Web.Controllers
{
    public class UserController : BaseController
    {
        //
        // GET: /Register/
        private readonly IUser _userService;
        private readonly IEmail _emailService;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly ISite _siteService;
        private readonly ICompany _companyService;

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
        private ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }

        public UserController()
        {
            _userService = new UserService();
            _emailService = new Core.Implementation.EmailService();
            _siteService = new SiteService();
            _companyService = new CompanyService();
        }

        public ActionResult Register(string id)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Edit", "User", new {userName = OperatingUser.UserName});
            var invite = _companyService.ByInviteKey(id);
            if (invite == null)
            {
                return View("InvalidInvite");
            }
            User user = new User
            {
                LastActivityIp = UserSession.IpAddress,
                LastActiveMacAddress = UserSession.MacAddress,
                RegistrationCode = invite.Company.RegisterCode,
                UserTypeId = invite.UserTypeId
            };
            return View(user);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                user.UserRegisteredIp = UserSession.IpAddress;
                user.RegisteredMacAddress = UserSession.MacAddress;
                user.LastActivityIp = UserSession.IpAddress;
                user.LastActiveMacAddress = UserSession.MacAddress;
                user.EmploymentTypeId = (int)EmploymentType.ContractHourly; // this is default employemnt type at registration. later admin can set the type
                user.DateCreatedUtc = DateTimeOffset.UtcNow;
                user.LastActivityDateUtc = DateTimeOffset.UtcNow;
                user.LastUpdatedUtc = DateTimeOffset.UtcNow;
                user.PasswordLastChanged = DateTime.UtcNow;

                Company company = _companyService.Get(code: user.RegistrationCode);
                if (company == null || company.Id < 1)
                {
                    user.ErrorMessage =
                        $"Registration code {user.RegistrationCode} doesn't match with any  Company in the system";
                    ModelState.AddModelError("", user.ErrorMessage);
                    SetRegistrationContext(user);
                    return Json(user);
                }
                user.CompanyId = company.Id;
                user.IsActive = true;

                var result = await UserManager.CreateAsync(user, user.Password);
                if (result.Succeeded)
                {
                    user.RegistrationAddress.UserId = user.Id;
                  _userService.AddAddress(user.RegistrationAddress);
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    //return RedirectToAction("Index", "Home");

                    string emailMessage = _userService.ComposeRegisteredEmail(user);
                    _emailService.SendEmail(emailMessage, "Successfully Registered", new[] { user.Email});

                    return Json(user);
                }
                user.Uid = -5;
                user.ErrorMessage = string.Join("<br/>", result.Errors.Select(x => x));
                AddIdentityErrors(result);
            }
            else
            {
                ReadModelStateError(modelState:ModelState);
                user.ErrorMessage = ViewData["ModelError"] as string;
            }
            SetRegistrationContext(user);
            // If we got this far, something failed, redisplay form
            return Json(user);
        }


        private User SetRegistrationContext(User user)
        {
            user.LastActivityIp = UserSession.IpAddress;
            user.LastActiveMacAddress = UserSession.MacAddress;

            var systemTimeZones = TimeZoneInfo.GetSystemTimeZones();
            user.TimezonesList = (from t in systemTimeZones
                orderby t.Id
                select new SelectListItem
                {
                    Value = t.Id,
                    Text = t.Id
                }).ToList();
            user.TimezonesList.Single(x => x.Value == "US Eastern Standard Time").Selected = true;

            var userTypes = Get.UserTypes(adminCall: true);
            if (user.UserTypeId > 0)
                userTypes.First(x => x.Value == user.UserTypeId.ToString()).Selected = true;
            ViewBag.UserTypes = userTypes;
            return user;
        }
        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.ByEmail(model.Email);
                if (user != null)
                {
                    var resetCode = _userService.SeedPasswordReset(user.Id);
                    StringBuilder resetBuilder = new StringBuilder();
                    resetBuilder.AppendLine("Please use the link to reset your password");
                    resetBuilder.AppendLine($"{System.Web.HttpContext.Current.Request.Url.Scheme}://{System.Web.HttpContext.Current.Request.Url.Host}{Url.Action("ResetPassword", "User", new { uid = user.Id, code = resetCode })}");
                    _emailService.SendEmail(resetBuilder.ToString(), "PunchClock Password Reset Link", new[] {user.Email});
                    return View("ForgotPasswordConfirmation");
                }
                ViewBag.Message = "No account found with this email";
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ResetPassword(string uid, string code)
        {
            object retMessage = "Invalid code";
            var user = _userService.ByGuid(uid);
            if (!string.IsNullOrWhiteSpace(user.PasswordResetCode) 
                && user.PasswordResetCode.Equals(code, StringComparison.OrdinalIgnoreCase))
            {
                var newPassword = $"^{_userService.RandomString().ToUpper()}-{_userService.RandomString().ToLower()}{_userService.RandomNumber()}";
                UserManager.RemovePasswordAsync(uid).Wait();
                var result = UserManager.AddPasswordAsync(uid, newPassword).Result;
                if (!result.Succeeded)
                {
                    throw  new Exception($"this is the password: {newPassword} {string.Join("|", result.Errors)}");
                }
                //_userService.Update(userId: uid, password: newPassword);
                retMessage = newPassword;
            }
            return View(retMessage);
        }


        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public ActionResult Login(LoginModel model, string returnUrl)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }
        //    var success = UserService.Login(model.UserName, model.Password, UserUserSession.IpAddress, UserUserSession.MacAddress);
        //    if (success)
        //    {
        //        FormsAuthentication.SetAuthCookie(model.UserName, true);
        //        return RedirectToLocal(returnUrl);
        //    }
        //    return View(model);
        //}

        //private ActionResult RedirectToLocal(string returnUrl)
        //{
        //    if (Url.IsLocalUrl(returnUrl))
        //    {
        //        return Redirect(returnUrl);
        //    }
        //    return RedirectToAction("Index", "Home");
        //}

        [HttpGet]
        [Authorize]
        public ActionResult Edit(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                userName = OperatingUser.UserName;
            var user = _userService.Details(userName);
            user.UserRegisteredIp = UserSession.IpAddress;
            user.RegisteredMacAddress = UserSession.MacAddress;
            user.LastActivityIp = UserSession.IpAddress;
            user.LastActiveMacAddress = UserSession.MacAddress;
            return View(user);
        }

        [HttpPost]
        [Authorize]
        public JsonResult Edit(User user)
        {
            user.UserRegisteredIp = UserSession.IpAddress;
            user.RegisteredMacAddress = UserSession.MacAddress;
            user.LastActivityIp = UserSession.IpAddress;
            user.LastActiveMacAddress = UserSession.MacAddress;
            user.Uid = _userService.Update(user, false);
            return Json(new { user });
        }

        [HttpGet]
        [Authorize]
        public ActionResult Details(int id)
        {
            var user = _userService.DetailsById(id);

            var systemTimeZones = TimeZoneInfo.GetSystemTimeZones();
            user.TimezonesList = (from t in systemTimeZones
                orderby t.Id
                select new SelectListItem
                {
                    Value = t.Id,
                    Text = t.Id
                }).ToList();
            user.TimezonesList.Single(x => x.Value == user.RegisteredTimeZone).Selected = true;
            var employmentTypes = Get.EmploymentTypes();
            employmentTypes.First(x => x.Value == user.EmploymentTypeId.ToString()).Selected = true;
            ViewBag.EmploymentType = employmentTypes;

            var userTypes = Get.UserTypes(true);
            userTypes.First(x => x.Value == user.UserTypeId.ToString()).Selected = true;
            ViewBag.UserTypes = userTypes;

            //if (user.LastPunch.PunchIn != TimeSpan.MinValue)
            //{
            //    user.LastPunch.PunchIn =
            //        TimeZoneInfo.ConvertTimeFromUtc(user.LastPunch.PunchDate.Date + user.LastPunch.PunchIn,
            //                TimeZoneInfo.FindSystemTimeZoneById(OperatingUser.RegisteredTimeZone))
            //            .TimeOfDay;
            //}
            //if (user.LastPunch.PunchOut != null && user.LastPunch.PunchOut != TimeSpan.MinValue)
            //{
            //    user.LastPunch.PunchOut = TimeZoneInfo
            //        .ConvertTimeFromUtc(user.LastPunch.PunchDate.Date + user.LastPunch.PunchOut.Value,
            //            TimeZoneInfo.FindSystemTimeZoneById(OperatingUser.RegisteredTimeZone))
            //        .TimeOfDay;
            //}
            return PartialView("_Details", user);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Details(User user, bool adminUpdate = false)
        {
            _userService.Update(user, adminUpdate);
            return Json(new { user = user });
        }

        [HttpGet]
        public ActionResult Types()
        {
            return Json(_userService.GetTypes(), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Zones()
        {
            return Json(TimeZoneInfo.GetSystemTimeZones().OrderBy(x=>x.Id), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult All()
        {
            return Json(_userService.All(companyId: OperatingUser.CompanyId), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetDisplayName(string userId)
        {
            return Json(_userService.DetailsByKey(userId).DisplayName, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Countries()
        {
            return Json(_siteService.GetCountries(), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult States(int id)
        {
            return Json(_siteService.GetStates(countryId:id), JsonRequestBehavior.AllowGet);
        }
    }
}
