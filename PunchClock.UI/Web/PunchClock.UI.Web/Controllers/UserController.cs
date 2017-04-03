using System;
using System.Linq;
using System.Web.Mvc;
using PunchClock.Implementation;
using PunchClock.Common;
using PunchClock.Objects.Core.Enum;
using PunchClock.View.Model;
using PunchClock.UI.Web.Models;
using System.Threading.Tasks;
using System.Web.Security;

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
                return RedirectToAction("Edit", "User", new { userName = operatingUser.UserName });
            UserView user = new UserView
            {
                LastActivityIp = UserUserSession.IpAddress,
                LastActiveMacAddress = UserUserSession.MacAddress
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

            var userTypes = Get.UserTypes(adminCall: true);
            if (user.UserTypeId > 0)
                userTypes.First(x => x.Value == user.UserTypeId.ToString()).Selected = true;
            ViewBag.UserTypes = userTypes;

            return View(user);
        }

        [HttpPost]
        public JsonResult Register(UserView user, FormCollection coll)
        {
            user.UserRegisteredIp = UserUserSession.IpAddress;
            user.RegisteredMacAddress= UserUserSession.MacAddress;
            user.LastActivityIp = UserUserSession.IpAddress;
            user.LastActiveMacAddress = UserUserSession.MacAddress;
            user.EmploymentTypeId = (int)EmploymentType.ContractHourly; // this is default employemnt type at registration. later admin can set the type
            user.UserId = _userService.Add(user);
            return Json(new {user });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var success = UserService.Login(model.UserName, model.Password, UserUserSession.IpAddress, UserUserSession.MacAddress);
            if (success)
            {
                FormsAuthentication.SetAuthCookie(model.UserName, true);
                return RedirectToLocal(returnUrl);
            }
            return View(model);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                userName = operatingUser.UserName;
            var user = _userService.Details(userName);
            user.UserRegisteredIp = UserUserSession.IpAddress;
            user.RegisteredMacAddress = UserUserSession.MacAddress;
            user.LastActivityIp = UserUserSession.IpAddress;
            user.LastActiveMacAddress = UserUserSession.MacAddress;
            return View(user);
        }

        [HttpPost]
        [Authorize]
        public JsonResult Edit(UserView user)
        {
            user.UserRegisteredIp = UserUserSession.IpAddress;
            user.RegisteredMacAddress = UserUserSession.MacAddress;
            user.LastActivityIp = UserUserSession.IpAddress;
            user.LastActiveMacAddress = UserUserSession.MacAddress;
            user.UserId = _userService.Update(user, false);
            return Json(new {user });
        }
        [HttpGet]
        [Authorize]
        public ActionResult Details(int id)
        {
           var user = _userService.Details(userId: id);

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

            var userTypes = Get.UserTypes(adminCall: true);
            userTypes.First(x => x.Value == user.UserTypeId.ToString()).Selected = true;
            ViewBag.UserTypes = userTypes;

            if (user.LastPunch.PunchIn != TimeSpan.MinValue)
            {
                user.LastPunch.PunchIn = 
                    TimeZoneInfo.ConvertTimeFromUtc( user.LastPunch.PunchDate.Date +  user.LastPunch.PunchIn, TimeZoneInfo.FindSystemTimeZoneById(operatingUser.RegisteredTimeZone)).TimeOfDay;
            }
            if (user.LastPunch.PunchOut != null && user.LastPunch.PunchOut != TimeSpan.MinValue)
            {
                user.LastPunch.PunchOut = TimeZoneInfo.ConvertTimeFromUtc(user.LastPunch.PunchDate.Date + user.LastPunch.PunchOut.Value, 
                    TimeZoneInfo.FindSystemTimeZoneById(operatingUser.RegisteredTimeZone)).TimeOfDay;
            }


            return PartialView("_Details", user);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Details(UserView obj, bool adminUpdate = false)
        {
            _userService.Update(obj, adminUpdate);
            return Json(new { user = obj });
        }
    }
}
