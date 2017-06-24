using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PunchClock.Core.Models.Common;
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
        public string Title { get; set; }

        [StringLength(5000)]
        public string Description { get; set; }
        [Display( Name = "Status")]
        public int StatusId { get; set; }
        [Display(Name = "Type")]
        public int TypeId { get; set; }
        [Display(Name = "Priority")]
        public int PriorityId { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [Display(Name = "Project")]
        public int ProjectId { get; set; }
        [Display(Name = "Requestor")]
        public string RequestorId { get; set; }
        [ForeignKey("RequestorId")]
        public virtual  User  Requestor{ get; set; }
        [Display(Name = "Assigned To")]
        public string AssignedToId { get; set; }
        [ForeignKey("AssignedToId")]
        public virtual User AssignedTo { get; set; }

        [Display(Name = "Notify To")]
        public string NotifyTo { get; set; }

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