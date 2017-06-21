using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PunchClock.Core.Models.Common;
using System.ComponentModel.DataAnnotations.Schema;

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
