using System;
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

        public void Delete(SiteMap siteMap)
        {
            using (var context = new PunchClockDbContext())
            {
                var entity = context.SiteMaps.FirstOrDefault(x => x.Id == siteMap.Id);
                if (entity == null)
                    return;
                entity.IsDeleted = true;
                entity.ModifiedDateUtc = DateTime.UtcNow;
                context.SaveChanges();
            }
        }

        public void Update(SiteMap siteMap)
        {
            using (var context = new PunchClockDbContext())
            {
                var entity = context.SiteMaps.FirstOrDefault(x => x.Id == siteMap.Id);
                if (entity == null)
                    return;
                entity.Name = siteMap.Name;
                entity.ModifiedDateUtc = DateTime.UtcNow;
                context.SaveChanges();
            }
        }

        public void Add(SiteMap siteMap)
        {
            using (var context = new PunchClockDbContext())
            {
                context.SiteMaps.Add(siteMap);
                context.SaveChanges();
            }
        }

        public List<SiteMap> All(int companyId, bool isAdmin)
        {
            using (var context = new PunchClockDbContext())
            {
                return isAdmin
                    ? context.SiteMaps.ToList()
                    : context.SiteMaps.Where(x => x.CompanyId == companyId).ToList();
            }
        }

        public SiteMap Details(int id)
        {
            using (var context = new PunchClockDbContext())
            {
                return context.SiteMaps.FirstOrDefault(x => x.Id == id);
            }
        }
    }
}