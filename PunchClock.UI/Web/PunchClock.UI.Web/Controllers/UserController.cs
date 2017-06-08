using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using PunchClock.Core.Contracts;
using PunchClock.Core.Implementation;
using PunchClock.Core.Models.Common.Enum;
using PunchClock.Helper.Common;
using PunchClock.UI.Web.Models;
using PunchClock.View.Model;

namespace PunchClock.UI.Web.Controllers
{
    public class UserController : BaseController
    {
        //
        // GET: /Register/
        private readonly UserService _userService;
        private readonly IEmailRepository _emailRepository;
        public UserController()
        {
            _userService = new UserService();
            _emailRepository = new PunchClock.Core.Implementation.EmailService();
        }

        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Edit", "User", new { userName = OperatingUser.UserName });
            UserView userView = new UserView
            {
                LastActivityIp = UserSession.IpAddress,
                LastActiveMacAddress = UserSession.MacAddress
            };

            var systemTimeZones = TimeZoneInfo.GetSystemTimeZones();
            userView.TimezonesList = (from t in systemTimeZones
                                  orderby t.Id
                                  select new SelectListItem
                                  {
                                       Value = t.Id,
                                       Text = t.Id
                                  }).ToList();
            userView.TimezonesList.Single(x => x.Value == "US Eastern Standard Time").Selected = true;

            var userTypes = Get.UserTypes(true);
            if (userView.UserTypeId > 0)
                userTypes.First(x => x.Value == userView.UserTypeId.ToString()).Selected = true;
            ViewBag.UserTypes = userTypes;

            return View(userView);
        }

        [HttpPost]
        public JsonResult Register(UserView userView)
        {
            userView.UserRegisteredIp = UserSession.IpAddress;
            userView.RegisteredMacAddress= UserSession.MacAddress;
            userView.LastActivityIp = UserSession.IpAddress;
            userView.LastActiveMacAddress = UserSession.MacAddress;
            userView.EmploymentTypeId = (int)EmploymentType.ContractHourly; // this is default employemnt type at registration. later admin can set the type
            userView.UserId = _userService.Add(userView);
            return Json(new { userView });
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
                    resetBuilder.AppendLine($"{System.Web.HttpContext.Current.Request.Url.Scheme}://{System.Web.HttpContext.Current.Request.Url.Host}{Url.Action("ResetPassword", "User", new { id = user.Id, code = resetCode })}");
                    _emailRepository.SendEmail(resetBuilder.ToString(), "PunchClock Password Reset Link", new[] {user.Email});
                    return View("ForgotPasswordConfirmation");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ResetPassword(string id, string code)
        {
            var user = _userService.Details(id);
            if (user.PasswordResetCode.Equals(code, StringComparison.OrdinalIgnoreCase))
            {
                var newPassword = _userService.RandomString();
                _userService.Update(userId: id, password: newPassword);
                return View(newPassword);
            }
            return View("Invalid code");
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
            var userView = _userService.Details(userName);
            userView.UserRegisteredIp = UserSession.IpAddress;
            userView.RegisteredMacAddress = UserSession.MacAddress;
            userView.LastActivityIp = UserSession.IpAddress;
            userView.LastActiveMacAddress = UserSession.MacAddress;
            return View(userView);
        }

        [HttpPost]
        [Authorize]
        public JsonResult Edit(UserView userView)
        {
            userView.UserRegisteredIp = UserSession.IpAddress;
            userView.RegisteredMacAddress = UserSession.MacAddress;
            userView.LastActivityIp = UserSession.IpAddress;
            userView.LastActiveMacAddress = UserSession.MacAddress;
            userView.UserId = _userService.Update(userView, false);
            return Json(new { userView });
        }

        [HttpGet]
        [Authorize]
        public ActionResult Details(int id)
        {
            var userView = _userService.Details(id);

            var systemTimeZones = TimeZoneInfo.GetSystemTimeZones();
            userView.TimezonesList = (from t in systemTimeZones
                orderby t.Id
                select new SelectListItem
                {
                    Value = t.Id,
                    Text = t.Id
                }).ToList();
            userView.TimezonesList.Single(x => x.Value == userView.RegisteredTimeZone).Selected = true;
            var employmentTypes = Get.EmploymentTypes();
            employmentTypes.First(x => x.Value == userView.EmploymentTypeId.ToString()).Selected = true;
            ViewBag.EmploymentType = employmentTypes;

            var userTypes = Get.UserTypes(true);
            userTypes.First(x => x.Value == userView.UserTypeId.ToString()).Selected = true;
            ViewBag.UserTypes = userTypes;

            if (userView.LastPunch.PunchIn != TimeSpan.MinValue)
            {
                userView.LastPunch.PunchIn =
                    TimeZoneInfo.ConvertTimeFromUtc(userView.LastPunch.PunchDate.Date + userView.LastPunch.PunchIn,
                            TimeZoneInfo.FindSystemTimeZoneById(OperatingUser.RegisteredTimeZone))
                        .TimeOfDay;
            }
            if (userView.LastPunch.PunchOut != null && userView.LastPunch.PunchOut != TimeSpan.MinValue)
            {
                userView.LastPunch.PunchOut = TimeZoneInfo
                    .ConvertTimeFromUtc(userView.LastPunch.PunchDate.Date + userView.LastPunch.PunchOut.Value,
                        TimeZoneInfo.FindSystemTimeZoneById(OperatingUser.RegisteredTimeZone))
                    .TimeOfDay;
            }
            return PartialView("_Details", userView);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Details(UserView userView, bool adminUpdate = false)
        {
            _userService.Update(userView, adminUpdate);
            return Json(new { user = userView });
        }
    }
}
