using System.ComponentModel.DataAnnotations.Schema;
using PunchClock.Language.Model;

namespace PunchClock.Ticketing.Model
{
    public class TicketTypeResource : BaseResource
    {
        [Index("UniqueResourceTicketingTypeCulture", 1)]
        public int TypeMasterId { get; set; }
        [Index("UniqueResourceTicketingTypeCulture", 2)]
        public Culture Culture { get; set; }

        [ForeignKey("TypeMasterId")]
        public TicketType TicketType { get; set; }
    }
}
