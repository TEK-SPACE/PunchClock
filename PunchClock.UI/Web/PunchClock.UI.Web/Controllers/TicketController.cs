using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PunchClock.Core.Contracts;
using PunchClock.Core.Implementation;
using PunchClock.Ticketing.Contracts;
using PunchClock.Ticketing.Model;
using PunchClock.Ticketing.Services;

namespace PunchClock.UI.Web.Controllers
{
    [Authorize]
    public class TicketController : BaseController
    {
        private readonly ITicket _ticketService;
        private readonly IUserRepository _userRepository;

        public TicketController()
        {
            _ticketService = new TicketService();
            _userRepository = new UserService();
        }
        // GET: Ticket
        public ActionResult Index()
        {
            ViewData["Users"] = _userRepository.All(OperatingUser.CompanyId);
            ViewData["TicketCategories"] = _ticketService.GetCategories(OperatingUser.CompanyId);
            ViewData["TicketPriorities"] = _ticketService.GetPriorties(OperatingUser.CompanyId);
            ViewData["TicketProjects"] = _ticketService.GetProjects(OperatingUser.CompanyId);
            ViewData["TicketTypes"] = _ticketService.GetTypes(OperatingUser.CompanyId);
            ViewData["TicketStatusus"] = _ticketService.GetStatusus(OperatingUser.CompanyId);
            return View(new List<Ticket>());
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
                ticket.CompanyId = OperatingUser.CompanyId;
                ticket.CreatedById = OperatingUser.Id;
                ticket.ModifiedById = OperatingUser.Id;
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
                ticket.ModifiedById = OperatingUser.Id;
                ticket.ModifiedDateUtc = DateTime.UtcNow;
                _ticketService.Update(ticket);
            }
            return Json(new[] {ticket}.ToDataSourceResult(request, ModelState));
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
            return Json(_ticketService.GetStatusus(companyId: OperatingUser.CompanyId), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Categories()
        {
            return Json(_ticketService.GetCategories(companyId: OperatingUser.CompanyId), JsonRequestBehavior.AllowGet);
        }
             

        [HttpGet]
        public ActionResult Priorities()
        {
            return Json(_ticketService.GetPriorties(companyId: OperatingUser.CompanyId), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Types()
        {
            return Json(_ticketService.GetTypes(companyId: OperatingUser.CompanyId), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Projects()
        {
            return Json(_ticketService.GetProjects(companyId: OperatingUser.CompanyId), JsonRequestBehavior.AllowGet);
        }
    }
}