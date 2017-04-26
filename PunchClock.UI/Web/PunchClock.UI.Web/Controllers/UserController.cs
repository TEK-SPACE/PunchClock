using System;
using System.Linq;
using System.Web.Mvc;
using PunchClock.Implementation;
using PunchClock.Common;
using PunchClock.Objects.Core.Enum;
using PunchClock.View.Model;

namespace PunchClock.UI.Web.Controllers
{
    public class UserController : BaseController
    {
        //
        // GET: /Register/
        private readonly UserService _userService;

        public UserController()
        {
            _userService = new UserService();
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
