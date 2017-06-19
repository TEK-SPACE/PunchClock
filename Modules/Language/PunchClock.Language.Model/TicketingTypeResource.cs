using System.ComponentModel.DataAnnotations.Schema;

namespace PunchClock.Language.Model
{
    public class TicketingTypeResource : BaseResource
    {
        [Index("UniqueResourceTicketingTypeCulture", 1)]
        public int TypeMasterId { get; set; }
        [Index("UniqueResourceTicketingTypeCulture", 2)]
        public Culture Culture { get; set; }
    }
}
