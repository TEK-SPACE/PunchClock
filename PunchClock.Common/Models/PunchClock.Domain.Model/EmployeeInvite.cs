using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PunchClock.Domain.Model
{
   public  class EmployeeInvite
    {
        [Key]
        public int Id { get; set; }

        public string GlobalId { get; set; } = Guid.NewGuid().ToString("D");
        public string Name { get; set; }
        [EmailAddress]//[Index(IsUnique = true)]
        public string Email { get; set; }
        public string InvitedBy { get; set; }
        public int CompanyId { get; set; }
        public int UserTypeId { get; set; }
        [ForeignKey("UserTypeId")]
        public virtual UserType UserType { get; set; }
        [ForeignKey("CompanyId")]
        [NotMapped][ScaffoldColumn(false)]
        public string LinkToRegister { get; set; }
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime InvitationDateUtc { get; set; }

        public bool IsRegistered { get; set; } = false;
        public bool InviteRevoked { get; set; }
    }
}
