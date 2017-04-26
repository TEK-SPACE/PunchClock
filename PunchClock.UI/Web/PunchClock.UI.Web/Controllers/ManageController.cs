using PunchClock.Common;
using PunchClock.Implementation;
using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace PunchClock.UI.Web.Controllers
{
    [Authorize]
    public class ManageController : BaseController
    {
        //
        // GET: /Manage/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logs()
        {
            if (Get.AdminUsers().Any(y => OperatingUser.UserTypeId.Equals(y)))
            {
                return View();
            }
            else
                return Content("aaaha, please do not be smart", "text");
           
        }

        [Authorize]
        [HttpPost]
        public JsonResult Approve( FormCollection col)
        {
            View.Model.PunchView punch = new View.Model.PunchView();

            int pId;
            int.TryParse(col.Get("models[0][punchId]"), out pId);
            punch.PunchId = pId;

            int uId;
            int.TryParse(col.Get("models[0][userId]"), out uId);
            punch.UserId = uId;

            punch.Comments = col.Get("models[0][comments]");

            bool approved;
            bool.TryParse(col.Get("models[0][isManagerAccepted]"),out approved);
            punch.IsManagerAccepted = approved;

            UserService ub = new UserService();
            string userTimeZoneId = ub.GetTimeZoneOfUser(uId);
            //var ci = new System.Globalization.CultureInfo("en-us");
            punch.PunchIn = Parse.ToUtcTime(jsOffsetTime: col.Get("models[0][punchIn]"), timeZoneId: userTimeZoneId);
            punch.PunchOut = Parse.ToUtcTime(jsOffsetTime: col.Get("models[0][punchOut]"), timeZoneId: userTimeZoneId); 

           punch.PunchDate = Parse.ToDate(col.Get("models[0][punchDate]"));
           punch.PunchDate = new DateTime(punch.PunchDate.Year, punch.PunchDate.Month, punch.PunchDate.Day, punch.PunchIn.Hours, punch.PunchIn.Minutes, punch.PunchIn.Seconds);
           punch.PunchDate = TimeZoneInfo.ConvertTimeToUtc(punch.PunchDate, TimeZoneInfo.FindSystemTimeZoneById(OperatingUser.RegisteredTimeZone));

           bool isApproved = false;
            if (Get.AdminUsers().Any(y => OperatingUser.UserTypeId.Equals(y)))
            {
                PunchService pb = new PunchService();
                isApproved = pb.Approve(punchObj: punch, opUserId: OperatingUser.UserId);
            }
            //return Json(new { isSuccess = isApproved });
            return Json(isApproved);
        }

        [Authorize]
        public JsonResult OpenLogs()
        {
            if (Get.AdminUsers().Any(y => OperatingUser.UserTypeId.Equals(y)))
            {
                PunchService punchService = new PunchService();
                var obj = punchService.GetOpenLogs(opUserId: OperatingUser.UserId);

                foreach (var punch in obj)
                {
                    var pIn = TimeZoneInfo.ConvertTimeFromUtc(punch.PunchDate.Date + punch.PunchIn, TimeZoneInfo.FindSystemTimeZoneById(OperatingUser.RegisteredTimeZone));
                    var pOut = punch.PunchOut.HasValue ? TimeZoneInfo.ConvertTimeFromUtc(punch.PunchDate.Date + punch.PunchOut.Value, TimeZoneInfo.FindSystemTimeZoneById(OperatingUser.RegisteredTimeZone)) : DateTime.MinValue;

                    if (punch.PunchOut.HasValue)
                        punch.Duration = new TimeSpan(pOut.TimeOfDay.Hours, pOut.TimeOfDay.Minutes, pOut.TimeOfDay.Seconds) - new TimeSpan(pIn.TimeOfDay.Hours, pIn.TimeOfDay.Minutes, pIn.TimeOfDay.Seconds);
                    int tmpHoursInSecs;
                    int.TryParse(punch.Duration.TotalSeconds.ToString(CultureInfo.InvariantCulture), out tmpHoursInSecs);
                    punch.Hours = tmpHoursInSecs;
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(false, JsonRequestBehavior.AllowGet);
        }

    }
}
