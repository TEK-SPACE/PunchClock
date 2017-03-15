using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PunchClock.Domain.Model
{
    public class Holiday
    {
        private DateTime _holidayDate;

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int HolidayMonth { get; set; }
        public int HolidayDay { get; set; }
        [NotMapped]
        public DateTime HolidayDate {
            get
            {
                return _holidayDate;
            }
            set
            {
                _holidayDate = value;
                if ((HolidayMonth > 0) && (HolidayDay > 0))
                {
                    _holidayDate = new DateTime(DateTime.Now.Year, HolidayMonth, HolidayDay);
                }
            }
        }
        public int HolidayTypeId { get; set; }
        public int CountryId { get; set; }

        public virtual Country Country { get; set; }
        public virtual HolidayType HolidayType { get; set; }
        public virtual ICollection<HolidayTypeHoliday> HolidayTypeHolidays { get; set; }
    }
}
