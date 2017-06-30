using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PunchClock.Domain.Model;

namespace PunchClock.Ticketing.Model
{
   public class TicketCategory: CommonEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsCoreItem { get; set; } = false;
        [UIHint("TicketProjects")]
        public int ProjectId { get; set; } = 1;

        [ForeignKey("ProjectId")]
        public virtual TicketProject TicketProject { get; set; }
    }
}