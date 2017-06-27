using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using PunchClock.Core.Contracts;
using PunchClock.Core.DataAccess;
using PunchClock.Domain.Model;

namespace PunchClock.Core.Implementation
{
    public class SiteService : ISite
    {
        public List<Country> GetCountries()
        {
            using (var context = new PunchClockDbContext())
            {
                return context.Countries.ToList();
            }
        }

        public List<State> GetStates(int countryId)
        {
            using (var context = new PunchClockDbContext())
            {
                return context.States.Where(x => x.CountryId == countryId).ToList();
            }
        }

        public List<SelectListItem> GetEmploymentTypes(int companyId)
        {
            List<SelectListItem> employeeTypes;
            using (var context = new PunchClockDbContext())
            {
                employeeTypes = context.EmploymentTypes.Select(x =>
                    new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList();
            }
            return employeeTypes;
        }
    }
}