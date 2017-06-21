using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PunchClock.Ticketing.Model;

namespace PunchClock.Core.DataAccess.Seeders
{
    public partial class PunchDbInitializer
    {
        private void SeedTicketType(PunchClockDbContext context)
        {
            var tickettype = new List<TicketType>
            {
                //Question is used to indicate that the requester's issue is a question rather than a problem that needs to be solved.
                new TicketType {Id = 1, Name = "Question",Description ="",DisplayOrder =1},

                //Enhancement: a new feature or an improvement over an existing one
                new TicketType {Id = 1, Name = "Incident",Description ="",DisplayOrder =1},

                //Task: everything else, something that needs to be done
                new TicketType {Id = 1, Name = "Task",Description ="",DisplayOrder =1},

                //Problem is used to indicate that the requester is having an issue with your product or service that needs to be resolved
                new TicketType {Id=1,Name = "Problem",Description = "",DisplayOrder =1}
            };
            foreach (var Ttype in tickettype)
            {
                context.TicketTypes.AddOrUpdate(Ttype);
            }
        }
    }
}
