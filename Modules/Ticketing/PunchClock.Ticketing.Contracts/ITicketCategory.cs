using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PunchClock.Core.Models.Common;
using PunchClock.Ticketing.Model;

namespace PunchClock.Ticketing.Contracts
{
    public interface ITicketCategory
    {
        TicketCategory Add(TicketCategory category);
        TicketCategory Update(TicketCategory category);
        AjaxResponse Delete(int id);
             
    }
}
