using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PunchClock.Cms.Contract;
using PunchClock.Cms.Service;
using PunchClock.Core.Contracts;
using PunchClock.Core.Implementation;
using PunchClock.Domain.Model;
using PunchClock.Ticketing.Contracts;
using PunchClock.Ticketing.Model;
using PunchClock.Ticketing.Services;
using UserType = PunchClock.Domain.Model.Enum.UserType;

namespace PunchClock.UI.Web.Controllers
{
    [Authorize]
    public class TicketController : BaseController
    {
        private readonly ITicket _ticketService;
        private readonly IUser _userService;
        private readonly ICategoryService _categoryService;
        private readonly ITicketCategory _ticketCategoryService;
        private readonly ITicketPriority _ticketPriority;
        private readonly ITicketStatus _ticketStatus;
        private readonly ITicketType _ticketType;
        private readonly IEmail _emailService;
        private readonly ITicketProject _ticketProject;
        public TicketController()
        {
            _categoryService = new CategoryService();
            _ticketService = new TicketService();
            _userService = new UserService();
            _ticketCategoryService = new TicketCategoryService();
            _ticketPriority = new TicketPriorityService();
            _ticketStatus = new TicketStatusService();
            _ticketType = new TicketTypeService();
            _emailService = new Core.Implementation.EmailService();
            _ticketProject=new TicketProjectService();
        }

        // GET: Ticket
        public ActionResult List()
        {
            ViewData["Users"] = _userService.All(OperatingUser.CompanyId);
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
               
                var userIds = ticket.NotifyTo?.Split(',').ToList() ?? new List<string>();
                userIds.Add(ticket.RequestorId);
                userIds.Add(ticket.AssignedToId);
                userIds.Add(ticket.CreatedById);
                List<string> contributors = _userService.GetEmailsById(userIds.ToArray());

                ticket.LinkToTicketDetails =
                    $"{Request.Url.Scheme}://{Request.Url.Host}{Url.Action("Edit", "Ticket", new {id = ticket.Id})}";
                string emailMessage = _ticketService.ComposeTicketCreatedEmail(ticket);
                _emailService.SendEmail(emailMessage, $"New Ticket: {ticket.Title}", contributors.Distinct().ToArray());
                if (ticket.Id > 0)
                    return RedirectToAction("Edit", "Ticket", new {id = ticket.Id});
            }
            else
            {
                ReadModelStateError(ModelState);
            }
            return View(ticket);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var ticket = _ticketService.Details(id);
            return View(ticket);
        }

        [HttpPost]
        public ActionResult Edit(Ticket ticket, FormCollection formCollection)
        {
            if (!string.IsNullOrEmpty(formCollection["Comment"]))
            {
                ticket.Comments.Add(new TicketComment
                {
                    Description = formCollection["Comment"],
                    CreatedById = OperatingUser.Id,
                    CreatedDateUtc = DateTime.UtcNow,
                    ModifiedById = OperatingUser.Id,
                    ModifiedDateUtc = DateTime.UtcNow,
                    CompanyId = OperatingUser.CompanyId,
                    TicketId = ticket.Id
                });
            }
            var changes = new List<ChangeLog>();
            _ticketService.Update(ticket, ref changes);
            ticket = _ticketService.Details(ticket.Id);

            var userIds = ticket.NotifyTo?.Split(',').ToList() ?? new List<string>();
            userIds.Add(ticket.RequestorId);
            userIds.Add(ticket.AssignedToId);
            userIds.Add(ticket.CreatedById);
            List<string> contributors = _userService.GetEmailsById(userIds.ToArray());

            ticket.LinkToTicketDetails =
                $"{Request.Url.Scheme}://{Request.Url.Host}:{Request.Url.Port}{Url.Action("Edit", "Ticket", new { id = ticket.Id })}";
            string emailMessage = _ticketService.ComposeTicketEditEmail(ticket, changes);
            _emailService.SendEmail(emailMessage, $"Ticket Updated: {ticket.Title}", contributors.Distinct().ToArray());

            return View(ticket);
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(_ticketService.All().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request,
            Ticket ticket)
        {
           //removing all field related to date which is failing for ticket update
            ModelState.Remove("Requestor.PasswordLastChanged");
            ModelState.Remove("AssignedTo.PasswordLastChanged");
            ModelState.Remove("CreatedBy.PasswordLastChanged");
            ModelState.Remove("ModifiedBy.PasswordLastChanged");
            ModelState.Remove("Requestor.RegistrationAddress.Address1");
            ModelState.Remove("AssignedTo.RegistrationAddress.Address1");
            ModelState.Remove("CreatedBy.RegistrationAddress.Address1");
            ModelState.Remove("ModifiedBy.RegistrationAddress.Address1");
            ModelState.Remove("AssignedTo.LastActivityDateUtc");
            ModelState.Remove("CreatedBy.LastActivityDateUtc");
            ModelState.Remove("ModifiedBy.LastActivityDateUtc");
            ModelState.Remove("Requestor.LastActivityDateUtc");
            ModelState.Remove("Requestor.RegistrationAddress.City");
            ModelState.Remove("AssignedTo.RegistrationAddress.City");
            ModelState.Remove("CreatedBy.RegistrationAddress.City");
            ModelState.Remove("ModifiedBy.RegistrationAddress.City");
            ModelState.Remove("CreatedBy.DateCreatedUtc");
            ModelState.Remove("CreatedBy.LastUpdatedUtc");
            ModelState.Remove("ModifiedBy.DateCreatedUtc");
            ModelState.Remove("ModifiedBy.LastUpdatedUtc");
            ModelState.Remove("Requestor.DateCreatedUtc");
            ModelState.Remove("Requestor.LastUpdatedUtc");
            ModelState.Remove("AssignedTo.DateCreatedUtc");
            ModelState.Remove("AssignedTo.LastUpdatedUtc");
            ModelState.Remove("CreatedBy.RegistrationAddress.State.CreatedOnUtc");
            ModelState.Remove("CreatedBy.RegistrationAddress.Country.CreatedOnUtc");
            ModelState.Remove("ModifiedBy.RegistrationAddress.State.CreatedOnUtc");
            ModelState.Remove("ModifiedBy.RegistrationAddress.Country.CreatedOnUtc");
            ModelState.Remove("Requestor.RegistrationAddress.State.CreatedOnUtc");
            ModelState.Remove("Requestor.RegistrationAddress.Country.CreatedOnUtc");
            ModelState.Remove("AssignedTo.RegistrationAddress.State.CreatedOnUtc");
            ModelState.Remove("AssignedTo.RegistrationAddress.Country.CreatedOnUtc");
            ModelState.Remove("CreatedDateUtc");
            ModelState.Remove("ModifiedDateUtc");
            ModelState.Remove("Status.CreatedDateUtc");
            ModelState.Remove("Status.ModifiedDateUtc");
            ModelState.Remove("Category.CreatedDateUtc");
            ModelState.Remove("Category.ModifiedDateUtc");
            ModelState.Remove("TicketProject.CreatedDateUtc");
            ModelState.Remove("TicketProject.ModifiedDateUtc");
            ModelState.Remove("Type.CreatedDateUtc");
            ModelState.Remove("Type.ModifiedDateUtc");
            ModelState.Remove("Priority.CreatedDateUtc");
            ModelState.Remove("Priority.ModifiedDateUtc");
    
            if (ticket != null && ModelState.IsValid)
            {
                ticket.ModifiedById = OperatingUser.Id;
                ticket.ModifiedDateUtc = DateTime.UtcNow;
                var changes = new List<ChangeLog>();
                _ticketService.Update(ticket, ref changes);
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
            return Json(new[] {ticket}.ToDataSourceResult(request, ModelState));
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

        #region Ticket Category Config

        public ActionResult Config()
        {
            ViewData["IsSuperAdmin"] = OperatingUser.UserTypeId == (int) UserType.SuperAdmin;
            return View();
        }

        public ActionResult ReadCategory([DataSourceRequest] DataSourceRequest request)
        {
            var categoryList = _ticketCategoryService.GetCategoryByCompanyIdList(OperatingUser.CompanyId);
            return Json(categoryList.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult AddCategory([DataSourceRequest] DataSourceRequest request, TicketCategory category)
        {
            category.CompanyId = OperatingUser.CompanyId;
            category.CreatedById = OperatingUser.Id;
            category.ModifiedById = OperatingUser.Id;
            category= _ticketCategoryService.Add(category);
            return Json(new[] {category}.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult UpdateCategory([DataSourceRequest] DataSourceRequest request, TicketCategory category)
        {
            category.ModifiedById = OperatingUser.Id;
            category= _ticketCategoryService.Update(category);
            return Json(new[] {category}.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult DeleteCategory([DataSourceRequest] DataSourceRequest request, TicketCategory category)
        {
            category.ModifiedById = OperatingUser.Id;
            _ticketCategoryService.Delete(category);
            return Json(new[] { category }.ToDataSourceResult(request));
        }
        #endregion

        #region Ticket Priority Config

        public ActionResult ReadPriority([DataSourceRequest] DataSourceRequest request)
        {
            var priorityList = _ticketPriority.GetPriorityByCompanyIdList(OperatingUser.CompanyId);
            return Json(priorityList.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult AddPriority([DataSourceRequest] DataSourceRequest request, TicketPriority ticketPriority)
        {
            ticketPriority.CompanyId = OperatingUser.CompanyId;
            ticketPriority.CreatedById = OperatingUser.Id;
            ticketPriority.ModifiedById = OperatingUser.Id;
            _ticketPriority.Add(ticketPriority);
            return Json(new[] {ticketPriority}.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult UpdatePriority([DataSourceRequest] DataSourceRequest request, TicketPriority ticketPriority)
        {
            ticketPriority.ModifiedById = OperatingUser.Id;
            _ticketPriority.Update(ticketPriority);
            return Json(new[] {ticketPriority}.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult DeletePriority([DataSourceRequest] DataSourceRequest request, TicketPriority ticketPriority)
        {
            ticketPriority.ModifiedById = OperatingUser.Id;
            _ticketPriority.Delete(ticketPriority);
            return Json(new[] { ticketPriority }.ToDataSourceResult(request));
        }

        #endregion

        #region Ticket Status Config

        public ActionResult ReadStatus([DataSourceRequest] DataSourceRequest request)
        {
            var statusList = _ticketStatus.GetStatusByCompanyIdList(OperatingUser.CompanyId);
            return Json(statusList.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult AddStatus([DataSourceRequest] DataSourceRequest request, TicketStatus ticketStatus)
        {
            ticketStatus.CompanyId = OperatingUser.CompanyId;
            ticketStatus.CreatedById = OperatingUser.Id;
            ticketStatus.ModifiedById = OperatingUser.Id;
            _ticketStatus.Add(ticketStatus);
            return Json(new[] {ticketStatus}.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult UpdateStatus([DataSourceRequest] DataSourceRequest request, TicketStatus ticketStatus)
        {
            ticketStatus.ModifiedById = OperatingUser.Id;
            _ticketStatus.Update(ticketStatus);
            return Json(new[] {ticketStatus}.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult DeleteStatus([DataSourceRequest] DataSourceRequest request, TicketStatus ticketStatus)
        {
            ticketStatus.ModifiedById = OperatingUser.Id;
            _ticketStatus.Delete(ticketStatus);
            return Json(new[] { ticketStatus }.ToDataSourceResult(request));
        }

        #endregion

        #region Ticket Type Config

        public ActionResult ReadType([DataSourceRequest] DataSourceRequest request)
        {
            var typeList = _ticketType.GetTickettypeByCompanyIdList(OperatingUser.CompanyId);
            return Json(typeList.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult AddType([DataSourceRequest] DataSourceRequest request, TicketType ticketType)
        {
            ticketType.CompanyId = OperatingUser.CompanyId;
            ticketType.CreatedById = OperatingUser.Id;
            ticketType.ModifiedById = OperatingUser.Id;
            _ticketType.Add(ticketType);
            return Json(new[] {ticketType}.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult UpdateType([DataSourceRequest] DataSourceRequest request, TicketType ticketType)
        {
            ticketType.ModifiedById = OperatingUser.Id;
            _ticketType.Update(ticketType);
            return Json(new[] {ticketType}.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult DeleteType([DataSourceRequest] DataSourceRequest request, TicketType ticketType)
        {
            ticketType.ModifiedById = OperatingUser.Id;
            _ticketType.Delete(ticketType);
            return Json(new[] {ticketType}.ToDataSourceResult(request));
        }


        #endregion

        #region Ticket Project Config

        public ActionResult ReadProject([DataSourceRequest] DataSourceRequest request)
        {
            var typeList = _ticketProject.GetProjectsByCompanyId(OperatingUser.CompanyId);
            return Json(typeList.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult AddProject([DataSourceRequest] DataSourceRequest request, TicketProject ticketProject)
        {
            ticketProject.CompanyId = OperatingUser.CompanyId;
            ticketProject.CreatedById = OperatingUser.Id;
            ticketProject.ModifiedById = OperatingUser.Id;
            _ticketProject.Add(ticketProject);
            return Json(new[] { ticketProject }.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult UpdateProject([DataSourceRequest] DataSourceRequest request, TicketProject ticketProject)
        {
            ticketProject.ModifiedById = OperatingUser.Id;
            _ticketProject.Update(ticketProject);
            return Json(new[] { ticketProject }.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult DeleteProject([DataSourceRequest] DataSourceRequest request, TicketProject ticketProject)
        {
            ticketProject.ModifiedById = OperatingUser.Id;
            _ticketProject.Delete(ticketProject.Id);
            return Json(new[] { ticketProject }.ToDataSourceResult(request));
        }


        #endregion
    }
}