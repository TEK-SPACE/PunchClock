using NUnit.Framework;
using PunchClock.Ticketing.Contracts;
using PunchClock.Ticketing.Model;
using PunchClock.Ticketing.Services;

namespace PunchClock.Ticketing.Testing
{
    [TestFixture]
    public class TicketTest
    {
        private ITicket _ticketService;

        [SetUp]
        public void Init()
        {
            _ticketService = new TicketService();
        }

        [Test]
        public void AddTicket()
        {
            var ticket = new Ticket
            {
                Title = "Test Ticket",
                Description = "Test Description",
                StatusId = 1
            };
            _ticketService.Add(ticket);
        }
    }
}
