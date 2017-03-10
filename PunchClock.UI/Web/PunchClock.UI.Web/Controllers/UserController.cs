using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.ObjectModel;
using PunchClock.Objects.Core;
using PunchClock.Implementation;
using PunchClock.Common;
using PunchClock.Objects.Core.Enum;

namespace PunchClock.UI.Web.Controllers
{
    public class UserController : BaseController
    {
        //
        // GET: /Register/

        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Edit", "User", new { userName = operatingUser.UserName });
            UserObjLibrary user = new UserObjLibrary();
            user.LastActivity_ip = userSession.IpAddress;
            user.LastActive_MAC_address = userSession.MacAddress;

            ReadOnlyCollection<TimeZoneInfo> tz;
            tz = TimeZoneInfo.GetSystemTimeZones();
            user.TimezonesList = (from t in tz
                                  orderby t.Id
                                  select new SelectListItem
                                  {
                                       Value = t.Id,
                                       Text = t.Id
                                  }).ToList();
            user.TimezonesList.Where(x => x.Value == "US Eastern Standard Time").Single().Selected = true;

            var UserTypes = Get.UserTypes(adminCall: true);
            if (user.UserTypeId > 0)
                UserTypes.Where(x => x.Value == user.UserTypeId.ToString()).First().Selected = true;
            ViewBag.UserTypes = UserTypes;

            return View(user);
        }

        [HttpPost]
        public JsonResult Register(UserObjLibrary user, FormCollection coll)
        {
            user.UserRegistered_ip = userSession.IpAddress;
            user.Registered_MAC_address = userSession.MacAddress;
            user.LastActivity_ip = userSession.IpAddress;
            user.LastActive_MAC_address = userSession.MacAddress;
            user.EmploymentType = (int)EmploymentType.ContractHourly; // this is default employemnt type at registration. later admin can set the type
            UserService ub = new UserService();
            user.UserId = ub.Add(user);
            return Json(new { user = user });
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                userName = operatingUser.UserName;
            UserObjLibrary user = new UserObjLibrary();
            UserService ub = new UserService();
            user = ub.Details(userName);
            user.UserRegistered_ip = userSession.IpAddress;
            user.Registered_MAC_address = userSession.MacAddress;
            user.LastActivity_ip = userSession.IpAddress;
            user.LastActive_MAC_address = userSession.MacAddress;
            return View(user);
        }

        [HttpPost]
        [Authorize]
        public JsonResult Edit(UserObjLibrary user)
        {
            user.UserRegistered_ip = userSession.IpAddress;
            user.Registered_MAC_address = userSession.MacAddress;
            user.LastActivity_ip = userSession.IpAddress;
            user.LastActive_MAC_address = userSession.MacAddress;
            UserService ub = new UserService();
            user.UserId = ub.Update(user, false);
            return Json(new { user = user });
        }
        [HttpGet]
        [Authorize]
        public ActionResult Details(int id)
        {
            UserObjLibrary user = new UserObjLibrary();
            UserService ub = new UserService();
            user = ub.Details(userId: id);

            ReadOnlyCollection<TimeZoneInfo> tz;
            tz = TimeZoneInfo.GetSystemTimeZones();
            user.TimezonesList = (from t in tz
                                  orderby t.Id
                                  select new SelectListItem
                                  {
                                      Value = t.Id,
                                      Text = t.Id
                                  }).ToList();
            user.TimezonesList.Where(x => x.Value == user.RegisteredTimeZone).Single().Selected = true;
            var EmploymentTypes = Get.EmploymentTypes();
            EmploymentTypes.Where(x => x.Value == user.EmploymentType.ToString()).First().Selected = true;
            ViewBag.EmploymentType = EmploymentTypes;

            var UserTypes = Get.UserTypes(adminCall: true);
            UserTypes.Where(x => x.Value == user.UserTypeId.ToString()).First().Selected = true;
            ViewBag.UserTypes = UserTypes;

            if (user.LastPunch.PunchIn != null && user.LastPunch.PunchIn != TimeSpan.MinValue)
            {
                user.LastPunch.PunchIn = 
                    TimeZoneInfo.ConvertTimeFromUtc( user.LastPunch.PunchDate.Date +  user.LastPunch.PunchIn, TimeZoneInfo.FindSystemTimeZoneById(operatingUser.RegisteredTimeZone)).TimeOfDay;
            }
            if (user.LastPunch.PunchOut != null && user.LastPunch.PunchOut != TimeSpan.MinValue)
            {
                user.LastPunch.PunchOut = user.LastPunch.PunchOut.HasValue ? 
                    TimeZoneInfo.ConvertTimeFromUtc(user.LastPunch.PunchDate.Date + user.LastPunch.PunchOut.Value, 
                    TimeZoneInfo.FindSystemTimeZoneById(operatingUser.RegisteredTimeZone)).TimeOfDay : TimeSpan.MinValue;
            }


            return PartialView("_Details", user);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Details(UserObjLibrary obj, bool adminUpdate = false)
        {
            UserObjLibrary user = new UserObjLibrary();
            UserService ub = new UserService();
            user.UserId = ub.Update(obj, adminUpdate);
            return Json(new { user = obj });
        }
    }
}
