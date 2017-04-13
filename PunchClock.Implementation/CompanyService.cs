using PunchClock.DAL;
using PunchClock.Domain.Model;
using System.Collections.Generic;
using Omu.ValueInjecter;
using System;
using System.Linq;
using System.Data.Entity;
using PunchClock.Model.Mapper;
using PunchClock.Objects.Core.Enum;
using UserType = PunchClock.Objects.Core.Enum.UserType;

namespace PunchClock.Implementation
{
    public class CompanyService
    {
        public int Add(View.Model.CompanyView companyView)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                if (unitOfWork.CompanyRepository.Get(x => x.Name.ToLower().Equals(companyView.Name.ToLower())).Any())
                    return (int)RegistrationStatus.DuplicateCompany;
                var companyDomain = new Company();
                new Map().ViewToDomain(companyView, companyDomain);
                companyView.RegisterCode = companyDomain.RegisterCode = new Common.Get().RandomNumber().ToString();
                companyDomain.GlobalId = Guid.NewGuid();
                companyDomain.CreatedBy = 0; // User is not created yet so we dont have userId
                companyDomain.IsActive = false; // Admin needs to monitor and  activate
                companyDomain.IsDeleted = false;

                unitOfWork.CompanyRepository.Insert(companyDomain);
                unitOfWork.Save();
                return companyDomain.Id;
            }
        }
        public List<View.Model.CompanyView> GetBy(string name)
        {
            var companyViews = new List<View.Model.CompanyView>();
            using (var unitOfWork = new UnitOfWork())
            {
                var companies = unitOfWork.CompanyRepository.Get(x => x.Name == name).ToList();
                new Map().DomainToView(companyViews, companies);
            }
            return companyViews;
        }
        public View.Model.CompanyView Get(string code)
        {
            var companyView = new View.Model.CompanyView();
            using (var unitOfWork = new UnitOfWork())
            {
                var company = unitOfWork.CompanyRepository.Get(x => x.RegisterCode == code).FirstOrDefault();
                new Map().DomainToView(companyView, company);
            }
            return companyView;
        }
        public View.Model.CompanyView Get(int companyId)
        {
            var companyView = new View.Model.CompanyView();
            using (var unitOfWork = new UnitOfWork())
            {
                var company = unitOfWork.CompanyRepository.GetById(companyId);
                new Map().DomainToView(companyView, company);
            }
            return companyView;
        }
        public void SetCreatedBy(int companyId, int userId)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var company = unitOfWork.CompanyRepository.GetById(companyId);
                company.CreatedBy = userId;
                unitOfWork.CompanyRepository.Update(company);
                unitOfWork.Save();
            }
        }
        public int Update(View.Model.CompanyView obj)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var company = unitOfWork.CompanyRepository.GetById(obj.CompanyId);
                company.Name = obj.Name;
                company.Summary = obj.Summary;
                company.DeltaPunchTime = obj.DeltaPunchTime;
                if (!string.IsNullOrWhiteSpace(obj.LogoUrl))
                {
                    company.LogoBinary = obj.LogoBinary;
                    company.LogoBinary = obj.LogoBinary;
                }
                else
                {
                    company.LogoUrl = company.LogoUrl;
                    company.LogoBinary = company.LogoBinary;
                }
                unitOfWork.CompanyRepository.Update(company);
                unitOfWork.Save();
                obj.CompanyId = company.Id;
            }
            return obj.CompanyId;
        }

        public List<View.Model.EmployeePaidHolidayView> PaidHolidayPkg(int companyId)
        {
            List<View.Model.EmployeePaidHolidayView> employeePaidHolidays;
            using (PunchClockDbContext context = new PunchClockDbContext())
            {
                employeePaidHolidays = (from et in context.EmploymentTypes
                                        join pk in context.EmployeePaidHolidays on et.Id equals pk.EmploymentTypeId into pkGroup
                                        from pkg in pkGroup.DefaultIfEmpty()
                                        join c in context.Companies on pkg.CompanyId equals c.Id into cGroup
                                        from cg in cGroup.DefaultIfEmpty()
                                        where cg.Id == companyId || cg.Id == 0
                                        select new View.Model.EmployeePaidHolidayView
                                        {
                                            CompanyId = cg.Id == 0 ? companyId : cg.Id,
                                            EmploymentTypeId = et.Id,
                                            IsHolidayPaid = pkg.IsHolidayPaid,
                                            EmploymentTypeName = et.Name
                                        }).ToList();
            }
            return employeePaidHolidays;
        }

        public void UpdatePaidHolidayPkg(List<View.Model.EmployeePaidHolidayView> pkgs)
        {
            var compId = pkgs.First().CompanyId;
            using (var unitOfWork = new UnitOfWork())
            {
                //using (var repo = new ComplexTypeRepository<usp_GetCompanyHolidaysForEmployee_Result>(unitOfWork))
                //{
                //    var tmpPkgs = (from ceh in unitOfWork.EmployeePaidHolidayPaidRepository.Get()
                //                   where ceh.CompanyId == compId
                //                   select ceh).ToList();
                //    string deleteSQL = "DELETE  CEHP FROM dbo.CompanyEmployeeHolidayPaid CEHP WHERE CEHP.CompanyId = @companyId";
                //    repo.ExecQuery(deleteSQL, new SqlParameter("@companyId", compId));
                //    //unitOfWork.Save();
                //    foreach (var pkg in pkgs)
                //    {
                //        string insertSQL = "INSERT INTO dbo.CompanyEmployeeHolidayPaid(companyId , EmploymentTypeId , isHolidayPaid) VALUES  (@companyId, @employmentTypeId, @isHolidayPaid)";
                //        repo.ExecQuery(insertSQL,
                //            new SqlParameter("@companyId", compId),
                //            new SqlParameter("@employmentTypeId", pkg.EmploymentTypeId),
                //            new SqlParameter("@isHolidayPaid", pkg.IsHolidayPaid));
                //        //unitOfWork.Save();
                //    }
                //    unitOfWork.Save();
                //}
            }
        }

        public List<View.Model.CompanyHolidayView> CompanyHolidays(int companyId)
        {
            List<View.Model.CompanyHolidayView> companyHolidays;
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                companyHolidays = (from h in unitOfWork.HolidayRepository.Get()
                                   join t in unitOfWork.HolidayTypeHolidayRepository.Get() on h.Id equals t.HolidayId
                                   join ht in unitOfWork.HolidayTypeRepository.Get() on t.TypeId equals ht.Id
                                   from ch in unitOfWork.CompanyHolidayRepository.Get(x => x.HolidayId == h.Id).DefaultIfEmpty()
                                   where ch.CompanyId == companyId || ch.CompanyId == 0
                                   select new View.Model.CompanyHolidayView
                                   {
                                       CompanyId = ch.CompanyId,
                                       HolidayId = h.Id,
                                       HolidayName = h.Name,
                                       HolidayType = ht.Name,
                                       HolidayDate = DbFunctions.CreateDateTime(
                                                           DateTime.Now.Year,
                                                           h.HolidayMonth,
                                                           h.HolidayDay, 0, 0, 0)
                                   }).ToList();
            }
            return companyHolidays;
        }

        public void UpdateCompanyHolidays(List<View.Model.CompanyHolidayView> hlds)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                //using (var repo = new ComplexTypeRepository<usp_GetCompanyHolidaysForEmployee_Result>(unitOfWork))
                //{
                //    if (hlds.Count > 0)
                //    {
                //        hlds = hlds.Distinct().ToList();
                //        int compId = hlds.First().CompanyId;
                //        string deleteSql = " DELETE TCH FROM dbo.tCompanyHoliday AS TCH WHERE TCH.companyId = @CompanyId";
                //        repo.ExecQuery(deleteSql, new SqlParameter("@CompanyId", compId));
                //        //unitOfWork.Save();
                //        string insertSQL = "INSERT	INTO dbo.tCompanyHoliday ( companyId, holidayId ) VALUES  (  @CompanyId, @holidayId )";
                //        foreach (var item in hlds)
                //        {
                //            repo.ExecQuery(insertSQL,
                //                new SqlParameter("@CompanyId", compId), 
                //                new SqlParameter("@holidayId", item.HolidayId));
                //            //unitOfWork.Save();
                //        }
                //    }
                //}
                unitOfWork.Save();
            }
        }

        public List<View.Model.HolidayView> GetCompanyHolidays(int companyId, int userId, DateTime stDate, DateTime enDate)
        {
            List<Holiday> holidays;
            using (PunchClockDbContext context = new PunchClockDbContext())
            {
                holidays = context.GetCompanyHolidays(companyId).ToList();
            }
            if (stDate != DateTime.MinValue)
                holidays = holidays.Where(x => x.HolidayDate >= stDate.Date).ToList();
            if (enDate != DateTime.MinValue)
                holidays = holidays.Where(x => x.HolidayDate <= enDate.Date).ToList();
            var holidayViews = new List<View.Model.HolidayView>();
            new Map().DomainToView(holidayViews, holidays);
            return holidayViews;
        }
    }
}
