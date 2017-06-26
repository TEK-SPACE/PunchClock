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
        [NotMapped]
        public string Month {
            get
            {
                var In = TimeZoneInfo.ConvertTimeFromUtc(PunchDate.Date + PunchIn,
                    TimeZoneInfo.FindSystemTimeZoneById(User.RegisteredTimeZone));
                return In.ToString("MMMM");
            }
        }
        public TimeSpan? PunchOut { get; set; }
        public int UserId { get; set; }
        public string UserGuid { get; set; }
        public bool Approved { get; set; }
        public bool ApprovalRequired { get; set; }
        public string Comments { get; set; }
        public string IpAddress { get; set; }
        public string MacAddress { get; set; }

        [NotMapped]
        public string EmployeeName => User != null ? $"{User.FirstName} {User.LastName}" : string.Empty;

        public int Hours
        {
            get { return PunchOut.HasValue ? Convert.ToInt32(PunchOut.Value.Subtract(PunchIn).TotalHours) : 0; }
            set { }
        }

        [NotMapped]
        public TimeSpan Duration
        {
            get
            {
                var Out = PunchOut.HasValue
                    ? TimeZoneInfo.ConvertTimeFromUtc(PunchDate.Date + PunchOut.Value,
                        TimeZoneInfo.FindSystemTimeZoneById(User.RegisteredTimeZone))
                    : DateTime.MinValue;
                var In = TimeZoneInfo.ConvertTimeFromUtc(PunchDate.Date + PunchIn,
                    TimeZoneInfo.FindSystemTimeZoneById(User.RegisteredTimeZone));

                if (PunchOut.HasValue)
                    return new TimeSpan(Out.TimeOfDay.Hours, Out.TimeOfDay.Minutes, Out.TimeOfDay.Seconds) -
                           new TimeSpan(In.TimeOfDay.Hours, In.TimeOfDay.Minutes, In.TimeOfDay.Seconds);
                return DateTime.MinValue.TimeOfDay;
            }
            set { }
        }

        public int ApprovedHours
        {
            get
            {
                if (PunchOut != null)
                    return ApprovalRequired
                        ? (Approved ? PunchOut.Value.Subtract(PunchIn).Seconds : 0)
                        : PunchOut.Value.Subtract(PunchIn).Seconds;
                return 0;
            }
            set { }
        }

        [ForeignKey("UserGuid")]
        public virtual User User { get; set; }
    }
}
