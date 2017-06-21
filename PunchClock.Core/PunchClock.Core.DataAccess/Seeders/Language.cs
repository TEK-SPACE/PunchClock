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
            List<Domain.Model.Language> languages = new List<Domain.Model.Language>
            {
                new Domain.Model.Language {Id=1,Name=LanguageCulture.English.ToString()},
                new Domain.Model.Language {Id=2,Name=LanguageCulture.Spanish.ToString()},
                new Domain.Model.Language {Id=3,Name=LanguageCulture.Hindi.ToString()}
            };
            foreach (var language in languages)
            {
                context.Languages.AddOrUpdate(language);
            }
        }
    }
}
