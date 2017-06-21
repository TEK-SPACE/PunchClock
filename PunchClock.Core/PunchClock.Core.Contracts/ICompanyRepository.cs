using System;
using System.Collections.Generic;
using PunchClock.Core.Models.Common.Enum;
using PunchClock.Domain.Model;
using PunchClock.View.Model;

namespace PunchClock.Core.Contracts
{
    public interface ICompanyRepository
    {
        int Add(Company company);
        List<Company> GetBy(string name);
        Company Get(string code);
        Company Get(int companyId);
        void SetCreatedBy(int companyId, int userId);
        CompanyTransaction Update(Company company);
        List<EmployeePaidHolidayView> PaidHolidayPkg(int companyId);
        void UpdatePaidHolidayPkg(List<EmployeePaidHolidayView> pkgs);
        List<CompanyHolidayView> CompanyHolidays(int companyId);
        void UpdateCompanyHolidays(List<CompanyHolidayView> hlds);
        List<HolidayView> GetCompanyHolidays(int companyId, int userId, DateTime stDate, DateTime enDate);
    }
}
