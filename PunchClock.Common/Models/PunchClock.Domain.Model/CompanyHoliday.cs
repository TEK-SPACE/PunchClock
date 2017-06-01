using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PunchClock.Domain.Model
{
    public class CompanyHoliday
    {
        [Key, Column(Order = 0)]
        public int CompanyId { get; set; }
        [Key, Column(Order = 1)]
        public int HolidayId { get; set; }
    }
}
