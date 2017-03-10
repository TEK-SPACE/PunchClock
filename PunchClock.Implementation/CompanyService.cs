using PunchClock.DAL;
using PunchClock.DAL.Models;
using PunchClock.Model;
using PunchClock.Objects.Core;
using System.Collections.Generic;
using Omu.ValueInjecter;
using System;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Data.Entity;
using PunchClock.Objects.Core.Enum;
using UserType = PunchClock.Objects.Core.Enum.UserType;

namespace PunchClock.Implementation
{
    public class CompanyService
    {
        public int Add(CompanyObjLibrary obj)
        {
            Company company = new Company();
            using (var unitOfWork = new UnitOfWork())
            {
                if (unitOfWork.UserRepository.Get(x => x.UserName.ToLower() == obj.User.UserName.ToLower()).Any())
                    return (int)RegistrationStatus.UserNameNotAvailable;
                if (unitOfWork.CompanyRepository.Get(x => x.Name.ToLower().Equals(obj.Name.ToLower())).Any())
                    return (int)RegistrationStatus.DuplicateCompany;

                company.InjectFrom(obj);
                company.GlobalId = Guid.NewGuid();
                company.IsActive = false; // Admin needs to monitor and  activate
                company.IsDeleted = false;

                unitOfWork.CompanyRepository.Insert(company);

                company.User.CompanyId = company.CompanyId;
                company.User.UserTypeId = (int)UserType.CompanyAdmin;
                company.User.PasswordSalt = PasswordService.GenerateSalt();
                company.User.PasswordHash = PasswordService.EncodePassword(obj.User.Password, obj.User.PasswordSalt);
                company.User.InjectFrom(obj.User);
                unitOfWork.UserRepository.Insert(company.User);
                unitOfWork.Save();
            }
            return company.CompanyId;
        }

        public CompanyObjLibrary Details(int companyId)
        {
            var companyDetails = new CompanyObjLibrary();
            using (var unitOfWork = new UnitOfWork())
            {
                var company = unitOfWork.CompanyRepository.GetById(companyId);
                companyDetails.InjectFrom(company);
            }
            return companyDetails;
        }

        public int Update(CompanyObjLibrary obj)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                Company company = new Company();
                company.InjectFrom(obj);
                unitOfWork.CompanyRepository.Insert(company);
            }
            return 0;
        }

        public List<CompanyEmployeeHolidayPaidObjLibrary> PaidHolidayPkg(int companyId)
        {
            List<CompanyEmployeeHolidayPaidObjLibrary> pkgs;
            using (PunchClockEntities context = new PunchClockEntities())
            {
                pkgs = (from et in context.EmploymentTypes
                        join pk in context.CompanyEmployeeHolidayPaids on et.EmploymentTypeId equals pk.EmploymentTypeId into pkGroup
                        from pkg in pkGroup.DefaultIfEmpty()
                        join c in context.Companies on pkg.CompanyId equals c.CompanyId into cGroup
                        from cg in cGroup.DefaultIfEmpty()
                        where cg.CompanyId == companyId || cg.CompanyId == 0
                        select new CompanyEmployeeHolidayPaidObjLibrary
                        {
                            CompanyId = cg.CompanyId == 0 ? companyId : cg.CompanyId,
                            EmploymentTypeId = et.EmploymentTypeId,
                            IsHolidayPaid = pkg.IsHolidayPaid,
                            EmploymentTypeName = et.EmploymentTypeName
                        }).ToList();
            }
            return pkgs;
        }

        public void UpdatePaidHolidayPkg(List<CompanyEmployeeHolidayPaidObjLibrary> pkgs)
        {
            var compId = pkgs.First().CompanyId;
            using (var unitOfWork = new UnitOfWork())
            {
                using (var repo = new ComplexTypeRepository<usp_GetCompanyHolidaysForEmployee_Result>(unitOfWork))
                {
                    var tmpPkgs = (from ceh in unitOfWork.CompanyEmployeeHolidayPaidRepository.Get()
                                   where ceh.CompanyId == compId
                                   select ceh).ToList();
                    string deleteSQL = "DELETE  CEHP FROM dbo.CompanyEmployeeHolidayPaid CEHP WHERE CEHP.CompanyId = @companyId";
                    repo.ExecQuery(deleteSQL, new SqlParameter("@companyId", compId));
                    //unitOfWork.Save();
                    foreach (var pkg in pkgs)
                    {
                        string insertSQL = "INSERT INTO dbo.CompanyEmployeeHolidayPaid(companyId , EmploymentTypeId , isHolidayPaid) VALUES  (@companyId, @employmentTypeId, @isHolidayPaid)";
                        repo.ExecQuery(insertSQL,
                            new SqlParameter("@companyId", compId),
                            new SqlParameter("@employmentTypeId", pkg.EmploymentTypeId),
                            new SqlParameter("@isHolidayPaid", pkg.IsHolidayPaid));
                        //unitOfWork.Save();
                    }
                    unitOfWork.Save();
                }
            }
        }

        public List<CompanyHolidayObjLibrary> CompanyHolidays(int companyId)
        {
            List<CompanyHolidayObjLibrary> companyHolidayList;
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                companyHolidayList = (from h in unitOfWork.HolidayRepository.Get()
                                      join t in unitOfWork.HolidayTypeHolidayRepository.Get() on h.HolidayId equals t.HolidayId
                                      join ht in unitOfWork.HolidayTypeRepository.Get() on t.TypeId equals ht.HolidayTypeId
                                      from ch in unitOfWork.CompanyHolidayRepository.Get(x => x.HolidayId == h.HolidayId).DefaultIfEmpty()
                                      where ch.CompanyId == companyId || ch.CompanyId == 0
                                      select new CompanyHolidayObjLibrary
                                      {
                                          CompanyId = ch.CompanyId,
                                          HolidayId = h.HolidayId,
                                          HolidayName = h.HolidayName,
                                          HolidayType = ht.TypeName,
                                          HolidayDate = DbFunctions.CreateDateTime(
                                                              DateTime.Now.Year, 
                                                              h.HolidayMonth, 
                                                              h.HolidayDate, 0, 0, 0)
                                      }).ToList();
            }
            return companyHolidayList;
        }

        public void UpdateCompanyHolidays(List<CompanyHolidayObjLibrary> hlds)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                using (var repo = new ComplexTypeRepository<usp_GetCompanyHolidaysForEmployee_Result>(unitOfWork))
                {
                    if (hlds.Count > 0)
                    {
                        hlds = hlds.Distinct().ToList();
                        int compId = hlds.First().CompanyId;
                        string deleteSql = " DELETE TCH FROM dbo.tCompanyHoliday AS TCH WHERE TCH.companyId = @CompanyId";
                        repo.ExecQuery(deleteSql, new SqlParameter("@CompanyId", compId));
                        //unitOfWork.Save();
                        string insertSQL = "INSERT	INTO dbo.tCompanyHoliday ( companyId, holidayId ) VALUES  (  @CompanyId, @holidayId )";
                        foreach (var item in hlds)
                        {
                            repo.ExecQuery(insertSQL,
                                new SqlParameter("@CompanyId", compId), 
                                new SqlParameter("@holidayId", item.HolidayId));
                            //unitOfWork.Save();
                        }
                    }
                }
                unitOfWork.Save();
            }
        }

        public List<HolidaysObjLibrary> GetCompanyHolidays(int companyId, int userId, DateTime stDate, DateTime enDate)
        {
            var intime = new TimeSpan(09, 00, 00);
            var outTime = new TimeSpan(17, 00, 00);

            List<HolidaysObjLibrary> holidays;
            using (var repo = new ComplexTypeRepository<usp_GetCompanyHolidaysForEmployee_Result>(new UnitOfWork()))
            {
                var results = repo.ExecWithStoreProcedure(
                      "usp_GetCompanyHolidaysForEmployee @companyId, @EmployeeId",
                         new SqlParameter()
                         {
                             ParameterName = "companyId",
                             SqlDbType = SqlDbType.Int,
                             Direction = ParameterDirection.Input,
                             Value = companyId
                         },
                             new SqlParameter()
                             {
                                 ParameterName = "EmployeeId",
                                 SqlDbType = SqlDbType.Int,
                                 Direction = ParameterDirection.Input,
                                 Value = userId
                             });
                holidays = (from h in results.ToList()
                           select new HolidaysObjLibrary
                           {
                               HolidayDate = h.HolidayDate,
                               HolidayName = h.HolidayName,
                               PunchIn = intime,
                               PunchOut = outTime,
                               Hours = (int)outTime.Subtract(intime).TotalSeconds
                           }).ToList();
            }
            if (stDate != DateTime.MinValue)
                holidays = holidays.Where(x => x.HolidayDate >= stDate.Date).ToList();
            if (enDate != DateTime.MinValue)
                holidays = holidays.Where(x => x.HolidayDate <= enDate.Date).ToList();
            return holidays;
        }
    }
}
