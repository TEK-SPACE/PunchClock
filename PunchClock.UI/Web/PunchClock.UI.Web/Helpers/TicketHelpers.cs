using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PunchClock.Core.Contracts;
using PunchClock.Core.Implementation;
using PunchClock.Ticketing.Contracts;
using PunchClock.Ticketing.Model;
using PunchClock.Ticketing.Services;

namespace PunchClock.UI.Web.Helpers
{
    public static class TicketHelpers
    {
        private static readonly ITicketStatus TicketStatus;
        private static readonly IUserRepository UserRepository;

        static TicketHelpers()
        {
            TicketStatus=new TicketStatusService();
            UserRepository = new UserService();
        }
        public static List<SelectListItem> GetTicketStatusByCompanyId(string userId)

        {
            var user = UserRepository.DetailsByKey(userId);
            var ticketStatuses = TicketStatus.GetStatusByCompanyIdList(user.CompanyId);

            var returnList = new List<SelectListItem>();
             if (ticketStatuses != null)
                returnList.AddRange(ticketStatuses.Select(status => new SelectListItem
                {
                    Text = status.Name,
                    Value = status.Id.ToString()
                }));

            return returnList;
        }
        public static List<SelectListItem> GetTicketPriorityByCompanyId(string userId)
        {
            var user = UserRepository.DetailsByKey(userId);
            //need to replace below code with get data from service like getting for status. this is for default use

            var returnList = new List<SelectListItem>();
            var asc = new SelectListItem
            {
                Text = "AdminPri",
                Value = "1"
            };
            returnList.Add(asc);

            var dsc = new SelectListItem
            {
                Text = "TicketingPri",
                Value = "2"
            };
            returnList.Add(dsc);

            return returnList;
        }

        public static List<SelectListItem> GetTicketTypeByCompanyId(string userId)
        {
            var user = UserRepository.DetailsByKey(userId);
            //need to replace below code with get data from service like getting for status. this is for default use

            var returnList = new List<SelectListItem>();
            var asc = new SelectListItem
            {
                Text = "AdminType",
                Value = "1"
            };
            returnList.Add(asc);

            var dsc = new SelectListItem
            {
                Text = "TicketingType",
                Value = "2"
            };
            returnList.Add(dsc);

            return returnList;
        }
        public static List<SelectListItem> GetTicketCategoryByCompanyId(string userId)
        {
            var user = UserRepository.DetailsByKey(userId);
            //need to replace below code with get data from service like getting for status. this is for default use
            var returnList = new List<SelectListItem>();
            var asc = new SelectListItem
            {
                Text = "AdminType",
                Value = "1"
            };
            returnList.Add(asc);

            var dsc = new SelectListItem
            {
                Text = "TicketingType",
                Value = "2"
            };
            returnList.Add(dsc);

            return returnList;
        }

    }
}