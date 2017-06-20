using PunchClock.Core.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PunchClock.Ticketing.Model
{
    public class TicketAttachment: CommonEntity
    {
        public int Id { get; set; } = 0;
        public string Attachement { get; set; }
    }
}