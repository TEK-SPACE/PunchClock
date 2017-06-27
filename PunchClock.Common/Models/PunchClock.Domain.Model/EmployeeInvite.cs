using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PunchClock.Domain.Model
{
   public  class EmployeeInvite
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [EmailAddress]//[Index(IsUnique = true)]
        public string Email { get; set; }
        public int CompanyId { get; set; }
        [NotMapped]
        public string LinkToRegister { get; set; }
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime InvitationDateUtc { get; set; }

        public bool IsRegistered { get; set; } = false;
        public bool InviteRevoked { get; set; }
    }
}
