using System.ComponentModel.DataAnnotations;

namespace PunchClock.Ticketing.Model
{
    public class TicketType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
