using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PunchClock.Domain.Model
{
    public class EmployeePaidHoliday
    {
        [Key, Column(Order = 0)]
        public int CompanyId { get; set; }
        [Key, Column(Order = 1)]
        public int EmploymentTypeId { get; set; }
        [Key, Column(Order = 2)]
        public bool IsHolidayPaid { get; set; }
    }
}
