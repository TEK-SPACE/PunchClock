using System.ComponentModel.DataAnnotations.Schema;
using PunchClock.Language.Model;

namespace PunchClock.Ticketing.Model
{
   public class TicketStatusResource : BaseResource
    {
        [Index("UniqueResourceTicketingTypeCulture", 1)]
        public int StatusMasterId { get; set; }
        [Index("UniqueResourceTicketingTypeCulture", 2)]
        public Culture Culture { get; set; }
        [ForeignKey("StatusMasterId")]
        public TicketStatus TicketStatus { get; set; }
    }

}
