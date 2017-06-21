using System.Collections.Generic;
using System.Data.Entity.Migrations;
using PunchClock.Core.Models.Common.Enum;
using PunchClock.Domain.Model;

namespace PunchClock.Core.DataAccess.Seeders
{
    public partial class PunchDbInitializer
    {
        private void SeedLanguage(PunchClockDbContext context)
        {
            List<Language> languages = new List<Language>
            {
                new Language {Id=1,Name=LanguageCulture.English.ToString()},
                new Language {Id=2,Name=LanguageCulture.Spanish.ToString()},
                new Language {Id=3,Name=LanguageCulture.Hindi.ToString()}
            };
            foreach (var language in languages)
            {
                context.Languages.AddOrUpdate(language);
            }
        }
    }
}
