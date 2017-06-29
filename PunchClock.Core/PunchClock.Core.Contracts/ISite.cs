using System.Collections.Generic;
using System.Web.Mvc;
using PunchClock.Domain.Model;

namespace PunchClock.Core.Contracts
{
    public interface  ISite
    {
        List<Country> GetCountries();
        List<State> GetStates(int countryId);
        List<SelectListItem> GetEmploymentTypes(int companyId);
        void Delete(SiteMap siteMap);
        void Update(SiteMap siteMap);
        void Add(SiteMap siteMap);
        List<SiteMap> All(int companyId, bool isAdmin);
        SiteMap Details(int id);
    }
}
