using System.ComponentModel.DataAnnotations;

namespace PunchClock.Domain.Model
{
    public class EmployeePaidHoliday
    {
        [Key]
        public int CompanyId { get; set; }
        [Key]
        public int EmploymentTypeId { get; set; }
        [Key]
        public bool IsHolidayPaid { get; set; }
    }
}
