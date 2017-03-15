using System.ComponentModel.DataAnnotations;

namespace PunchClock.Domain.Model
{
    public class HolidayTypeHoliday
    {
        [Key]
        public int HolidayId { get; set; }
        [Key]
        public int TypeId { get; set; }
        public virtual Holiday Holiday { get; set; }
        public virtual HolidayType HolidayType { get; set; }
    }
}
