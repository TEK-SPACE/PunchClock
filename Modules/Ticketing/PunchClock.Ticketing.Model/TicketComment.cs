using PunchClock.Core.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PunchClock.Ticketing.Model
{
    public class TicketComment: CommonEntity
    {
        [Key]
        public int Id { get; set; } 
        public string Description { get; set; }
        public bool IsPrivate { get; set; }
        public int TicketId { get; set; }

        [ForeignKey("TicketId")]
        public virtual Ticket Ticket { get; set; }
    }
}
