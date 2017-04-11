using PunchClock.Common;
using PunchClock.Implementation;
using PunchClock.Objects.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using PunchClock.View.Model;

namespace PunchClock.UI.Web.Controllers
{
    [Authorize]
    public class PunchController : BaseController
    {
        //
        // GET: /Punch/

        public ActionResult Index()
        {
            PunchService punchService = new PunchService();
            PunchView obj = punchService.OpUserOpenLog(operatingUser.UserId);
            return View(obj);
        }

        [HttpPost]
        public ActionResult Index(PunchView obj)
        {
            return View(obj);
        }

        [HttpPost]
        public JsonResult InOut(int pId, string InOut, DateTime? pTime)
        {
            string message = string.Empty;
            PunchService pb = new PunchService();
            bool reqDifferentTime;
            bool.TryParse(Request.Form["reqDifferentTime"].Replace("true,false","true"), out reqDifferentTime);
            DateTime punchDateTime = DateTime.MinValue;
            if (reqDifferentTime)
            {
                punchDateTime = TimeZoneInfo.ConvertTimeToUtc(
                    new DateTime(punchDateTime.Year,
                        punchDateTime.Month,
                        punchDateTime.Day,
                        pTime.Value.Hour,
                        pTime.Value.Minute,
                        pTime.Value.Second),
                    TimeZoneInfo.FindSystemTimeZoneById(operatingUser.RegisteredTimeZone));
            }
            TimeSpan punchTime = reqDifferentTime ? punchDateTime.TimeOfDay : TimeSpan.MinValue;
            if (InOut == "in")
            {
                message = pb.PunchIn(operatingUser.UserId, punchTime, UserUserSession.IpAddress);
            }
            else if (InOut == "out")
            {
                
                message = pb.PunchOut(operatingUser.UserId, pId, punchTime, UserUserSession.IpAddress);
            }

            return Json(message);
        }

        [HttpGet]
        public ActionResult Report()
        {
            ViewBag.Message = "Enter your search criteria";
            List<SelectListItem> Months =  Get.YearMonths();
            UserService UB = new UserService();
            var users = UB.GetAllCompanyEmployees(companyId: operatingUser.CompanyId, opUserTypeId: operatingUser.UserTypeId);
            ViewBag.users = new SelectList(users, "Value", "Text", operatingUser.UserId);
            ViewBag.Months = Months;
            return View();
        }


        [HttpPost]
        public JsonResult Report(DateTime? stDate, DateTime? enDate, int userId = 0)
        {
            ViewBag.Message = "Enter your search criteria";
            List<PunchView> obj = new List<PunchView>();
            if (userId == 0)
                userId = operatingUser.UserId;
            PunchService pb = new PunchService();

            //TimeSpan tss = new TimeSpan(00, 00, 00);
            //stDate = stDate.Date + tss;

            if (Request.Form["searchType"] == "1")
            {
                int month;
                int.TryParse(Request.Form["Month"], out month);
                stDate = new DateTime(DateTime.Now.Year, month, 1);
                int numberOfDays = DateTime.DaysInMonth(DateTime.Now.Year, month);
                enDate = new DateTime(DateTime.Now.Year, month, numberOfDays);
                Session["MonthName"] = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            }

            TimeSpan tse = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            enDate = enDate.Value.Date + tse;

            //stDate = TimeZoneInfo.ConvertTimeToUtc(stDate, TimeZoneInfo.FindSystemTimeZoneById(operatingUser.registeredTimeZone));
            //enDate = TimeZoneInfo.ConvertTimeToUtc(enDate, TimeZoneInfo.FindSystemTimeZoneById(operatingUser.registeredTimeZone));

            obj = pb.Search(operatingUserId: operatingUser.UserId, userId: userId, stDate: stDate.Value, enDate:enDate.Value);

            CompanyService cb = new CompanyService();
            List<HolidayView> holidays = cb.GetCompanyHolidays(companyId: operatingUser.CompanyId, userId: userId == 0 ? operatingUser.UserId : userId, stDate: stDate.Value, enDate: enDate.Value);

            foreach (var holiday in holidays)
            {

                // If holiday is not weekend
                string nameOfTheDay = holiday.HolidayDate.Value.ToString("dddd", new System.Globalization.CultureInfo("en-US")).ToLower();

                if (nameOfTheDay != "sunday" && nameOfTheDay != "saturday")
                {
                    obj.Add(new PunchView
                    {
                        PunchId = -69,
                        PunchDate = TimeZoneInfo.ConvertTimeToUtc(holiday.HolidayDate.Value.Date, TimeZoneInfo.FindSystemTimeZoneById(operatingUser.RegisteredTimeZone)),
                        PunchIn = TimeZoneInfo.ConvertTimeToUtc(holiday.HolidayDate.Value.Date + holiday.PunchIn, TimeZoneInfo.FindSystemTimeZoneById(operatingUser.RegisteredTimeZone)).TimeOfDay,
                        PunchOut = TimeZoneInfo.ConvertTimeToUtc(holiday.HolidayDate.Value.Date + holiday.PunchOut.Value, TimeZoneInfo.FindSystemTimeZoneById(operatingUser.RegisteredTimeZone)).TimeOfDay,
                        Hours = holiday.Hours,
                        Comments = holiday.HolidayName
                    });
                }

            }
            foreach (var punch in obj)
            {
                if (punch.PunchId != -69)
                 punch.PunchDate = TimeZoneInfo.ConvertTimeFromUtc(punch.PunchDate, TimeZoneInfo.FindSystemTimeZoneById(operatingUser.RegisteredTimeZone));
                
                //punch.punchIn = TimeZoneInfo.ConvertTimeFromUtc(punch.punchDate.Date + punch.punchIn, TimeZoneInfo.FindSystemTimeZoneById(operatingUser.registeredTimeZone)).TimeOfDay;
                 //punch.punchOut = punch.punchOut.HasValue ? TimeZoneInfo.ConvertTimeFromUtc(punch.punchDate.Date + punch.punchOut.Value, TimeZoneInfo.FindSystemTimeZoneById(operatingUser.registeredTimeZone)).TimeOfDay : new TimeSpan(0,0,0);

                 var pIn = TimeZoneInfo.ConvertTimeFromUtc(punch.PunchDate.Date + punch.PunchIn, TimeZoneInfo.FindSystemTimeZoneById(operatingUser.RegisteredTimeZone));
                 var pOut = punch.PunchOut.HasValue ? TimeZoneInfo.ConvertTimeFromUtc(punch.PunchDate.Date + punch.PunchOut.Value, TimeZoneInfo.FindSystemTimeZoneById(operatingUser.RegisteredTimeZone)) : DateTime.MinValue;

                 if (punch.PunchOut.HasValue)
                     punch.Duration = new TimeSpan(pOut.TimeOfDay.Hours, pOut.TimeOfDay.Minutes, pOut.TimeOfDay.Seconds) - new TimeSpan(pIn.TimeOfDay.Hours, pIn.TimeOfDay.Minutes, pIn.TimeOfDay.Seconds); 
                 int tmpHoursInSecs;
                 int.TryParse(punch.Duration.TotalSeconds.ToString(), out tmpHoursInSecs);
                 punch.Hours = tmpHoursInSecs;
                 if (punch.RequestForApproval && !punch.IsManagerAccepted)
                     punch.ApprovedHours = 0;
                 else
                     punch.ApprovedHours = tmpHoursInSecs;
            }
            obj = obj.OrderBy(x => x.PunchDate).ToList();
            Session.Add("punchObjectLibrary", obj);
            return Json(obj);
        }

        public FileResult Export()
        {
            List<PunchView> obj = new List<PunchView>();
            if (Session["punchObjectLibrary"] != null)
                obj = (List<PunchView>)Session["punchObjectLibrary"];
            else
                return File("", "application/pdf", "NoDataFound.pdf");
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
            UserService UB = new UserService();
            UserView userDetails = UB.Details(obj.FirstOrDefault().UserId);
            var EmployeeName = string.Format("{0} {1} {2}", userDetails.FirstName, userDetails.MiddleName, userDetails.LastName);

            //Adding Company
            if (operatingUser.Company.LogoBinary != null)
            {
                //string imageBase64 = Convert.ToBase64String(operatingUser.tCompany.logoBinary);
                //string imageSrc = string.Format("data:image/gif;base64,{0}", imageBase64);
                Image gif = Image.GetInstance(operatingUser.Company.LogoBinary);
                gif.ScaleAbsoluteWidth(140);
                gif.ScaleAbsoluteHeight(30);
                dataTable.AddCell(new PdfPCell(gif) { Colspan = 3, BorderWidth = 0, PaddingTop = 5, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_LEFT });
            }
            else
            {
                dataTable.AddCell(new PdfPCell(new Phrase("PunchClock")) { Colspan = 3, BorderWidth = 0, PaddingTop = 5, PaddingBottom = 5, BorderWidthRight=0, HorizontalAlignment = Element.ALIGN_LEFT });
            }
            // Add report title
            //obj.Min(x => x.punchDate.ToString("d")) 
            dataTable.AddCell(new PdfPCell(
                new Phrase(EmployeeName+"\n"+
                    (Session["MonthName"] != null && !string.IsNullOrEmpty(Session["MonthName"].ToString()) ?
                    Session["MonthName"].ToString() +" "+DateTime.Now.Year :
                    obj.First().PunchDate.ToString("d") + " - " + obj.Last().PunchDate.ToString("d"))
            )) { Colspan = 2, BorderWidth = 0, BorderWidthLeft=0, PaddingTop = 5, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_RIGHT });
           
            // Adding headers
            dataTable.AddCell(new PdfPCell(new Phrase("Date")) { BackgroundColor = new BaseColor(233, 244, 249), BorderWidth = 1, PaddingTop = 5, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER });
            dataTable.AddCell(new PdfPCell(new Phrase("In-Time")) { BackgroundColor = new BaseColor(233, 244, 249), BorderWidth = 1, PaddingTop = 5, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER });
            dataTable.AddCell(new PdfPCell(new Phrase("Out-Time")) { BackgroundColor = new BaseColor(233, 244, 249), BorderWidth = 1, PaddingTop = 5, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER });
            dataTable.AddCell(new PdfPCell(new Phrase("Hours")) { BackgroundColor = new BaseColor(233, 244, 249), BorderWidth = 1, PaddingTop = 5, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER });
            dataTable.AddCell(new PdfPCell(new Phrase("Comments")) { BackgroundColor = new BaseColor(233, 244, 249), BorderWidth = 1, PaddingTop = 5, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER });

            dataTable.HeaderRows = 1;
            dataTable.DefaultCell.BorderWidth = 1;
            List<TimeSpan> TimeSpanCollection = new List<TimeSpan>();
            foreach (PunchView punch in obj)
            {
                
                var pIn = TimeZoneInfo.ConvertTimeFromUtc(punch.PunchDate.Date + punch.PunchIn, TimeZoneInfo.FindSystemTimeZoneById(operatingUser.RegisteredTimeZone));
                var pOut = punch.PunchOut.HasValue ? TimeZoneInfo.ConvertTimeFromUtc(punch.PunchDate.Date + punch.PunchOut.Value, TimeZoneInfo.FindSystemTimeZoneById(operatingUser.RegisteredTimeZone)): DateTime.MinValue;
                //dataTable.AddCell(TimeZoneInfo.ConvertTimeFromUtc(punch.punchDate, TimeZoneInfo.FindSystemTimeZoneById(operatingUser.registeredTimeZone)).ToString("d"));
                if (punch.PunchId == -69)
                {
                    dataTable.AddCell(punch.PunchDate.ToString("d"));
                    dataTable.AddCell("N/A");
                    dataTable.AddCell("N/A");
                }
                else
                {
                    dataTable.AddCell(TimeZoneInfo.ConvertTimeFromUtc(punch.PunchDate, TimeZoneInfo.FindSystemTimeZoneById(operatingUser.RegisteredTimeZone)).ToString("d"));
                    dataTable.AddCell(pIn.ToString(@"hh\:mm\:ss tt"));
                    dataTable.AddCell(pOut != DateTime.MinValue ? pOut.ToString(@"hh\:mm\:ss tt") : "");
                }
                punch.Duration = new TimeSpan(pOut.TimeOfDay.Hours, pOut.TimeOfDay.Minutes, pOut.TimeOfDay.Seconds) - new TimeSpan(pIn.TimeOfDay.Hours, pIn.TimeOfDay.Minutes, pIn.TimeOfDay.Seconds);
                if (punch.Duration.TotalMinutes > 0)
                {

                    if (punch.RequestForApproval && !punch.IsManagerAccepted)
                        dataTable.AddCell(new PdfPCell(new Phrase(punch.Duration.ToString(@"hh\:mm\:ss"))) { BackgroundColor = new BaseColor(233, 50, 50), BorderWidth = 1, PaddingLeft = 3, PaddingTop = 3, PaddingBottom = 3, HorizontalAlignment = Element.ALIGN_CENTER });
                    else
                    {
                        TimeSpanCollection.Add(punch.Duration);
                        dataTable.AddCell(punch.Duration.ToString(@"hh\:mm\:ss"));
                    }
                }
                else
                    dataTable.AddCell("");
                 if (punch.PunchId == -69)
                    dataTable.AddCell(new PdfPCell(new Phrase(punch.Comments)) { BackgroundColor = new BaseColor(186, 248, 189), BorderWidth = 1, PaddingLeft = 3, PaddingTop = 3, PaddingBottom = 3, HorizontalAlignment = Element.ALIGN_CENTER });
                else if (punch.RequestForApproval && !punch.IsManagerAccepted)
                    dataTable.AddCell("Req. Approval");//dataTable.AddCell(punch.comments);
                else
                    dataTable.AddCell("");
            }
            if (dataTable.Rows.Count > 0)
            {
                var TotalHours = new TimeSpan(TimeSpanCollection.Sum(r => r.Duration().Ticks));
                int compTotalHours =0;
                for (int i = 1; i <= TotalHours.Days; i++)
                {
                    compTotalHours = i * 24;
                }
                dataTable.AddCell(new PdfPCell(new Phrase(""
                    //"Fr:"+obj.Min(x => x.punchDate.ToString("d")) + " To:" + obj.Max(x => x.punchDate.ToString("d"))
                    )) { BackgroundColor = new BaseColor(233, 244, 249), BorderWidth = 0, PaddingTop = 5, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_LEFT });
                dataTable.AddCell(new PdfPCell(new Phrase("")) { BackgroundColor = new BaseColor(233, 244, 249), BorderWidth = 0, PaddingTop = 5, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_LEFT });
                dataTable.AddCell(new PdfPCell(new Phrase("")) { BackgroundColor = new BaseColor(233, 244, 249), BorderWidth = 0, PaddingTop = 5, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_LEFT });
                dataTable.AddCell(new PdfPCell(new Phrase(compTotalHours+ TotalHours.Hours + "h:"
                    + (TotalHours.Minutes < 10 ? "0" + TotalHours.Minutes.ToString() : TotalHours.Minutes.ToString())+ "m:"
                    + (TotalHours.Seconds < 10 ? "0" + TotalHours.Seconds.ToString() : TotalHours.Seconds.ToString()) 
                    + "s" )) { BackgroundColor = new BaseColor(233, 244, 249), BorderWidth = 0, PaddingTop = 5, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_LEFT });

                dataTable.AddCell(new PdfPCell(new Phrase("")) { BackgroundColor = new BaseColor(233, 244, 249), BorderWidth = 0, PaddingLeft = 3, PaddingTop = 5, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_LEFT });
            }
            // Add table to the document
            document.Add(dataTable);

            //This is important don't forget to close the document
            document.Close();
            Session.Remove("MonthName");
            // send the memory stream as File
            return File(output.ToArray(), "application/pdf", "PunchReport.pdf");

        }

        public static TimeSpan TotalTime(IEnumerable<TimeSpan> TheCollection)
        {
            int i = 0;
            int TotalSeconds = 0;

            var ArrayDuration = TheCollection.ToArray();

            for (i = 0; i < ArrayDuration.Length; i++)
            {
                TotalSeconds = (int)(ArrayDuration[i].TotalSeconds) + TotalSeconds;
            }

            return TimeSpan.FromSeconds(TotalSeconds);
        }

        //[HttpGet]
        //public ActionResult Test()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ContentResult Test(DateTime punchIn)
        //{
        //    TimeZone localZone = TimeZone.CurrentTimeZone;
            
        //    bool reqDifferentTime;
        //    DateTime punchDateTime = DateTime.MinValue;
        //    punchDateTime = punchIn.ToUniversalTime();
        //    return Content(localZone.StandardName+ " " + punchIn.ToString() + " " + punchDateTime.TimeOfDay.ToString(), "text");
        //}

    }
}
