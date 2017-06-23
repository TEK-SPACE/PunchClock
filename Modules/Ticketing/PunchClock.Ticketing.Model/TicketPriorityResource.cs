using System.ComponentModel.DataAnnotations.Schema;
using PunchClock.Language.Model;

namespace PunchClock.Ticketing.Model
{
   public class TicketPriorityResource : BaseResource
    {
        [Index("UniqueResourceTicketingTypeCulture", 1)]
        public int PriorityMasterId { get; set; }
        [Index("UniqueResourceTicketingTypeCulture", 2)]
        public Culture Culture { get; set; }
        [ForeignKey("PriorityMasterId")]
        public TicketPriority TicketPriority { get; set; }
    }

}
