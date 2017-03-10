using PunchClock.Common;
using PunchClock.Implementation;
using PunchClock.Objects.Core;
using System;
using System.Collections.Generic;
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
            if (Get.AdminUsers().Any(y => operatingUser.UserTypeId.Equals(y)))
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
            PunchObjectLibrary obj = new PunchObjectLibrary();

            int pId;
            int.TryParse(col.Get("models[0][punchId]"), out pId);
            obj.PunchId = pId;

            int uId;
            int.TryParse(col.Get("models[0][userId]"), out uId);
            obj.UserId = uId;

            obj.Comments = col.Get("models[0][comments]");

            bool approved;
            bool.TryParse(col.Get("models[0][isManagerAccepted]"),out approved);
            obj.IsManagerAccepted = approved;

            UserService ub = new UserService();
            string userTimeZoneId = ub.getTimeZoneOfUser(uId);
            //var ci = new System.Globalization.CultureInfo("en-us");
            obj.PunchIn = Parse.ToUtcTime(jsOffsetTime: col.Get("models[0][punchIn]"), timeZoneId: userTimeZoneId);
            obj.PunchOut = Parse.ToUtcTime(jsOffsetTime: col.Get("models[0][punchOut]"), timeZoneId: userTimeZoneId); 

           obj.PunchDate = Parse.ToDate(col.Get("models[0][punchDate]"));
           obj.PunchDate = new DateTime(obj.PunchDate.Year, obj.PunchDate.Month, obj.PunchDate.Day, obj.PunchIn.Hours, obj.PunchIn.Minutes, obj.PunchIn.Seconds);
           obj.PunchDate = TimeZoneInfo.ConvertTimeToUtc(obj.PunchDate, TimeZoneInfo.FindSystemTimeZoneById(operatingUser.RegisteredTimeZone));

           bool isApproved = false;
            if (Get.AdminUsers().Any(y => operatingUser.UserTypeId.Equals(y)))
            {
                PunchService pb = new PunchService();
                isApproved = pb.Approve(punchObj: obj, opUserId: operatingUser.UserId);
            }
            //return Json(new { isSuccess = isApproved });
            return Json(true);
        }

        [Authorize]
        public JsonResult OpenLogs()
        {
            if (Get.AdminUsers().Any(y => operatingUser.UserTypeId.Equals(y)))
            {
                List<PunchObjectLibrary> obj = new List<PunchObjectLibrary>();
                PunchService pb = new PunchService();
                obj = pb.GetOpenLogs(opUserId: operatingUser.UserId);

                foreach (var punch in obj)
                {
                    var pIn = TimeZoneInfo.ConvertTimeFromUtc(punch.PunchDate.Date + punch.PunchIn, TimeZoneInfo.FindSystemTimeZoneById(operatingUser.RegisteredTimeZone));
                    var pOut = punch.PunchOut.HasValue ? TimeZoneInfo.ConvertTimeFromUtc(punch.PunchDate.Date + punch.PunchOut.Value, TimeZoneInfo.FindSystemTimeZoneById(operatingUser.RegisteredTimeZone)) : DateTime.MinValue;

                    if (punch.PunchOut.HasValue)
                        punch.Duration = new TimeSpan(pOut.TimeOfDay.Hours, pOut.TimeOfDay.Minutes, pOut.TimeOfDay.Seconds) - new TimeSpan(pIn.TimeOfDay.Hours, pIn.TimeOfDay.Minutes, pIn.TimeOfDay.Seconds);
                    int tmpHoursInSecs;
                    int.TryParse(punch.Duration.TotalSeconds.ToString(), out tmpHoursInSecs);
                    punch.Hours = tmpHoursInSecs;
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(false, JsonRequestBehavior.AllowGet);
        }

    }
}
