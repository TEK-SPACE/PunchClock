using System.ComponentModel.DataAnnotations;

namespace PunchClock.Domain.Model
{
    public class CompanyHoliday
    {
        [Key]
        public int CompanyId { get; set; }
        [Key]
        public int HolidayId { get; set; }
    }
}
