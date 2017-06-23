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
using EmploymentType = PunchClock.Core.Models.Common.Enum.EmploymentType;

namespace PunchClock.UI.Web.Controllers
{
    public class UserController : BaseController
    {
        //
        // GET: /Register/
        private readonly IUserRepository _userService;
        private readonly IEmailRepository _emailRepository;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
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
            _emailRepository = new Core.Implementation.EmailService();
        }

        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Edit", "User", new { userName = OperatingUser.UserName });
            User user = new User
            {
                LastActivityIp = UserSession.IpAddress,
                LastActiveMacAddress = UserSession.MacAddress
            };

            var systemTimeZones = TimeZoneInfo.GetSystemTimeZones();
            user.TimezonesList = (from t in systemTimeZones
                                  orderby t.Id
                                  select new SelectListItem
                                  {
                                       Value = t.Id,
                                       Text = t.Id
                                  }).ToList();
            user.TimezonesList.Single(x => x.Value == "US Eastern Standard Time").Selected = true;

            var userTypes = Get.UserTypes(true);
            if (user.UserTypeId > 0)
                userTypes.First(x => x.Value == user.UserTypeId.ToString()).Selected = true;
            ViewBag.UserTypes = userTypes;

            return View(user);
        }

        [HttpPost]
        public JsonResult Register(User user)
        {
            user.UserRegisteredIp = UserSession.IpAddress;
            user.RegisteredMacAddress= UserSession.MacAddress;
            user.LastActivityIp = UserSession.IpAddress;
            user.LastActiveMacAddress = UserSession.MacAddress;
            user.EmploymentTypeId = (int)EmploymentType.ContractHourly; // this is default employemnt type at registration. later admin can set the type
            user.Uid = _userService.Add(user);
            return Json(new { user });
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
                    _emailRepository.SendEmail(resetBuilder.ToString(), "PunchClock Password Reset Link", new[] {user.Email});
                    return View("ForgotPasswordConfirmation");
                }
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
    }
}
