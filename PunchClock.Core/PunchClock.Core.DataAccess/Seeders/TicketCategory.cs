using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PunchClock.Ticketing.Model;

namespace PunchClock.Core.DataAccess.Seeders
{
    public partial class PunchDbInitializer
    {
        private void SeedTicketCategory(PunchClockDbContext context)
        {   
            var ticketcat = new List<TicketCategory>
            {
                new TicketCategory {Id = 1, Name = "American Greetings", Description ="", DisplayOrder = 1},
                new TicketCategory {Id = 1, Name = "GerMedUSA", Description ="", DisplayOrder = 1},
                new TicketCategory {Id = 1, Name = "OpenAir", Description ="", DisplayOrder = 1},
                new TicketCategory {Id = 1, Name = "Ametek", Description ="", DisplayOrder = 1},
                new TicketCategory {Id = 1, Name = "ACCO Brands", Description ="", DisplayOrder = 1},
                new TicketCategory {Id = 1, Name = "Cape Air", Description ="", DisplayOrder = 1},


            };
        }
    }
}