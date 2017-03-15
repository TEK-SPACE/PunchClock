using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PunchClock.Domain.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public Guid GlobalId { get; set; }
        public int UserTypeId { get; set; }
        public int EmploymentTypeId { get; set; }
        public int CompanyId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public DateTime? PasswordLastChanged { get; set; }
        public bool PasswordDisabled { get; set; }
        public string RegisteredTimeZone { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int? IsAdmin { get; set; }
        public DateTimeOffset DateCreatedUtc { get; set; }
        public DateTimeOffset LastUpdatedUtc { get; set; }
        public DateTimeOffset LastActivityDateUtc { get; set; }
        public string UserRegisteredIp { get; set; }
        public string RegisteredMacAddress { get; set; }
        public string LastActiveMacAddress { get; set; }
        public string LastActivityIp { get; set; }

        public virtual EmploymentType EmploymentType { get; set; }
        public virtual UserType UserType { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<Punch> Punches { get; set; }
    }
}
