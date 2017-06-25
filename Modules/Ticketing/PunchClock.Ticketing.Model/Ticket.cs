using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PunchClock.Domain.Model;

namespace PunchClock.Ticketing.Model
{
    public class Ticket :CommonEntity
    {
        [Key]
        public int Id { get; set; }

        [StringLength(250)]
        [Display(Name = "Title")]
        [Required]
        public string Title { get; set; }

        [StringLength(5000)]
        [Required]
        public string Description { get; set; }
        [Display( Name = "Status")]
        [Required]
        public int StatusId { get; set; }
        [Display(Name = "Type")]
        [Required]
        public int TypeId { get; set; }
        [Display(Name = "Priority")]
        [Required]
        public int PriorityId { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [Display(Name = "Project")]
        [Required]
        public int ProjectId { get; set; }
        [Display(Name = "Requestor")]
        [Required]
        public string RequestorId { get; set; }
        [ForeignKey("RequestorId")]
        public virtual  User  Requestor{ get; set; }
        [Display(Name = "Assigned To")]
        [Required]
        [UIHint("UsersDropdownList")]
        public string AssignedToId { get; set; }

        [ForeignKey("AssignedToId")]
        public virtual User AssignedTo { get; set; }

        [Display(Name = "Notify To")]
        public string NotifyTo { get; set; }
        [Column(TypeName = "datetime2")]
        [Display(Name = "Due Date")]
        public DateTime? DueDateUtc { get; set; }
        //public List<TicketNotificationsTo> NotificationsTo { get; set; }

        [ForeignKey("ProjectId")]
        public virtual TicketProject TicketProject { get; set; }

        [ForeignKey("TypeId")]
        public virtual TicketType Type { get; set; }
        [ForeignKey("PriorityId")]
        public virtual TicketPriority Priority { get; set; }
        [ForeignKey("CategoryId")]
        public virtual TicketCategory Category { get; set; }
        [ForeignKey("StatusId")]
        public virtual TicketStatus Status { get; set; }
        [NotMapped]
        public virtual List<TicketComment> Comments { get; set; }
    }
}