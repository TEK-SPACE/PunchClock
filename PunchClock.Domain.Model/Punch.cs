using System;
using System.ComponentModel.DataAnnotations;

namespace PunchClock.Domain.Model
{
    public class Punch
    {
        [Key]
        public int Id { get; set; }
        public DateTime PunchDate { get; set; }
        public TimeSpan PunchIn { get; set; }
        public TimeSpan? PunchOut { get; set; }
        public int UserId { get; set; }
        public bool ManagerAccepted { get; set; }
        public bool RequestForApproval { get; set; }
        public string Comments { get; set; }
        public virtual User User { get; set; }
    }
}
