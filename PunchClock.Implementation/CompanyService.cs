using PunchClock.DAL;
using PunchClock.DAL.Models;
using PunchClock.Domain.Model;
using System.Collections.Generic;
using Omu.ValueInjecter;
using System;
using System.Linq;
using System.Data.SqlClient;
using System.Data.Entity;
using PunchClock.Model.Mapper;
using PunchClock.Objects.Core.Enum;
using UserType = PunchClock.Objects.Core.Enum.UserType;

namespace PunchClock.Implementation
{
    public class CompanyService
    {
        public int Add(View.Model.Company companyView)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                if (
                    unitOfWork.UserRepository.Get(x => x.UserName.ToLower() == companyView.User.UserName.ToLower())
                        .Any())
                    return (int) RegistrationStatus.UserNameNotAvailable;
                if (unitOfWork.CompanyRepository.Get(x => x.Name.ToLower().Equals(companyView.Name.ToLower())).Any())
                    return (int) RegistrationStatus.DuplicateCompany;
                var companyDomain = new Domain.Model.Company();
                new Map().ViewToDomain(companyView, companyDomain);
                companyDomain.GlobalId = Guid.NewGuid();
                companyDomain.IsActive = false; // Admin needs to monitor and  activate
                companyDomain.IsDeleted = false;

                unitOfWork.CompanyRepository.Insert(companyDomain);

                companyDomain.User.CompanyId = companyDomain.Id;
                companyDomain.User.UserTypeId = (int) UserType.CompanyAdmin;
                companyDomain.User.PasswordSalt = PasswordService.GenerateSalt();
                companyDomain.User.PasswordHash = PasswordService.EncodePassword(companyView.User.Password,
                    companyView.User.PasswordSalt);

                var userDomain = new Domain.Model.User();
                new Map().ViewToDomain(companyView.User, userDomain);
                unitOfWork.UserRepository.Insert(userDomain);
                unitOfWork.Save();
                return companyDomain.Id;
            }
        }

        public View.Model.Company Details(int companyId)
        {
            var companyView= new View.Model.Company();
            using (var unitOfWork = new UnitOfWork())
            {
                var company = unitOfWork.CompanyRepository.GetById(companyId);
                new Map().DomainToView(companyView, company);
            }
            return companyView;
        }

        public int Update(View.Model.Company obj)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                Company company = new Company();
                company.InjectFrom(obj);
                unitOfWork.CompanyRepository.Insert(company);
            }
            return 0;
        }

        public List<View.Model.EmployeePaidHoliday> PaidHolidayPkg(int companyId)
        {
            List<View.Model.EmployeePaidHoliday> employeePaidHolidays;
            using (PunchClockContext context = new PunchClockContext())
            {
                employeePaidHolidays = (from et in context.EmploymentType
                    join pk in context.EmployeePaidHoliday on et.Id equals pk.EmploymentTypeId into pkGroup
                    from pkg in pkGroup.DefaultIfEmpty()
                    join c in context.Company on pkg.CompanyId equals c.Id into cGroup
                    from cg in cGroup.DefaultIfEmpty()
                    where cg.Id == companyId || cg.Id == 0
                    select new View.Model.EmployeePaidHoliday
                    {
                        CompanyId = cg.Id == 0 ? companyId : cg.Id,
                        EmploymentTypeId = et.Id,
                        IsHolidayPaid = pkg.IsHolidayPaid,
                        EmploymentTypeName = et.Name
                    }).ToList();
            }
            return employeePaidHolidays;
        }

        public void UpdatePaidHolidayPkg(List<View.Model.EmployeePaidHoliday> pkgs)
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

        public List<View.Model.CompanyHoliday> CompanyHolidays(int companyId)
        {
            List<View.Model.CompanyHoliday> companyHolidays;
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                companyHolidays = (from h in unitOfWork.HolidayRepository.Get()
                                      join t in unitOfWork.HolidayTypeHolidayRepository.Get() on h.Id equals t.HolidayId
                                      join ht in unitOfWork.HolidayTypeRepository.Get() on t.TypeId equals ht.Id
                                      from ch in unitOfWork.CompanyHolidayRepository.Get(x => x.HolidayId == h.Id).DefaultIfEmpty()
                                      where ch.CompanyId == companyId || ch.CompanyId == 0
                                      select new View.Model.CompanyHoliday
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

        public void UpdateCompanyHolidays(List<View.Model.CompanyHoliday> hlds)
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

        public List<View.Model.Holiday> GetCompanyHolidays(int companyId, int userId, DateTime stDate, DateTime enDate)
        {
            List<Holiday> holidays;
            using (PunchClockContext context = new PunchClockContext())
            {
                holidays = context.GetCompanyHolidays(companyId).ToList();
            }
            if (stDate != DateTime.MinValue)
                holidays = holidays.Where(x => x.HolidayDate >= stDate.Date).ToList();
            if (enDate != DateTime.MinValue)
                holidays = holidays.Where(x => x.HolidayDate <= enDate.Date).ToList();
            var holidayViews = new List<View.Model.Holiday>();
            new Map().DomainToView(holidayViews, holidays);
            return holidayViews;
        }
    }
}
