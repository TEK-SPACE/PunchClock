using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PunchClock.Domain.Model;

namespace PunchClock.DAL
{
    public class PunchClockContext: DbContext 
    {
        public PunchClockContext() : base("DefaultConnection")
        {
            Database.SetInitializer(new PunchDbInitializer());
        }
        public DbSet<Company> Company { get; set; }
        public DbSet<CompanyHoliday> CompanyHoliday { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<EmployeePaidHoliday> EmployeePaidHoliday { get; set; }
        public DbSet<EmploymentType> EmploymentType { get; set; }
        public DbSet<Holiday> Holiday { get; set; }
        public DbSet<HolidayType> HolidayType { get; set; }
        public DbSet<HolidayTypeHoliday> HolidayTypeHoliday { get; set; }
        public DbSet<PaidHoliday> PaidHoliday { get; set; }
        public DbSet<Punch> Punch { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserType> UserType { get; set; }

        public List<Holiday> GetCompanyHolidays(int companyId)
        {
            return (from h in Holiday
                join ch in CompanyHoliday on h.Id equals ch.HolidayId
                select h).ToList();
        }
    }
}
