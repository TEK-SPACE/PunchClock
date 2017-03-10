using System;
using System.Linq;

namespace PunchClock.Common
{
    public static class Parse
    {
        public static DateTime ToDate(string jsOffsetDate)
        {
            DateTime retDate;
            if (!DateTime.TryParse(jsOffsetDate, out retDate))
            {
                var dateArray = jsOffsetDate.Split(' ').ToArray();
                var shortDate = dateArray[0] + " " + dateArray[1] + " " + dateArray[2] + " " + dateArray[3] + " " + DateTime.UtcNow.TimeOfDay;
                DateTime.TryParse(shortDate, out retDate);
            }
            return retDate;
        }

        public static TimeSpan ToUtcTime(string jsOffsetTime, string timeZoneId)
        {
            DateTime retDate;
            if (!DateTime.TryParse(jsOffsetTime, out retDate))
            {
                var dateArray = jsOffsetTime.Split(' ').ToArray();
                var shortDate = DateTime.Now.Date.ToString("d") + " " + dateArray[4];
                DateTime.TryParse(shortDate, out retDate);
                retDate = TimeZoneInfo.ConvertTimeToUtc(new DateTime(retDate.Year,retDate.Month, retDate.Day, retDate.Hour, retDate.Minute, retDate.Second), TimeZoneInfo.FindSystemTimeZoneById(timeZoneId));
            }
            else
            {
                retDate = TimeZoneInfo.ConvertTimeToUtc(retDate, TimeZoneInfo.FindSystemTimeZoneById(timeZoneId));
            }
            return retDate.TimeOfDay;
           
        }
    }
}
