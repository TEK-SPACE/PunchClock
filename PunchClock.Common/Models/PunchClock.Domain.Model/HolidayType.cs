using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PunchClock.Domain.Model
{
    public class HolidayType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateEnteredUtc { get; set; }

        public virtual ICollection<Holiday> Holidays { get; set; }
        public virtual ICollection<HolidayTypeHoliday> HolidayTypeHolidays { get; set; }
    }
}
