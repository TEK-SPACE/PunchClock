using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PunchClock.Core.DataAccess;
using PunchClock.Domain.Model;
using PunchClock.Ticketing.Contracts;
using PunchClock.Ticketing.Model;

namespace PunchClock.Ticketing.Services
{
    public class TicketProjectService:ITicketProject
    {
        public TicketProject Add(TicketProject project)
        {
            using (var context = new PunchClockDbContext())
            {
                context.TicketProjects.Add(project);
                context.SaveChanges();
            }
            return project;
        }

        public TicketProject Update(TicketProject project)
        {
            using (var context = new PunchClockDbContext())
            {
                var ticketProject = context.TicketProjects.FirstOrDefault(x => x.Id == project.Id);
                if (ticketProject == null) return project;
                ticketProject.Name = project.Name;
                ticketProject.Description = project.Description;
                ticketProject.ModifiedById = project.ModifiedById;
                ticketProject.ModifiedDateUtc = DateTime.UtcNow;
                context.SaveChanges();
            }
            return project;
        }

        public AjaxResponse Delete(int id)
        {
            throw new NotImplementedException();
        }

        public TicketProject GetProjectById(int id)
        {
            throw new NotImplementedException();
        }

        public List<TicketProject> GetAllTicketProjects()
        {
            throw new NotImplementedException();
        }

        public List<TicketProject> GetProjectsByCompanyId(int companyId)
        {
            using (var context = new PunchClockDbContext())
            {
                return context.TicketProjects.Where(x => x.IsDeleted == false && x.CompanyId == companyId).ToList();
            }
        }
    }
}
