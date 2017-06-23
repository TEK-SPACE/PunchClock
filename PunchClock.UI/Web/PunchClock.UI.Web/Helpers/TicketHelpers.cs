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
        private static readonly ITicketCategory TicketCategory;
        private static readonly ITicketPriority TicketPriority;
        private static readonly ITicketType TicketType;
        private static readonly IUserRepository UserRepository;

        static TicketHelpers()
        {
            TicketStatus = new TicketStatusService();
            TicketCategory = new TicketCategoryService();
            TicketPriority = new TicketPriorityService();
            TicketType = new TicketTypeService();
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
            var ticketPriorities = TicketPriority.GetPriorityByCompanyIdList(user.CompanyId);

            var returnList = new List<SelectListItem>();
            if (ticketPriorities != null)
                returnList.AddRange(ticketPriorities.Select(priority => new SelectListItem
                {
                    Text = priority.Name,
                    Value = priority.Id.ToString()
                }));
            return returnList;
        }

        public static List<SelectListItem> GetTicketTypeByCompanyId(string userId)
        {
            var user = UserRepository.DetailsByKey(userId);
            //need to replace below code with get data from service like getting for status. this is for default use
            var ticketTypes = TicketType.GetTickettypeByCompanyIdList(user.CompanyId);

            var returnList = new List<SelectListItem>();
            if (ticketTypes != null)
                returnList.AddRange(ticketTypes.Select(category => new SelectListItem
                {
                    Text = category.Name,
                    Value = category.Id.ToString()
                }));
            return returnList;
        }
        public static List<SelectListItem> GetTicketCategoryByCompanyId(string userId)
        {
            var user = UserRepository.DetailsByKey(userId);
            //need to replace below code with get data from service like getting for status. this is for default use
            var ticketCategories = TicketCategory.GetCategoryByCompanyIdList(user.CompanyId);

            var returnList = new List<SelectListItem>();
            if (ticketCategories != null)
                returnList.AddRange(ticketCategories.Select(category => new SelectListItem
                {
                    Text = category.Name,
                    Value = category.Id.ToString()
                }));
            return returnList;
        }
    }
}