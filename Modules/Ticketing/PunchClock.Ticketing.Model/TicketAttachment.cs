using System;
using System.ComponentModel.DataAnnotations.Schema;
using PunchClock.Core.Models.Common;

namespace PunchClock.Ticketing.Model
{
    public class TicketAttachment: CommonEntity
    {
        public int Id { get; set; } = 0;
        public string FileName { get; set; }
        public Byte[] Data { get; set; }
        public string Description { get; set; }
        public bool IsPrivate { get; set; }
        public int TicketId { get; set; }

        [ForeignKey("TicketId")]
        public virtual Ticket Ticket { get; set; }
    }
}