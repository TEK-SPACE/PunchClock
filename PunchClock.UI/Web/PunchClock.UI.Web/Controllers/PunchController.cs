using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PunchClock.View.Model;
using PunchClock.Core.Implementation;
using PunchClock.Domain.Model;
using PunchClock.Helper.Common;
using PunchClock.TimeTracker.Contract;
using PunchClock.TimeTracker.Model;
using PunchClock.TimeTracker.Model.Constants;
using PunchClock.TimeTracker.Service;

namespace PunchClock.UI.Web.Controllers
{
    [Authorize]
    public class PunchController : BaseController
    {
        private readonly UserService _userService;
        private readonly IPunch _punchService;
        private readonly CompanyService _companyService;

        public PunchController()
        {
            _userService= new UserService();
            _punchService = new PunchService();
            _companyService = new CompanyService();
        }
        //
        // GET: /Punch/

        public ActionResult Index()
        {
            Punch punch = _punchService.OpenLogByUser(OperatingUser.Uid);
            return View(punch);
        }

        [HttpPost]
        public ActionResult Index(Punch punch)
        {
            return View(punch);
        }

        [HttpPost]
        public JsonResult InOut(int punchId, string inOrOut, DateTime? differentTime)
        {
            string message = string.Empty;
            bool reqDifferentTime;
            bool.TryParse(Request.Form["reqDifferentTime"].Replace("true,false","true"), out reqDifferentTime);
            DateTime punchDateTime = DateTime.MinValue;
            if (reqDifferentTime)
            {
                if (differentTime != null)
                    punchDateTime = TimeZoneInfo.ConvertTimeToUtc(
                        new DateTime(punchDateTime.Year,
                            punchDateTime.Month,
                            punchDateTime.Day,
                            differentTime.Value.Hour,
                            differentTime.Value.Minute,
                            differentTime.Value.Second),
                        TimeZoneInfo.FindSystemTimeZoneById(OperatingUser.RegisteredTimeZone));
            }
            TimeSpan punchTime = reqDifferentTime ? punchDateTime.TimeOfDay : TimeSpan.MinValue;
            switch (inOrOut)
            {
                case "in":
                    message = _punchService.PunchIn(OperatingUser.Uid, punchTime, UserSession.IpAddress, UserSession.MacAddress);
                    break;
                case "out":
                    message = _punchService.PunchOut(OperatingUser.Uid, punchId, punchTime, UserSession.IpAddress, UserSession.MacAddress);
                    break;
            }
            return Json(message);
        }

        [HttpGet]
        public ActionResult Report()
        {
            ViewBag.IsAdmin = OperatingUser.IsAdmin;
            List<SelectListItem> months =  Get.YearMonths();
            var users = _userService.GetAllCompanyEmployees(companyId: OperatingUser.CompanyId, opUserTypeId: OperatingUser.UserTypeId);
            ViewBag.users = new SelectList(users, "Value", "Text", OperatingUser.Uid);
            ViewBag.Months = months;
            return View();
        }


        [HttpPost]
        public JsonResult Report(DateTime? startDate, DateTime? endDate, int userId = 0)
        {
            ViewBag.Message = "Enter your search criteria";
            if (userId == 0)
                userId = OperatingUser.Uid;
            switch (Request.Form["searchType"])
            {
                case ReportConstant.MonthlyReport:
                    int month;
                    int.TryParse(Request.Form["Month"], out month);
                    startDate = new DateTime(DateTime.Now.Year, month, 1);
                    int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, month);
                    endDate = new DateTime(DateTime.Now.Year, month, daysInMonth, 23, 59, 59);
                    if (DateTimeFormatInfo.CurrentInfo != null)
                        Session["MonthName"] = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
                    break;
                case ReportConstant.DateRangeReport:
                    if (startDate == null || endDate == null) return null;
                    endDate = endDate.Value.Date + new TimeSpan(23, 59, 59);
                    break;
            }

            if (startDate == null || endDate == null) return null;
            var punches = _punchService.Search(opUserId: OperatingUser.Uid, userId: userId,
                stDate: startDate.Value,
                enDate: endDate.Value);

            List<HolidayView> holidayViews = _companyService.GetCompanyHolidays(companyId: OperatingUser.CompanyId,
                userId: userId == 0 ? OperatingUser.Uid : userId, stDate: startDate.Value, enDate: endDate.Value);

            punches.AddRange(from holiday in holidayViews
                let nameOfTheDay = holiday.HolidayDate.ToString("dddd").ToLower()
                where nameOfTheDay != "sunday" && nameOfTheDay != "saturday"
                select new Punch
                {
                    Id = ReportConstant.PunchIdForPaidHoliday,
                    PunchDate = TimeZoneInfo.ConvertTimeToUtc(holiday.HolidayDate.Date,
                        TimeZoneInfo.FindSystemTimeZoneById(OperatingUser.RegisteredTimeZone)),
                    PunchIn = TimeZoneInfo.ConvertTimeToUtc(holiday.HolidayDate.Date + holiday.PunchIn,
                            TimeZoneInfo.FindSystemTimeZoneById(OperatingUser.RegisteredTimeZone))
                        .TimeOfDay,
                    PunchOut = TimeZoneInfo
                        .ConvertTimeToUtc(holiday.HolidayDate.Date + holiday.PunchOut,
                            TimeZoneInfo.FindSystemTimeZoneById(OperatingUser.RegisteredTimeZone))
                        .TimeOfDay,
                    Hours = holiday.Hours ?? 0,
                    Comments = holiday.HolidayName
                });
            foreach (var punch in punches)
            {
                if (punch.Id != ReportConstant.PunchIdForPaidHoliday)
                    punch.PunchDate = TimeZoneInfo.ConvertTimeFromUtc(punch.PunchDate,
                        TimeZoneInfo.FindSystemTimeZoneById(OperatingUser.RegisteredTimeZone));
                var pIn = TimeZoneInfo.ConvertTimeFromUtc(punch.PunchDate.Date + punch.PunchIn,
                    TimeZoneInfo.FindSystemTimeZoneById(OperatingUser.RegisteredTimeZone));
                var pOut = punch.PunchOut.HasValue
                    ? TimeZoneInfo.ConvertTimeFromUtc(punch.PunchDate.Date + punch.PunchOut.Value,
                        TimeZoneInfo.FindSystemTimeZoneById(OperatingUser.RegisteredTimeZone))
                    : DateTime.MinValue;
                if (punch.PunchOut.HasValue)
                    punch.Duration =
                        new TimeSpan(pOut.TimeOfDay.Hours, pOut.TimeOfDay.Minutes, pOut.TimeOfDay.Seconds) -
                        new TimeSpan(pIn.TimeOfDay.Hours, pIn.TimeOfDay.Minutes, pIn.TimeOfDay.Seconds);
                int tmpHoursInSecs;
                int.TryParse(punch.Duration.TotalSeconds.ToString(CultureInfo.InvariantCulture), out tmpHoursInSecs);
                punch.Hours = tmpHoursInSecs;
                if (punch.ApprovalRequired && !punch.Approved)
                    punch.ApprovedHours = 0;
                else
                    punch.ApprovedHours = tmpHoursInSecs;
            }
            punches = punches.OrderBy(x => x.PunchDate).ToList();
            Session.Add("SessionPunchResult", punches);
            return Json(punches);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Add([DataSourceRequest] DataSourceRequest request,
            Punch punch)
        {
            if (punch != null && ModelState.IsValid)
            {
                _punchService.Add(punch);
            }

            return Json(new[] { punch }.ToDataSourceResult(request, ModelState));
        }
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            var punches = _punchService.All();
            punches.ForEach(x=>x.DurationInSeconds = x.Duration.TotalSeconds);
            return Json(punches.ToDataSourceResult(request));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request,
            Punch punch)
        {
            if (punch != null && ModelState.IsValid)
            {
                _punchService.Update(punch);
            }

            return Json(new[] { punch }.ToDataSourceResult(request, ModelState));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete([DataSourceRequest] DataSourceRequest request,
            Punch punch)
        {
            if (punch != null && ModelState.IsValid)
            {
                _punchService.Delete(punch);
            }
            return Json(new[] { punch }.ToDataSourceResult(request, ModelState));
        }
        [HttpPost]
        public ActionResult ExportSave(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }
        [HttpPost]
        public JsonResult Approve(int id)
        {
            _punchService.Approve(id);
            return Json(true);
        }
        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult Approve([DataSourceRequest] DataSourceRequest request,
        //    Punch punch)
        //{
        //    if (punch != null && ModelState.IsValid)
        //    {
        //        _punchService.Approve(punch);
        //    }

        //    return Json(new[] { punch }.ToDataSourceResult(request, ModelState));
        //}
        public FileResult Export()
        {
            List<Punch> punches;
            if (Session["SessionPunchResult"] != null)
                punches = (List<Punch>) Session["SessionPunchResult"];
            else
                return File("", "application/pdf", "NoDataFound.pdf");

            if (!punches.Any())
                return null;

            var userId = punches.First().UserId;

            // step 1: creation of a document-object
            var document = new Document(PageSize.A4, 10, 10, 10, 10);

            //step 2: we create a memory stream that listens to the document
            var output = new MemoryStream();
            PdfWriter.GetInstance(document, output);

            //step 3: we open the document
            document.Open();

            //step 4: we add content to the document
            var numOfColumns = 5;
            var dataTable = new PdfPTable(numOfColumns);

            dataTable.DefaultCell.Padding = 3;

            dataTable.DefaultCell.BorderWidth = 2;
            dataTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            User userDetails = _userService.Details(userId);
            var employeeName = $"{userDetails.FirstName} {userDetails.MiddleName} {userDetails.LastName}";

            //Adding Company
            if (OperatingUser.Company.LogoBinary != null)
            {
                Image gif = Image.GetInstance(OperatingUser.Company.LogoBinary);
                gif.ScaleAbsoluteWidth(140);
                gif.ScaleAbsoluteHeight(30);
                dataTable.AddCell(new PdfPCell(gif)
                {
                    Colspan = 3,
                    BorderWidth = 0,
                    PaddingTop = 5,
                    PaddingBottom = 5,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });
            }
            else
            {
                dataTable.AddCell(new PdfPCell(new Phrase("PunchClock"))
                {
                    Colspan = 3,
                    BorderWidth = 0,
                    PaddingTop = 5,
                    PaddingBottom = 5,
                    BorderWidthRight = 0,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });
            }
            dataTable.AddCell(new PdfPCell(
                new Phrase(employeeName + "\n" +
                           (!string.IsNullOrEmpty(Session["MonthName"]?.ToString())
                               ? Session["MonthName"] + " " + DateTime.Now.Year
                               : punches.First().PunchDate.ToString("d") + " - " +
                                 punches.Last().PunchDate.ToString("d"))
                ))
            {
                Colspan = 2,
                BorderWidth = 0,
                BorderWidthLeft = 0,
                PaddingTop = 5,
                PaddingBottom = 5,
                HorizontalAlignment = Element.ALIGN_RIGHT
            });

            // Adding headers
            dataTable.AddCell(new PdfPCell(new Phrase("Date"))
            {
                BackgroundColor = new BaseColor(233, 244, 249),
                BorderWidth = 1,
                PaddingTop = 5,
                PaddingBottom = 5,
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            dataTable.AddCell(new PdfPCell(new Phrase("In-Time"))
            {
                BackgroundColor = new BaseColor(233, 244, 249),
                BorderWidth = 1,
                PaddingTop = 5,
                PaddingBottom = 5,
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            dataTable.AddCell(new PdfPCell(new Phrase("Out-Time"))
            {
                BackgroundColor = new BaseColor(233, 244, 249),
                BorderWidth = 1,
                PaddingTop = 5,
                PaddingBottom = 5,
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            dataTable.AddCell(new PdfPCell(new Phrase("Hours"))
            {
                BackgroundColor = new BaseColor(233, 244, 249),
                BorderWidth = 1,
                PaddingTop = 5,
                PaddingBottom = 5,
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            dataTable.AddCell(new PdfPCell(new Phrase("Comments"))
            {
                BackgroundColor = new BaseColor(233, 244, 249),
                BorderWidth = 1,
                PaddingTop = 5,
                PaddingBottom = 5,
                HorizontalAlignment = Element.ALIGN_CENTER
            });

            dataTable.HeaderRows = 1;
            dataTable.DefaultCell.BorderWidth = 1;
            List<TimeSpan> timeSpanCollection = new List<TimeSpan>();
            foreach (Punch punch in punches)
            {

                var pIn = TimeZoneInfo.ConvertTimeFromUtc(punch.PunchDate.Date + punch.PunchIn,
                    TimeZoneInfo.FindSystemTimeZoneById(OperatingUser.RegisteredTimeZone));
                var pOut = punch.PunchOut.HasValue
                    ? TimeZoneInfo.ConvertTimeFromUtc(punch.PunchDate.Date + punch.PunchOut.Value,
                        TimeZoneInfo.FindSystemTimeZoneById(OperatingUser.RegisteredTimeZone))
                    : DateTime.MinValue;
                if (punch.Id == -69)
                {
                    dataTable.AddCell(punch.PunchDate.ToString("d"));
                    dataTable.AddCell("N/A");
                    dataTable.AddCell("N/A");
                }
                else
                {
                    dataTable.AddCell(TimeZoneInfo
                        .ConvertTimeFromUtc(punch.PunchDate,
                            TimeZoneInfo.FindSystemTimeZoneById(OperatingUser.RegisteredTimeZone))
                        .ToString("d"));
                    dataTable.AddCell(pIn.ToString(@"hh\:mm\:ss tt"));
                    dataTable.AddCell(pOut != DateTime.MinValue ? pOut.ToString(@"hh\:mm\:ss tt") : "");
                }
                punch.Duration = new TimeSpan(pOut.TimeOfDay.Hours, pOut.TimeOfDay.Minutes, pOut.TimeOfDay.Seconds) -
                                 new TimeSpan(pIn.TimeOfDay.Hours, pIn.TimeOfDay.Minutes, pIn.TimeOfDay.Seconds);
                if (punch.Duration.TotalMinutes > 0)
                {

                    if (punch.ApprovalRequired && !punch.Approved)
                        dataTable.AddCell(new PdfPCell(new Phrase(punch.Duration.ToString(@"hh\:mm\:ss")))
                        {
                            BackgroundColor = new BaseColor(233, 50, 50),
                            BorderWidth = 1,
                            PaddingLeft = 3,
                            PaddingTop = 3,
                            PaddingBottom = 3,
                            HorizontalAlignment = Element.ALIGN_CENTER
                        });
                    else
                    {
                        timeSpanCollection.Add(punch.Duration);
                        dataTable.AddCell(punch.Duration.ToString(@"hh\:mm\:ss"));
                    }
                }
                else
                    dataTable.AddCell("");
                if (punch.Id == -69)
                    dataTable.AddCell(new PdfPCell(new Phrase(punch.Comments))
                    {
                        BackgroundColor = new BaseColor(186, 248, 189),
                        BorderWidth = 1,
                        PaddingLeft = 3,
                        PaddingTop = 3,
                        PaddingBottom = 3,
                        HorizontalAlignment = Element.ALIGN_CENTER
                    });
                else if (punch.ApprovalRequired && !punch.Approved)
                    dataTable.AddCell("Req. Approval");
                else
                    dataTable.AddCell("");
            }
            if (dataTable.Rows.Count > 0)
            {
                var totalHours = new TimeSpan(timeSpanCollection.Sum(r => r.Duration().Ticks));
                int compTotalHours = 0;
                for (int i = 1; i <= totalHours.Days; i++)
                {
                    compTotalHours = i * 24;
                }
                dataTable.AddCell(new PdfPCell(new Phrase(""
                ))
                {
                    BackgroundColor = new BaseColor(233, 244, 249),
                    BorderWidth = 0,
                    PaddingTop = 5,
                    PaddingBottom = 5,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });
                dataTable.AddCell(new PdfPCell(new Phrase(""))
                {
                    BackgroundColor = new BaseColor(233, 244, 249),
                    BorderWidth = 0,
                    PaddingTop = 5,
                    PaddingBottom = 5,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });
                dataTable.AddCell(new PdfPCell(new Phrase(""))
                {
                    BackgroundColor = new BaseColor(233, 244, 249),
                    BorderWidth = 0,
                    PaddingTop = 5,
                    PaddingBottom = 5,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });
                dataTable.AddCell(new PdfPCell(new Phrase(compTotalHours + totalHours.Hours + "h:"
                                                          +
                                                          (totalHours.Minutes < 10
                                                              ? "0" + totalHours.Minutes.ToString()
                                                              : totalHours.Minutes.ToString()) + "m:"
                                                          +
                                                          (totalHours.Seconds < 10
                                                              ? "0" + totalHours.Seconds.ToString()
                                                              : totalHours.Seconds.ToString())
                                                          + "s"))
                {
                    BackgroundColor = new BaseColor(233, 244, 249),
                    BorderWidth = 0,
                    PaddingTop = 5,
                    PaddingBottom = 5,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });

                dataTable.AddCell(new PdfPCell(new Phrase(""))
                {
                    BackgroundColor = new BaseColor(233, 244, 249),
                    BorderWidth = 0,
                    PaddingLeft = 3,
                    PaddingTop = 5,
                    PaddingBottom = 5,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });
            }
            // Add table to the document
            document.Add(dataTable);

            //This is important don't forget to close the document
            document.Close();
            Session.Remove("MonthName");
            // send the memory stream as File
            return File(output.ToArray(), "application/pdf", "PunchReport.pdf");
        }

        public static TimeSpan TotalTime(IEnumerable<TimeSpan> theCollection)
        {
            int i;
            int totalSeconds = 0;
            var arrayDuration = theCollection.ToArray();
            for (i = 0; i < arrayDuration.Length; i++)
            {
                totalSeconds = (int) (arrayDuration[i].TotalSeconds) + totalSeconds;
            }
            return TimeSpan.FromSeconds(totalSeconds);
        }
    }
}
