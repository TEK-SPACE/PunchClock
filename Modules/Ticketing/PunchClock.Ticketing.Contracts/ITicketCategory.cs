﻿using System.Collections.Generic;
using PunchClock.Core.Models.Common;
using PunchClock.Ticketing.Model;

namespace PunchClock.Ticketing.Contracts
{
    public interface ITicketCategory
    {
        TicketCategory Add(TicketCategory category);
        TicketCategory Update(TicketCategory category);
        AjaxResponse Delete(int id);

        TicketCategory GetTicketCategoryId(int id);
        List<TicketCategory> GetAllTicketCategry();
        List<TicketCategory> GetCategoryByCompanyIdList(int companyId);


    }
}
