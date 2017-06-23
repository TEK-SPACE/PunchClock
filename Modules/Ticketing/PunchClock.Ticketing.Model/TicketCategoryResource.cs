using System.ComponentModel.DataAnnotations.Schema;
using PunchClock.Language.Model;

namespace PunchClock.Ticketing.Model
{
  public class TicketCategoryResource : BaseResource
    {
        [Index("UniqueResourceTicketingTypeCulture", 1)]
        public int CategoryMasterId { get; set; }
        [Index("UniqueResourceTicketingTypeCulture", 2)]
        public Culture Culture { get; set; }
        [ForeignKey("CategoryMasterId")]
        public TicketCategory TicketCategory { get; set; }
    }

}
