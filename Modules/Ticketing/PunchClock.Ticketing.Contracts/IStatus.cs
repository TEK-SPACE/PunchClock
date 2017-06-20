using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PunchClock.Core.Models.Common;
using PunchClock.Ticketing.Model;

namespace PunchClock.Ticketing.Contracts
{
    public interface IStatus
    {
        TicketStatus Add(TicketStatus status);
        TicketStatus Update(TicketStatus status);
        AjaxResponse Delete(int id);
    }
}
