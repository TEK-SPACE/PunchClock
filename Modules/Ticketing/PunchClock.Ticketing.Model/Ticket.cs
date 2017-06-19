using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PunchClock.Ticketing.Model
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        [StringLength(250)]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [StringLength(5000)]
        public string Description { get; set; }
        public int StatusId { get; set; }
        [ForeignKey("StatusId")]
        public virtual TicketStatus Status { get; set; }

        public virtual List<TicketComment> Comments { get; set; }
    }
}
