using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PunchClock.Domain.Model
{
    public class HolidayTypeHoliday
    {
        [Key, Column(Order = 0)]
        public int HolidayId { get; set; }
        [Key, Column(Order = 1)]
        public int TypeId { get; set; }
        public virtual Holiday Holiday { get; set; }
        public virtual HolidayType HolidayType { get; set; }
    }
}
