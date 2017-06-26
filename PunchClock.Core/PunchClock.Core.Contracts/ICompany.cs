using System;
using System.Collections.Generic;
using PunchClock.Domain.Model;
using PunchClock.Domain.Model.Enum;
using PunchClock.View.Model;

namespace PunchClock.Core.Contracts
{
    public interface ICompany
    {
        int Add(Company company);
        List<Company> GetBy(string name);
        Company Get(string code);
        Company Get(int companyId);
        void SetCreatedBy(int companyId, string userId);
        CompanyTransaction Update(Company company);
        List<EmployeePaidHolidayView> PaidHolidayPkg(int companyId);
        void UpdatePaidHolidayPkg(List<EmployeePaidHolidayView> pkgs);
        List<CompanyHolidayView> CompanyHolidays(int companyId);
        void UpdateCompanyHolidays(List<CompanyHolidayView> hlds);
        List<HolidayView> GetCompanyHolidays(int companyId, int userId, DateTime stDate, DateTime enDate);
        List<SiteMenu> GetSiteMap(int companyId);
    }
}
