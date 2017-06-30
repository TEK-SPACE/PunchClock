using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PunchClock.Domain.Model;
using PunchClock.Ticketing.Model;

namespace PunchClock.Ticketing.Contracts
{
    public interface ITicketProject
    {
        TicketProject Add(TicketProject project);
        TicketProject Update(TicketProject project);
        AjaxResponse Delete(int id);

        TicketProject GetProjectById(int id);
        List<TicketProject> GetAllTicketProjects();
        List<TicketProject> GetProjectsByCompanyId(int companyId);
       
    }
}
