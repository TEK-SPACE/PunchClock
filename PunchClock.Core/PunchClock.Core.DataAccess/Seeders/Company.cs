using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using PunchClock.Core.Models.Common.Enum;
using PunchClock.Domain.Model;

namespace PunchClock.Core.DataAccess.Seeders
{
    public partial class PunchDbInitializer
    {
        private void SeedCompany(PunchClockDbContext context)
        {
            List<Company> companies = new List<Company>
            {
                new Company {Id=1,Name="TEKSPACE",RegisterCode="7493582" ,GlobalId=Guid.NewGuid()}
            };
            foreach (var company in companies)
            {
                context.Companies.AddOrUpdate(company);
            }
        }
        private void SeedCompanyLanguage(PunchClockDbContext context)
        {
            List<CompanyLanguage> languages = new List<CompanyLanguage>
            {
                new CompanyLanguage {Id = 1, CompanyId = 1, LanguageId = (int)LanguageCulture.English},
                new CompanyLanguage {Id = 2, CompanyId = 1, LanguageId = (int)LanguageCulture.Spanish},
                new CompanyLanguage {Id = 3, CompanyId = 1, LanguageId = (int)LanguageCulture.Spanish}
            };
            foreach (var language in languages)
            {
                context.CompanyLanguages.AddOrUpdate(language);
            }
        }
    }
}