using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PunchClock.View.Model
{
    public class User
    {
        public Punch LastPunch { get; set; } = new Punch();

        [ScaffoldColumn(false)]
        public int UserId { get; set; }

        public int EmploymentTypeId { get; set; }
        public int CompanyId { get; set; }

        [ScaffoldColumn(false)]
        public Guid GlobalId { get; set; }
        [Display(Name = "First Name"), Required(ErrorMessage = "{0} is Required"),
       RegularExpression("^[a-zA-Z-' ]+$", ErrorMessage = "Invalid {0}"), StringLength(150, MinimumLength = 2, ErrorMessage = "Min {2}, Max {1} chars")]

        public string FirstName { get; set; }

        [Display(Name = "Last Name"), Required(ErrorMessage = "{0} is Required"),
      RegularExpression("^[a-zA-Z-' ]+$", ErrorMessage = "Invalid {0}"), StringLength(150, MinimumLength = 2, ErrorMessage = "Min {2}, Max {1} chars")]

        public string LastName { get; set; }
        [Display(Name = "Email")]
        [Required]
        public string Email { get; set; }
        [Display(Name = "Mobile"),
       RegularExpression(@"^(\(?\d\d\d\)?)?( |-|\.)?\d\d\d( |-|\.)?\d{4,4}(( |-|\.)?[ext\.]+ ?\d+)?$", ErrorMessage = "Invalid {0}")]

        public string Telephone { get; set; }

        [Display(Name = "Middle Initial"),
       RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Invalid {0}"), StringLength(1, ErrorMessage = "Max {1} char")]

        public string MiddleName { get; set; }
        [Display(Name = "User Name")]
        [Required]
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }

        [ScaffoldColumn(false)]
        public DateTime PasswordLastChanged { get; set; }
        [ScaffoldColumn(false)]
        public bool PasswordDisabled { get; set; }
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
        public string LastActivityIp { get; set; }
        [ScaffoldColumn(false)]
        public string RegisteredMacAddress { get; set; }
        [ScaffoldColumn(false)]
        public string LastActiveMacAddress { get; set; }

        [Display(Name = "Account Type")]
        public int UserTypeId { get; set; }

        [Display(Name = "Password")]
        [Required]
        public string Password { get; set; }

        [Display(Name = "Registration Code"), Required]
        public string RegistrationCode { get; set; }

        [Display(Name = "Timezone")]
        [Required(ErrorMessage = "Please select your timezone")]
        public string RegisteredTimeZone { get; set; }
        public IEnumerable<SelectListItem> TimezonesList { get; set; }

        public Company Company { get; set; }
        public ICollection<Punch> Punch { get; set; }
        public PunchClock.Objects.Core.Enum.UserType UserType { get; set; }
    }
}
