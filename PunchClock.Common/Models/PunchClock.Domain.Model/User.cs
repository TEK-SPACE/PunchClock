using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PunchClock.Domain.Model
{
    public class User : IdentityUser
    {
        public User() : base()
        {
        }
        public User(string userName) : base(userName)
        {
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        //[ScaffoldColumn(false)]
        //public new string Id { get; set; }

        //[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public int Uid { get; set; }

        [Display(Name = "Account Type")]
        public int UserTypeId { get; set; }

        [Required]
        public int EmploymentTypeId { get; set; }
        public int CompanyId { get; set; }

        [Display(Name = "First Name"), Required(ErrorMessage = "{0} is Required"),
         RegularExpression("^[a-zA-Z-' ]+$", ErrorMessage = "Invalid {0}"),
         StringLength(150, MinimumLength = 2, ErrorMessage = "Min {2}, Max {1} chars")]

        public string FirstName { get; set; }

        [Display(Name = "Middle Initial"),
         RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Invalid {0}"), StringLength(1, ErrorMessage = "Max {1} char")]

        public string MiddleName { get; set; }

        [Display(Name = "Last Name"), Required(ErrorMessage = "{0} is Required"),
         RegularExpression("^[a-zA-Z-' ]+$", ErrorMessage = "Invalid {0}"),
         StringLength(150, MinimumLength = 2, ErrorMessage = "Min {2}, Max {1} chars")]

        public string LastName { get; set; }
        [NotMapped]
        public string DisplayName => new Regex("[ ]{2,}", RegexOptions.None).Replace($"{FirstName} {MiddleName} {LastName}"," ");

        [Display(Name = "Email")]
        [Required]
        public override string Email { get; set; }

        [Display(Name = "Mobile"),
         RegularExpression(@"^(\(?\d\d\d\)?)?( |-|\.)?\d\d\d( |-|\.)?\d{4,4}(( |-|\.)?[ext\.]+ ?\d+)?$",
             ErrorMessage = "Invalid {0}")]

        public new string PhoneNumber { get; set; }

        [Display(Name = "User Name")]
        [Required]
        public override string UserName { get; set; }

        [NotMapped]
        public string PasswordSalt { get; set; }

        [Column(TypeName = "datetime2")]
        [ScaffoldColumn(false)]
        public DateTime PasswordLastChanged { get; set; }

        [ScaffoldColumn(false)]
        public bool PasswordDisabled { get; set; }

        [Display(Name = "Timezone")]
        [Required(ErrorMessage = "Please select your timezone")]
        public string RegisteredTimeZone { get; set; }

        [ScaffoldColumn(false)]
        public bool IsActive { get; set; }

        [ScaffoldColumn(false)]
        public bool IsDeleted { get; set; }

        [ScaffoldColumn(false)]
        public int? IsAdmin { get; set; }

        [ScaffoldColumn(false)]
        public DateTimeOffset DateCreatedUtc { get; set; }

        [ScaffoldColumn(false)]
        public DateTimeOffset LastUpdatedUtc { get; set; }

        [ScaffoldColumn(false)]
        public DateTimeOffset LastActivityDateUtc { get; set; }

        [ScaffoldColumn(false)]
        public string UserRegisteredIp { get; set; }

        [ScaffoldColumn(false)]
        public string RegisteredMacAddress { get; set; }

        [ScaffoldColumn(false)]
        public string LastActiveMacAddress { get; set; }

        [ScaffoldColumn(false)]
        public string LastActivityIp { get; set; }

        [Display(Name = "Registration Code"), Required(ErrorMessage = "{0} is Required")]
        public string RegistrationCode { get; set; }

        [Display(Name = "Password")]
        //[Required]
        [NotMapped] [RegularExpression(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$", ErrorMessage = "{0} doesn't meet the requirement")]
        public string Password { get; set; }

        public string PasswordResetCode { get; set; }
        public DateTime? PasswordResetValidityTill { get; set; }

        public ICollection<Address> Addresses { get; set; }

        [ForeignKey("EmploymentTypeId")]
        public virtual EmploymentType EmploymentType { get; set; }
        [ForeignKey("UserTypeId")]
        public virtual UserType UserType { get; set; }
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> TimezonesList { get; set; }

        //public virtual ICollection<Punch> Punches { get; set; }
    }
}