using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using PunchClock.Domain.Model;
using PunchClock.Domain.Model.Enum;

namespace PunchClock.Core.DataAccess.Seeders
{
    public partial class PunchDbInitializer
    {
        private void SeedCompany(PunchClockDbContext context)
        {
            List<Company> companies = new List<Company>
            {
               _firstCompany
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
                new CompanyLanguage {Id = 1, CompanyId = _firstCompany.Id, LanguageId = (int)LanguageCulture.English},
                new CompanyLanguage {Id = 2, CompanyId = _firstCompany.Id, LanguageId = (int)LanguageCulture.Spanish},
                new CompanyLanguage {Id = 3, CompanyId = _firstCompany.Id, LanguageId = (int)LanguageCulture.Spanish}
            };
            foreach (var language in languages)
            {
                context.CompanyLanguages.AddOrUpdate(language);
            }
        }
    }
}