using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using PunchClock.Core.DataAccess;
using PunchClock.Ticketing.Contracts;
using PunchClock.Ticketing.Model;
using PunchClock.Domain.Model;
using PunchClock.Configuration.Contract;
using PunchClock.Configuration.Model.Constants;
using PunchClock.Configuration.Service;
using PunchClock.Domain.Model.Enum;
using RedandBlue.Common;
using RedandBlue.Common.Logging;

namespace PunchClock.Ticketing.Services
{
    public class TicketService : ITicket
    {
        private readonly IAppSetting _appSettingService;

        public TicketService()
        {
            _appSettingService = new AppSettingService();
        }
        public Ticket Add(Ticket ticket)
        {
            using (PunchClockDbContext context = new PunchClockDbContext())
            {
                if (ticket.DueDateUtc.HasValue)
                    ticket.DueDateUtc = ticket.DueDateUtc.Value.ToUniversalTime();
                context.Tickets.Add(ticket);
                context.SaveChanges();
            }
            return ticket;
        }

        public List<Ticket> All()
        {
            using (var context = new PunchClockDbContext())
            {
                return context.Tickets.Where(x => !x.IsDeleted)
                    .Include(x => x.Type)
                    .Include(x => x.Status)
                    .Include(x => x.Priority)
                    .Include(x => x.TicketProject)
                    .Include(x => x.Category)
                    .Include(x => x.AssignedTo)
                    .Include(x => x.Requestor)
                    .Include(x => x.CreatedBy)
                    .Include(x => x.ModifiedBy)
                    .Include(x=>x.Company)
                    .Include(x => x.Category)
                    .ToList();
            }
        }
        

        public void Delete(Ticket ticket)
        {
            using (var context = new PunchClockDbContext())
            {
                var entity = context.Tickets.FirstOrDefault(x => x.Id == ticket.Id);
                if (ticket == null) return;
                if (entity != null) entity.IsDeleted = true;
                context.SaveChanges();
            }
        }

        public AjaxResponse Delete(int ticketId)
        {
            var response = new AjaxResponse
            {
                ResponseId = ticketId,
                ResponseText = "Record is not deleted",
                Success = false
            };
            using (var context = new PunchClockDbContext())
            {
                var ticket = context.Tickets.FirstOrDefault(x => x.Id == ticketId);
                if (ticket == null) return response;
                response.Success = true;
                response.ResponseText = "Data Deleted Successfully";
                return response;
            }
        }

        public List<TicketCategory> GetCategories(int companyId)
        {
            using (var context = new PunchClockDbContext())
            {
                return context.TicketCategories.Where(x => x.CompanyId == companyId || x.IsCoreItem).OrderBy(x => x.DisplayOrder)
                    .ToList();
            }
        }

        public List<TicketProject> GetProjects(int companyId)
        {
            using (var context = new PunchClockDbContext())
            {
                return context.TicketProjects.Where(x => x.CompanyId == companyId || x.IsCoreItem).OrderBy(x => x.DisplayOrder)
                    .ToList();
            }
        }

        public Ticket Details(int id)
        {
            using (var context = new PunchClockDbContext())
            {
                return context.Tickets
                    .Include(x=>x.CreatedBy)
                    .Include(x=>x.Requestor)
                    .Include(x => x.AssignedTo)
                    .Include(x => x.TicketProject)
                    .Include(x => x.Status)
                    .Include(x => x.Type)
                    .Include(x => x.Priority)
                    .Include(x => x.Category)
                    .Include(x=>x.Comments).FirstOrDefault(x => x.Id == id);
            }
        }

        public string ComposeTicketCreatedEmail(Ticket ticket)
        {
            var appSettings = _appSettingService.GetByModules((int)ModuleType.Core, (int)ModuleType.Ticketing);
            var logoPath = appSettings
                .First(x => x.Key.Equals(AppKey.CoreEmailTemplateLogoPath, StringComparison.OrdinalIgnoreCase))
                .Value;

            var templateName = appSettings
                .First(x => x.Key.Equals(AppKey.TicketCreateEmailTemplate, StringComparison.OrdinalIgnoreCase))
                .Value;
            var emailTemplatePath = Path.Combine(Util.AssemblyDirectory, "Templates", "Email","Ticket", templateName);
            if (!File.Exists(emailTemplatePath))
            {
                Log.Error($"Template doesnt't exists at {emailTemplatePath}");
            }
            else
            {
                var ticketLink = ticket.LinkToTicketDetails;
                ticket = Details(ticket.Id);
                ticket.LinkToTicketDetails = ticketLink;
                var emailContent = File.ReadAllText(emailTemplatePath);
                emailContent = emailContent.Replace("#Logo#", logoPath);
                emailContent = emailContent.Replace("#Title#", ticket.Title);
                emailContent = emailContent.Replace("#Project#", ticket.TicketProject.Name);
                emailContent = emailContent.Replace("#Priority#", ticket.Priority.Name);
                emailContent = emailContent.Replace("#Description#", ticket.Description);
                emailContent = emailContent.Replace("#Status#", ticket.Status.Name);
                emailContent = emailContent.Replace("#Type#", ticket.Type.Name);
                emailContent = emailContent.Replace("#Requestor#", ticket.Requestor.DisplayName);
                emailContent = emailContent.Replace("#AssignedTo#", ticket.AssignedTo.DisplayName);
                emailContent = emailContent.Replace("#Category#", ticket.Category.Name);
                emailContent = emailContent.Replace("#DueDate#", ticket.DueDateUtc?.ToString() ?? "");
                emailContent = emailContent.Replace("#EstimatedEffort#", ticket.EstimatedEffort.ToString("00"));
                emailContent = emailContent.Replace("#WorkCompleted#", ticket.CompletedWork.ToString("00"));
                emailContent = emailContent.Replace("#CreatedOn#", ticket.CreatedDateUtc.ToString("F"));
                emailContent = emailContent.Replace("#CreatedBy#", ticket.CreatedBy.DisplayName);
                emailContent = emailContent.Replace("#LinkToTicketDetails#", ticket.LinkToTicketDetails);
                return emailContent;
            }
            return string.Empty;
        }

        public string ComposeTicketEditEmail(Ticket ticket, List<ChangeLog> changeLogs)
        {
            var appSettings = _appSettingService.GetByModules((int)ModuleType.Core, (int)ModuleType.Ticketing);

            var logoPath = appSettings
                .First(x => x.Key.Equals(AppKey.CoreEmailTemplateLogoPath, StringComparison.OrdinalIgnoreCase))
                .Value;

            var templateName = appSettings
                .First(x => x.Key.Equals(AppKey.TicketEditEmailTemplate, StringComparison.OrdinalIgnoreCase))
                .Value;
            var emailTemplatePath = Path.Combine(Util.AssemblyDirectory, "Templates", "Email", "Ticket", templateName);
            if (!File.Exists(emailTemplatePath))
            {
                Log.Error($"Template doesnt't exists at {emailTemplatePath}");
            }
            else
            {
                var rowTemplate =
                    "<tr style='#Style#'><td><strong>#FieldName#</strong></td> <td>#OldValue#</td><td>#NewValue#</td></tr>";
                int i = 0;
                var changedContent = string.Empty;
                foreach (var change in changeLogs)
                {
                    int oldValue;
                    int newValue;
                    using (var context = new PunchClockDbContext())
                    {
                        var status = context.TicketStatuses;
                        var categories = context.TicketCategories;
                        var priorities = context.TicketPriorities;
                        var projects = context.TicketProjects;
                        var types = context.TicketTypes;
                        switch (change.PropertyName)
                        {
                            case "StatusId":
                                change.PropertyName = "Status";
                                oldValue = Convert.ToInt32(change.OldValue);
                                newValue = Convert.ToInt32(change.NewValue);
                                change.OldValue = status.FirstOrDefault(x => x.Id == oldValue)?.Name ?? change.OldValue;
                                change.NewValue = status.FirstOrDefault(x => x.Id == newValue)?.Name ?? change.NewValue;
                                break;
                            case "CategoryId":
                                change.PropertyName = "Category";
                                oldValue = Convert.ToInt32(change.OldValue);
                                newValue = Convert.ToInt32(change.NewValue);
                                change.OldValue = categories.FirstOrDefault(x => x.Id == oldValue)?.Name ?? change.OldValue;
                                change.NewValue = categories.FirstOrDefault(x => x.Id == newValue)?.Name ?? change.NewValue;
                                break;
                            case "PriorityId":
                                change.PropertyName = "Priority";
                                oldValue = Convert.ToInt32(change.OldValue);
                                newValue = Convert.ToInt32(change.NewValue);
                                change.OldValue = priorities.FirstOrDefault(x => x.Id == oldValue)?.Name ?? change.OldValue;
                                change.NewValue = priorities.FirstOrDefault(x => x.Id == newValue)?.Name ?? change.NewValue;
                                break;
                            case "ProjectId":
                                change.PropertyName = "Project";
                                oldValue = Convert.ToInt32(change.OldValue);
                                newValue = Convert.ToInt32(change.NewValue);
                                change.OldValue = projects.FirstOrDefault(x => x.Id == oldValue)?.Name ?? change.OldValue;
                                change.NewValue = projects.FirstOrDefault(x => x.Id == newValue)?.Name ?? change.NewValue;
                                break;
                            case "TypeId":
                                change.PropertyName = "Type";
                                oldValue = Convert.ToInt32(change.OldValue);
                                newValue = Convert.ToInt32(change.NewValue);
                                change.OldValue = types.FirstOrDefault(x => x.Id == oldValue)?.Name ?? change.OldValue;
                                change.NewValue = types.FirstOrDefault(x => x.Id == newValue)?.Name ?? change.NewValue;
                                break;
                            case "RequestorId":
                            case "AssignedToId":
                                if(change.PropertyName == "RequestorId")
                                    change.PropertyName = "Requestor";
                                if (change.PropertyName == "AssignedToId")
                                    change.PropertyName = "Assigned To";
                                change.OldValue = context.Users.FirstOrDefault(x => x.Id == change.OldValue)?.DisplayName ?? change.OldValue;
                                change.NewValue = context.Users.FirstOrDefault(x => x.Id == change.NewValue)?.DisplayName ?? change.NewValue;
                                break;
                            case "NotifyTo":
                                change.PropertyName = "Notify To";
                                if (change.OldValue == null)
                                    change.OldValue = string.Empty;
                                if (change.NewValue == null)
                                    change.NewValue = string.Empty;
                                var oldUserIds = change.OldValue.Split(',');
                                var newUserIds = change.NewValue.Split(',');
                                change.OldValue = string.Join(" | ", context.Users.Where(x=> oldUserIds.Any(u=> x.Id == u)).ToList().Select(x=> x.DisplayName));
                                change.NewValue = string.Join(" | ", context.Users.Where(x => newUserIds.Any(u => x.Id == u)).ToList().Select(x => x.DisplayName));
                                break;
                        }
                    }
                    changedContent += rowTemplate.Replace("#FieldName#", change.PropertyName)
                        .Replace("#OldValue#", change.OldValue)
                        .Replace("#NewValue#", change.NewValue)
                        .Replace("#Style#", i % 2 == 0 ? "background: #eee;" : "background: #fff;");
                    i++;
                }

                var emailContent = File.ReadAllText(emailTemplatePath);
                var ticketLink = ticket.LinkToTicketDetails;
                ticket = Details(ticket.Id);
                ticket.LinkToTicketDetails = ticketLink;
                emailContent = emailContent.Replace("#Logo#", logoPath);
                emailContent = emailContent.Replace("#RowTemplate#", changedContent);

                emailContent = emailContent.Replace("#Title#", ticket.Title);
                emailContent = emailContent.Replace("#Project#", ticket.TicketProject.Name);
                emailContent = emailContent.Replace("#Priority#", ticket.Priority.Name);
                emailContent = emailContent.Replace("#Description#", ticket.Description);
                emailContent = emailContent.Replace("#Status#", ticket.Status.Name);
                emailContent = emailContent.Replace("#Type#", ticket.Type.Name);
                emailContent = emailContent.Replace("#Requestor#", ticket.Requestor.DisplayName);
                emailContent = emailContent.Replace("#AssignedTo#", ticket.AssignedTo.DisplayName);
                emailContent = emailContent.Replace("#Category#", ticket.Category.Name);
                emailContent = emailContent.Replace("#DueDate#", ticket.DueDateUtc?.ToString() ?? "");
                emailContent = emailContent.Replace("#EstimatedEffort#", ticket.EstimatedEffort.ToString("00"));
                emailContent = emailContent.Replace("#WorkCompleted#", ticket.CompletedWork.ToString("00"));
                emailContent = emailContent.Replace("#CreatedOn#", ticket.CreatedDateUtc.ToString("F"));
                emailContent = emailContent.Replace("#CreatedBy#", ticket.CreatedBy.DisplayName);
                emailContent = emailContent.Replace("#LinkToTicketDetails#", ticket.LinkToTicketDetails);
                return emailContent;
            }
            return string.Empty;
        }

        public List<TicketType> GetTypes(int companyId)
        {
            using (var context = new PunchClockDbContext())
            {
                return context.TicketTypes.Where(x => x.CompanyId == companyId || x.IsCoreItem).OrderBy(x => x.DisplayOrder).ToList();
            }
        }

        public List<TicketPriority> GetPriorties(int companyId)
        {
            using (var context = new PunchClockDbContext())
            {
                return context.TicketPriorities.Where(x => x.CompanyId == companyId || x.IsCoreItem).OrderBy(x => x.DisplayOrder).ToList();
            }
        }

        public List<TicketStatus> GetStatusus(int companyId)
        {
            using (var context = new PunchClockDbContext())
            {
                return context.TicketStatuses.Where(x=>x.CompanyId == companyId || x.IsCoreItem).OrderBy(x => x.DisplayOrder).ToList();
            }
        }

        public Ticket Update(Ticket ticket, ref List<ChangeLog> changeLogs)
        {
            using (var context = new PunchClockDbContext())
            {
                var entity = context.Tickets.FirstOrDefault(x => x.Id == ticket.Id);
                if (entity == null)
                    return ticket;
                entity.Title = ticket.Title;
                entity.ProjectId = ticket.ProjectId;
                entity.PriorityId = ticket.PriorityId;
                entity.Description = ticket.Description;
                entity.StatusId = ticket.StatusId;
                entity.TypeId = ticket.TypeId;
                entity.RequestorId = ticket.RequestorId;
                entity.AssignedToId = ticket.AssignedToId;
                entity.NotifyTo = ticket.NotifyTo;
                entity.CategoryId = ticket.CategoryId;
                entity.EstimatedEffort = ticket.EstimatedEffort;
                entity.CompletedWork = ticket.CompletedWork;
                //entity.DueDateUtc = ticket.DueDateUtc;
                if (ticket.DueDateUtc.HasValue)
                    entity.DueDateUtc = ticket.DueDateUtc.Value.ToUniversalTime();
                if (ticket.Comments != null && ticket.Comments.Any())
                    context.TicketComments.AddOrUpdate(ticket.Comments.ToArray());
                changeLogs = context.GetEntityChanges();
                context.SaveChanges();
            }
            return ticket;
        }
    }
}