using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PunchClock.Ticketing.Model;
using PunchClock.Domain.Model;

namespace PunchClock.Ticketing.Contracts
{
    public interface ITicketComment
    {
        TicketComment Add(TicketComment comment);
        TicketComment Update(TicketComment comment);
        AjaxResponse Delete(int id);
    }
}
