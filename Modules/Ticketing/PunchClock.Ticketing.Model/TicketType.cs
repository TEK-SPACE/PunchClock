using PunchClock.Core.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace PunchClock.Ticketing.Model
{
    public class TicketType: CommonEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
    }
}
