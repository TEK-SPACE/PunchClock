using System.ComponentModel.DataAnnotations;

namespace PunchClock.Domain.Model
{
    public class PaidHoliday
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
