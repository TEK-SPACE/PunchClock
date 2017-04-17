﻿using System;
using System.ComponentModel.DataAnnotations;

namespace PunchClock.View.Model
{
   public class PunchView
    {
        public int PunchId { get; set; }

        [UIHint("TimeSpan")]
        public TimeSpan PunchIn { get; set; }
        [UIHint("TimeSpan")]
        public TimeSpan? PunchOut { get; set; }
        [UIHint("DateTime")]
        public DateTime PunchDate { get; set; }
        public int? Hours { get; set; }
        public int? ApprovedHours { get; set; }
        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }
        public int UserId { get; set; }
        public bool IsManagerAccepted { get; set; }
        public bool RequestForApproval { get; set; }
        public string Comments { get; set; }
        public UserView User { get; set; }
        public TimeSpan Duration { get; set; }
        public string IpAddress { get; set; }
        public string MacAddress { get; set; }
        public string UserGuid { set; get; }
    }
}
