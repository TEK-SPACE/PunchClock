using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;
using PunchClock.Cms.Model;
using PunchClock.Configuration.Model;
using PunchClock.Domain.Model;
using PunchClock.Ticketing.Model;
using PunchClock.TimeTracker.Model;

namespace PunchClock.Core.DataAccess
{
    public class PunchClockDbContext: IdentityDbContext<User>, IDisposable
    {
        public PunchClockDbContext() : base("DefaultConnection")
        {
            //Database.SetInitializer(new PunchDbInitializer());
            //Database.SetInitializer(new CreateDatabaseIfNotExists<PunchClockDbContext>());
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = false;
        }
        public static PunchClockDbContext Create()
        {
            return new PunchClockDbContext();
        }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyHoliday> CompanyHolidays { get; set; }
        public DbSet<Domain.Model.Language> Languages { get; set; }
        public DbSet<CompanyLanguage> CompanyLanguages { get; set; }

        public DbSet<Country> Countries { get; set; }
        public DbSet<EmployeePaidHoliday> EmployeePaidHolidays { get; set; }
        public DbSet<EmploymentType> EmploymentTypes { get; set; }
        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<HolidayType> HolidayTypes { get; set; }
        public DbSet<HolidayTypeHoliday> HolidayTypeHoliday { get; set; }
        public DbSet<PaidHoliday> PaidHolidays { get; set; }
        public DbSet<Punch> Punches { get; set; }
        public DbSet<State> States { get; set; }
        //public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }

        public DbSet<Address> Addresses { get; set; }

        #region Tickeitng Module

        public DbSet<TicketType> TicketTypes { get; set; }
        public DbSet<TicketAttachment> TicketAttachments { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketCategory> TicketCategories { get; set; }
        public DbSet<TicketComment> TicketComments { get; set; }
        public DbSet<TicketPriority> TicketPriorities { get; set; }
        public DbSet<TicketStatus> TicketStatuses { get; set; }
        public DbSet<TicketProject> TicketProjects { get; set; }

        #region Resources
        public DbSet<TicketCategoryResource> TicketCategoryResources { get; set; }
        public DbSet<TicketPriorityResource> TicketPriorityResources { get; set; }
        public DbSet<TicketTypeResource> TicketTypeResources { get; set; }
        public DbSet<TicketStatusResource> TicketStatusResources { get; set; }

        #endregion

        #endregion

        #region Article/CMS
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleCategory> ArticleCategrories { get; set; }
        public DbSet<ArticleComment> ArticleComments { get; set; }
        public DbSet<ArticleTag> ArticleTags { get; set; }
        public DbSet<ArticleType> ArticleTypes { get; set; }

        #region Resources
        public DbSet<ArticleTypeResource> ArticleTypeResources { get; set; }
        public DbSet<ArticleTagResource> ArticleTagResources { get; set; }
        public DbSet<ArticleCategoryResource> ArticleCategoryResources { get; set; }
        #endregion

        #endregion

        public DbSet<Module> Modules { get; set; }
        public DbSet<AppSetting> AppSettings { get; set; }

        public DbSet<SiteMap> SiteMenus { get; set; }
        public DbSet<MenuUserAccess> MenuUserAccess { get; set; }

        public List<Holiday> GetCompanyHolidays(int companyId)
        {
            return (from h in Holidays
                join ch in CompanyHolidays on h.Id equals ch.HolidayId
                where ch.CompanyId == companyId
                select h).ToList();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<IdentityUser>().ToTable("Users", "dbo");
            modelBuilder.Entity<User>().ToTable("Users", "dbo").Property(x=>x.Uid).HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("IX_User_Uid") {IsUnique = true}
                }));
            modelBuilder.Entity<IdentityRole>().ToTable("Roles", "dbo");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles", "dbo");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims", "dbo");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins", "dbo");

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
