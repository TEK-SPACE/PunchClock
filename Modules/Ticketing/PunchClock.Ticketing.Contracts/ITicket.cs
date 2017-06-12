using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PunchClock.Ticketing.Model;

namespace PunchClock.Ticketing.Contracts
{
    public interface ITicket
    {
        int Add(Ticket ticket);
        bool Update(Ticket ticket);
    }
}
