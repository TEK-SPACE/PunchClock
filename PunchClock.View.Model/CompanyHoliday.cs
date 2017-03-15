using System;

namespace PunchClock.View.Model
{
    public class CompanyHoliday
    {
        public int CompanyId { get; set; }
        public int HolidayId { get; set; }

        public string HolidayType { get; set; }
        public string HolidayName { get; set; }

        public DateTime? HolidayDate { get; set; }
    }
}
