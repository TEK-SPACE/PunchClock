using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PunchClock.Domain.Model;

namespace PunchClock.Ticketing.Model
{
    [Table("TicketStatus", Schema = "dbo")]
    public class Status
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string LastModifiedByGuid { get; set; }
        [ForeignKey("LastModifiedByGuid")]
        public virtual User User { get; set; }
    }
}
