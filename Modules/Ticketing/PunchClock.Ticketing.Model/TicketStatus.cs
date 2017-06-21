using System.ComponentModel.DataAnnotations;
using PunchClock.Core.Models.Common;

namespace PunchClock.Ticketing.Model
{
      
    public class TicketStatus : CommonEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
    }
}
