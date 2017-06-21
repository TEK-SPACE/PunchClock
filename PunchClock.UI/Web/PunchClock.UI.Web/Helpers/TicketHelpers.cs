using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PunchClock.Ticketing.Contracts;
using PunchClock.Ticketing.Model;
using PunchClock.Ticketing.Services;

namespace PunchClock.UI.Web.Helpers
{
    public static class TicketHelpers
    {
        private static readonly ITicketStatus TicketStatus;

        static TicketHelpers()
        {
            TicketStatus=new TicketStatusService();
        }
        public static List<SelectListItem> GetStatusByCompanyId(int companyId)
        {
            var ticketStatuses = TicketStatus.GetStatusByCompanyIdList(companyId);

            var returnList = new List<SelectListItem>();
            var defaultValue = new SelectListItem
            {
                Text = "Select Status",
                Value = "0"
            };

            returnList.Add(defaultValue);
            if (ticketStatuses != null)
                returnList.AddRange(ticketStatuses.Select(status => new SelectListItem
                {
                    Text = status.Name,
                    Value = status.Id.ToString()
                }));

            return returnList;
        }
    }
}