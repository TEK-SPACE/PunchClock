using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PunchClock.Ticketing.Contracts;
using PunchClock.Ticketing.Model;
using PunchClock.Ticketing.Services;

namespace PunchClock.UI.Web.Controllers
{
    [Authorize]
    public class TicketController : Controller
    {
        private readonly ITicket _ticketService;
        public TicketController()
        {
            _ticketService = new TicketService();
        }
        // GET: Ticket
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                _ticketService.Add(ticket);
            }
            return View(ticket);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            var tickets = _ticketService.All();
            return Json(tickets.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request,
            Ticket ticket)
        {
            if (ticket != null && ModelState.IsValid)
            {
                _ticketService.Update(ticket);
            }
            return Json(new[] { ticket }.ToDataSourceResult(request, ModelState));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete([DataSourceRequest] DataSourceRequest request,
            Ticket ticket)
        {
            if (ticket != null && ModelState.IsValid)
            {
                _ticketService.Delete(ticket);
            }
            return Json(new[] { ticket }.ToDataSourceResult(request, ModelState));
        }

        [HttpGet]
        public ActionResult Status()
        {
            return Json(_ticketService.GetStatus(), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Type()
        {
            return Json(_ticketService.GetStatus(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Priority()
        {
            return Json(_ticketService.GetStatus(), JsonRequestBehavior.AllowGet);
        }
    }
}