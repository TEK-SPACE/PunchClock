using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Configuration;
using PunchClock.Domain.Model;

namespace PunchClock.TimeTracker.Model
{
    public class Punch
    {
        [Key]
        public int Id { get; set; }

        public DateTime PunchDate { get; set; }
        public TimeSpan PunchIn { get; set; }
        public TimeSpan? PunchOut { get; set; }
        public int UserId { get; set; }
        public string UserGuid { get; set; }
        public bool ManagerAccepted { get; set; }
        public bool RequestForApproval { get; set; }
        public string Comments { get; set; }
        public string IpAddress { get; set; }
        public string MacAddress { get; set; }

        [NotMapped]
        public string EmployeeName => User != null ? $"{User.FirstName} {User.LastName}" : string.Empty;

        public int Hours
        {
            get { return PunchOut.HasValue ? Convert.ToInt32(PunchOut.Value.Subtract(PunchIn).TotalSeconds) : 0; }
            set { }
        }
        [NotMapped]
        public TimeSpan Duration { get; set; }

        public int ApprovedHours
        {
            get
            {
                if (PunchOut != null)
                    return RequestForApproval
                        ? (ManagerAccepted ? PunchOut.Value.Subtract(PunchIn).Seconds : 0)
                        : PunchOut.Value.Subtract(PunchIn).Seconds;
                return 0;
            }
            set { }
        }

        [ForeignKey("UserGuid")]
        public virtual User User { get; set; }
    }
}
