using System.Data.Entity.Migrations;

namespace PunchClock.Core.DataAccess.Seeders
{
    public partial class PunchDbInitializer : DbMigrationsConfiguration<PunchClockDbContext>
    {

        public PunchDbInitializer()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
        protected override void Seed(PunchClockDbContext context)
        {
            #region Employment
            SeedEmploymentType(context);
            #endregion

            #region User
            SeedUserType(context);
            SeedUsers(context);
            #endregion

            #region Holidays
            SeedHolidayType(context);
            SeedHoliday(context);
            SeedHolidayTypeHoliday(context);
            #endregion

            #region Geo
            SeedCountry(context);
            SeedState(context);
            #endregion

            #region Company
            SeedCompany(context);
            SeedCompanyLanguage(context);
            #endregion

            #region CMS


            SeedArticleCategory(context);
            SeedArticleTags(context);
            #endregion

            #region Ticketing

            SeedStatus(context);
            SeedPriority(context);
            SeedTicketType(context);
            SeedTicketCategory(context);
            SeedProjects(context);

            #endregion

            #region Modules
            SeedModules(context);
            #endregion

            #region App Settings
            SeedAppSettings(context);
            #endregion

            SeedMenu(context);

            #region Language Culture
            SeedLanguage(context);
            #endregion


            base.Seed(context);
        }
    }
}
