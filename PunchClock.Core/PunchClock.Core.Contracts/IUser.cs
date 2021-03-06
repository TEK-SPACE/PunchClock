﻿using System.Collections.Generic;
using System.Web.Mvc;
using PunchClock.Domain.Model;

namespace PunchClock.Core.Contracts
{
    public interface IUser 
    {
        User DetailsByKey(string userId);
        User DetailsById(int userId);
        User Details(string userName);
        int Update(User user, bool b);
        int Add(User user);
        string SeedPasswordReset(string id);
        User ByGuid(string uid);
        string RandomString();
        string RandomNumber();
        List<UserType> GetTypes();
        User ByEmail(string modelEmail);
        List<User> All(int companyId);
        int AddAddress(Address userRegistrationAddress);
        string ComposeRegisteredEmail(User user);
        List<string> GetEmailsById(string[] split);
        List<SelectListItem> GetAllCompanyEmployees(int companyId, int opUserTypeId);
        User Details(int userId);
    }
}
