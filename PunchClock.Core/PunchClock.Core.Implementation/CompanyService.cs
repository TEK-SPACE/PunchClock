﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.IO;
using System.Linq;
using PunchClock.Configuration.Contract;
using PunchClock.Configuration.Model.Constants;
using PunchClock.Configuration.Service;
using PunchClock.Core.Contracts;
using PunchClock.Core.DataAccess;
using PunchClock.Domain.Model;
using PunchClock.Domain.Model.Enum;
using PunchClock.Model.Mapper;
using PunchClock.View.Model;
using RedandBlue.Common;
using RedandBlue.Common.Logging;

namespace PunchClock.Core.Implementation
{
    public class CompanyService : ICompany
    {
        private readonly IAppSetting _appSettingService;

        public CompanyService()
        {
            _appSettingService = new AppSettingService();
        }
        public int Add(Company company)
        {
            using (var context = new PunchClockDbContext())
            {
                if (context.Companies.Any(x => x.Name.Equals(company.Name, StringComparison.OrdinalIgnoreCase)))
                    return (int) RegistrationStatus.DuplicateCompany;
                //company.RegisterCode = new Helper.Common.Get().RandomNumber().ToString();
                company.IsActive = false; // SuperAdmin needs to monitor and  activate
                context.Companies.Add(company);
                context.SaveChanges();
                return company.Id;
            }
        }

        public List<Company> GetBy(string name)
        {
            using (var context = new PunchClockDbContext())
            {
                return context.Companies.Where(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).ToList();
            }
        }

        public Company Get(string code)
        {
            using (var context = new PunchClockDbContext())
            {
                return context.Companies.FirstOrDefault(
                    x => x.RegisterCode.Equals(code, StringComparison.OrdinalIgnoreCase));
            }
        }

        public Company Get(int companyId)
        {
            using (var context = new PunchClockDbContext())
            {
                return context.Companies.FirstOrDefault(x => x.Id.Equals(companyId));
            }
        }

        public void SetCreatedBy(int companyId, string userId)
        {
            using (var context = new PunchClockDbContext())
            {
                var company = context.Companies.FirstOrDefault(x => x.Id.Equals(companyId));
                if (company != null) company.CreatedById = userId;
                context.SaveChanges();
            }
        }

        public CompanyTransaction Update(Company company)
        {
            using (var context = new PunchClockDbContext())
            {
                var entityToUpdate = context.Companies.FirstOrDefault(x => x.Id == company.Id);
                if (entityToUpdate != null && entityToUpdate.Name == company.Name)
                {
                    entityToUpdate.Name = company.Name;
                }
                else
                {
                    if (context.Companies.Any(x => x.Name.ToLower() == company.Name.ToLower()))
                    {
                        return CompanyTransaction.DuplicateName;
                    }
                    if (entityToUpdate != null) entityToUpdate.Name = company.Name;
                }
                if (entityToUpdate == null) return CompanyTransaction.Success;
                entityToUpdate.Summary = company.Summary;
                entityToUpdate.DeltaPunchTime = company.DeltaPunchTime;
                if (!string.IsNullOrWhiteSpace(company.LogoUrl))
                {
                    entityToUpdate.LogoUrl = company.LogoUrl;
                    entityToUpdate.LogoBinary = company.LogoBinary;
                }
                else
                {
                    entityToUpdate.LogoUrl = entityToUpdate.LogoUrl;
                    entityToUpdate.LogoBinary = entityToUpdate.LogoBinary;
                }
                context.SaveChanges();
                company.Id = entityToUpdate.Id;
            }
            return CompanyTransaction.Success;
        }

        public List<EmployeePaidHolidayView> PaidHolidayPkg(int companyId)
        {
            List<EmployeePaidHolidayView> employeePaidHolidays;
            using (PunchClockDbContext context = new PunchClockDbContext())
            {
                employeePaidHolidays = (from et in context.EmploymentTypes
                    join pk in context.EmployeePaidHolidays on et.Id equals pk.EmploymentTypeId into pkGroup
                    from pkg in pkGroup.DefaultIfEmpty()
                    join c in context.Companies on pkg.CompanyId equals c.Id into cGroup
                    from cg in cGroup.DefaultIfEmpty()
                    where cg.Id == companyId || cg.Id == 0
                    select new EmployeePaidHolidayView
                    {
                        CompanyId = cg.Id == 0 ? companyId : cg.Id,
                        EmploymentTypeId = et.Id,
                        IsHolidayPaid = pkg.IsHolidayPaid,
                        EmploymentTypeName = et.Name
                    }).ToList();
            }
            return employeePaidHolidays;
        }

        public void UpdatePaidHolidayPkg(List<EmployeePaidHolidayView> pkgs)
        {
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

        public List<CompanyHolidayView> CompanyHolidays(int companyId)
        {
            List<CompanyHolidayView> companyHolidays;
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                companyHolidays = (from h in unitOfWork.HolidayRepository.Get()
                    join t in unitOfWork.HolidayTypeHolidayRepository.Get() on h.Id equals t.HolidayId
                    join ht in unitOfWork.HolidayTypeRepository.Get() on t.TypeId equals ht.Id
                    from ch in unitOfWork.CompanyHolidayRepository.Get(x => x.HolidayId == h.Id).DefaultIfEmpty()
                    where ch.CompanyId == companyId || ch.CompanyId == 0
                    select new CompanyHolidayView
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

        public void UpdateCompanyHolidays(List<CompanyHolidayView> hlds)
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

        public List<HolidayView> GetCompanyHolidays(int companyId, int userId, DateTime stDate, DateTime enDate)
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
            var holidayViews = new List<HolidayView>();
            new Map().DomainToView(holidayViews, holidays);
            return holidayViews;
        }

        public List<SiteMap> GetSiteMap(int companyId)
        {
            using (PunchClockDbContext context = new PunchClockDbContext())
            {
                return context.SiteMaps.Include(x=>x.UserAccesses).ToList().Where(x=>x.ParentId == null && (x.IsCoreItem || x.CompanyId == companyId)).ToList();
            }
        }

        public string ComposeRegisteredEmail(CompanyRegister companyRegister)
        {
            var appSettings = _appSettingService.GetByModules((int)ModuleType.Core);

            var templateName = appSettings
                .First(x => x.Key.Equals(AppKey.CoreCompanyRegisteredEmailTemplate, StringComparison.OrdinalIgnoreCase))
                .Value;
            var logoPath = appSettings
                .First(x => x.Key.Equals(AppKey.CoreEmailTemplateLogoPath, StringComparison.OrdinalIgnoreCase))
                .Value;
            var emailTemplatePath = Path.Combine(Util.AssemblyDirectory, "Templates", "Email", templateName);
            if (!File.Exists(emailTemplatePath))
            {
                Log.Error($"Template doesnt't exists at {emailTemplatePath}");
            }
            else
            {
                var emailContent = File.ReadAllText(emailTemplatePath);
                emailContent = emailContent.Replace("#Logo#", logoPath);
                emailContent = emailContent.Replace("#Comapny#", companyRegister.Company.Name);
                emailContent = emailContent.Replace("#RegisterCode#", companyRegister.Company.RegisterCode);
                emailContent = emailContent.Replace("#CreatedBy#", companyRegister.CreatedBy.DisplayName);
                emailContent = emailContent.Replace("#CreatedByEmail#", companyRegister.CreatedBy.Email);
                emailContent = emailContent.Replace("#RegisteredDate#", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                return emailContent;
            }
            return string.Empty;
        }

        public List<EmployeeInvite> Invites(int companyId)
        {
            using (PunchClockDbContext context = new PunchClockDbContext())
            {
                return context.EmployeeInvites.Include(x=>x.UserType).Where(x => x.CompanyId == companyId).ToList();
            }
        }

        public void UpdateInvite(EmployeeInvite invite)
        {
            using (PunchClockDbContext context = new PunchClockDbContext())
            {
                context.EmployeeInvites.AddOrUpdate(invite);
            }
        }

        public string ComposeInviteEmail(EmployeeInvite invite)
        {
            var appSettings = _appSettingService.GetByModules((int)ModuleType.Core);

            var templateName = appSettings
                .First(x => x.Key.Equals(AppKey.CoreCompanyInviteEmployeeEmailTemplate, StringComparison.OrdinalIgnoreCase))
                .Value;
            var logoPath = appSettings
                .First(x => x.Key.Equals(AppKey.CoreEmailTemplateLogoPath, StringComparison.OrdinalIgnoreCase))
                .Value;
            var emailTemplatePath = Path.Combine(Util.AssemblyDirectory, "Templates", "Email", templateName);
            if (!File.Exists(emailTemplatePath))
            {
                Log.Error($"Template doesnt't exists at {emailTemplatePath}");
            }
            else
            {
                var emailContent = File.ReadAllText(emailTemplatePath);
                emailContent = emailContent.Replace("#Logo#", logoPath);
                emailContent = emailContent.Replace("#Name#", invite.Name);
                emailContent = emailContent.Replace("#CreatedBy#", invite.InvitedBy);
                emailContent = emailContent.Replace("#Email#", invite.Email);
                emailContent = emailContent.Replace("#LinkToRegister#", invite.LinkToRegister);
                return emailContent;
            }
            return string.Empty;
        }

        public void DeleteInvite(EmployeeInvite invite)
        {
            using (PunchClockDbContext context = new PunchClockDbContext())
            {
                invite.InviteRevoked = true;
                context.EmployeeInvites.AddOrUpdate(invite);
            }
        }

        public string Invite(EmployeeInvite invite)
        {
            using (PunchClockDbContext context = new PunchClockDbContext())
            {
                invite.GlobalId = Guid.NewGuid().ToString("D");
                invite.UserType = context.UserTypes.FirstOrDefault(x => x.Id == invite.UserTypeId);
                context.EmployeeInvites.AddOrUpdate(invite);
                context.SaveChanges();
                return invite.GlobalId;
            }
        }

        public EmployeeInvite ByInviteKey(string id)
        {
            using (PunchClockDbContext context = new PunchClockDbContext())
            {
                var invite = context.EmployeeInvites.Include(x => x.Company)
                    .FirstOrDefault(x => x.GlobalId == id && !x.InviteRevoked);
                return invite;
            }
        }
    }
}