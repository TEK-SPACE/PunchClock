using System;
using System.ComponentModel.DataAnnotations;

namespace PunchClock.View.Model
{
    public class Holiday
    {
        [UIHint("TimeSpan")]
        public TimeSpan PunchIn { get; set; }
        [UIHint("TimeSpan")]
        public TimeSpan? PunchOut { get; set; }
        public int? Hours { get; set; }

        public string HolidayName { get; set; }
        public DateTime? HolidayDate { get; set; }
    }
}
