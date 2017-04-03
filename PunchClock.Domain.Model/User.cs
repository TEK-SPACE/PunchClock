using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace PunchClock.Domain.Model
{
    public class User : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        //[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Uid { get; set; }
        public Guid GlobalId { get; set; }
        public int UserTypeId { get; set; }
        public int EmploymentTypeId { get; set; }
        public int CompanyId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        [NotMapped]
        public string PasswordSalt { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime PasswordLastChanged { get; set; }
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
        //[Column(TypeName = "datetime2")]
        //public string LockoutEndDateUtc { get; set; } 

        public virtual EmploymentType EmploymentType { get; set; }
        public virtual UserType UserType { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<Punch> Punches { get; set; }
    }
}
