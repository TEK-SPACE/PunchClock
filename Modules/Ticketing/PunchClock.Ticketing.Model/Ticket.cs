using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using PunchClock.Domain.Model;

namespace PunchClock.Ticketing.Model
{
    public class Ticket :CommonEntity
    {
        [Key]
        [Display(Name = "Ticket Id")]
        public int Id { get; set; }

        [StringLength(250)]
        [Display(Name = "Title")]
        [Required]
        public string Title { get; set; }

        [StringLength(5000)]
        [Required]
        [AllowHtml]
        public string Description { get; set; }
        [Display( Name = "Status")]
        [UIHint("TicketStatus")]
        [Required]
        public int StatusId { get; set; }
        [Display(Name = "Type")]
        [Required]
        [UIHint("TicketTypes")]
        public int TypeId { get; set; }
        [Display(Name = "Priority")]
        [UIHint("TicketPriorities")]
        [Required]
        public int PriorityId { get; set; }
        [Display(Name = "Category")]
        [UIHint("TicketCategories")]
        public int CategoryId { get; set; }
        [Display(Name = "Project")]
        [UIHint("TicketProjects")]
        [Required]
        public int ProjectId { get; set; }
        [Display(Name = "Requestor")]
        [UIHint("UsersDropdownList")]
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
        public virtual ICollection<TicketComment> Comments { get; set; } = new List<TicketComment>();
    }
}