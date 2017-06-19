using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using PunchClock.Domain.Model;
using System.Linq;

namespace PunchClock.Core.DataAccess.Seeders
{
    public partial class PunchDbInitializer
    {
        private void SeedHolidayTypeHoliday(PunchClockDbContext context)
        {
            var nataionalHolidayTypeId = _holidayTypes.First(x => x.Name.Equals("National Holiday", StringComparison.OrdinalIgnoreCase)).Id;
            var observanceHolidayTypeId = _holidayTypes.First(x => x.Name.Equals("Observance", StringComparison.OrdinalIgnoreCase)).Id;
            var christianHolidayTypeId = _holidayTypes.First(x => x.Name.Equals("Christian Holiday", StringComparison.OrdinalIgnoreCase)).Id;
            //var stateHolidayTypeId = _holidayTypes.First(x => x.Name.Equals("State Holiday", StringComparison.OrdinalIgnoreCase)).Id;
            //var muslimHolidayTypeId = _holidayTypes.First(x => x.Name.Equals("Muslim Holiday", StringComparison.OrdinalIgnoreCase)).Id;
            //var jewishHolidayTypeId = _holidayTypes.First(x => x.Name.Equals("Jewish Holiday", StringComparison.OrdinalIgnoreCase)).Id;
            //var hinduHolidayTypeId = _holidayTypes.First(x => x.Name.Equals("Hindu Holiday", StringComparison.OrdinalIgnoreCase)).Id;
            //var otherHolidayTypeId = _holidayTypes.First(x => x.Name.Equals("Other Holiday", StringComparison.OrdinalIgnoreCase)).Id;
            List<HolidayTypeHoliday> holidayTypeHolidays = new List<HolidayTypeHoliday>();

            holidayTypeHolidays.AddRange(ContructNationalHolidayMapping(nataionalHolidayTypeId));
            holidayTypeHolidays.AddRange(ContructObservanceHolidayMapping(observanceHolidayTypeId));
            holidayTypeHolidays.AddRange(ContructChristianHolidayMapping(christianHolidayTypeId));

            foreach (var holidayTypeHoliday in holidayTypeHolidays)
            {
                context.HolidayTypeHoliday.AddOrUpdate(holidayTypeHoliday);
            }
        }

        private void SeedHolidayType(PunchClockDbContext context)
        {
            _holidayTypes = new List<HolidayType>
            {
                new HolidayType {Id = 1, Name = "National Holiday", DateEnteredUtc = DateTime.UtcNow},
                new HolidayType {Id = 2, Name = "State Holiday", DateEnteredUtc = DateTime.UtcNow},
                new HolidayType {Id = 3, Name = "Observance", DateEnteredUtc = DateTime.UtcNow},
                new HolidayType {Id = 4, Name = "Christian Holiday", DateEnteredUtc = DateTime.UtcNow},
                new HolidayType {Id = 5, Name = "Muslim Holiday", DateEnteredUtc = DateTime.UtcNow},
                new HolidayType {Id = 6,Name = "Jewish Holiday", DateEnteredUtc = DateTime.UtcNow},
                new HolidayType {Id = 7, Name = "Hindu Holiday", DateEnteredUtc = DateTime.UtcNow},
                new HolidayType {Id = 8, Name = "Other Holiday", DateEnteredUtc = DateTime.UtcNow},

            };
            foreach (var holidayType in _holidayTypes)
            {
                context.HolidayTypes.AddOrUpdate(holidayType);
            }
        }
        private static List<HolidayTypeHoliday> ContructChristianHolidayMapping(int christianHolidayTypeId)
        {
            return new List<HolidayTypeHoliday>
            {
                new HolidayTypeHoliday {HolidayId=19, TypeId =christianHolidayTypeId},
                new HolidayTypeHoliday {HolidayId=20, TypeId =christianHolidayTypeId}
            };
        }

        private static List<HolidayTypeHoliday> ContructObservanceHolidayMapping(int observanceHolidayTypeId)
        {
            return new List<HolidayTypeHoliday>
            {
                new HolidayTypeHoliday {HolidayId=11, TypeId =observanceHolidayTypeId},
                new HolidayTypeHoliday {HolidayId=12, TypeId =observanceHolidayTypeId},
                new HolidayTypeHoliday {HolidayId=13, TypeId =observanceHolidayTypeId},
                new HolidayTypeHoliday {HolidayId=14, TypeId =observanceHolidayTypeId},
                new HolidayTypeHoliday {HolidayId=15, TypeId =observanceHolidayTypeId},
                new HolidayTypeHoliday {HolidayId=16, TypeId =observanceHolidayTypeId},
                new HolidayTypeHoliday {HolidayId=17, TypeId =observanceHolidayTypeId},
                new HolidayTypeHoliday {HolidayId=18, TypeId =observanceHolidayTypeId}

            };
        }

        private static List<HolidayTypeHoliday> ContructNationalHolidayMapping(int nataionalHolidayTypeId)
        {
            return new List<HolidayTypeHoliday>
            {
                new HolidayTypeHoliday {HolidayId=1, TypeId =nataionalHolidayTypeId },
                new HolidayTypeHoliday {HolidayId=2, TypeId =nataionalHolidayTypeId },
                new HolidayTypeHoliday {HolidayId=3, TypeId =nataionalHolidayTypeId },
                new HolidayTypeHoliday {HolidayId=4, TypeId =nataionalHolidayTypeId },
                new HolidayTypeHoliday {HolidayId=5, TypeId =nataionalHolidayTypeId },
                new HolidayTypeHoliday {HolidayId=6, TypeId =nataionalHolidayTypeId },
                new HolidayTypeHoliday {HolidayId=7, TypeId =nataionalHolidayTypeId },
                new HolidayTypeHoliday {HolidayId=8, TypeId =nataionalHolidayTypeId },
                new HolidayTypeHoliday {HolidayId=9, TypeId =nataionalHolidayTypeId },
                new HolidayTypeHoliday {HolidayId=10, TypeId =nataionalHolidayTypeId}

            };
        }

        private void SeedHoliday(PunchClockDbContext context)
        {

            List<Holiday> holidays = new List<Holiday>
            {
                new Holiday {Id=1,Name="New Year''s Day",HolidayMonth=1,HolidayDay=1,HolidayTypeId=1,CountryId=1},
                new Holiday {Id=2,Name="Martin Luther King Day",HolidayMonth=1,HolidayDay=20,HolidayTypeId=1,CountryId=1},
                new Holiday {Id=3,Name="'Presidents Day' (Washington's Birthday)",HolidayMonth=2,HolidayDay=17,HolidayTypeId=1,CountryId=1},
                new Holiday {Id=4,Name="Memorial Day",HolidayMonth=5,HolidayDay=26,HolidayTypeId=1,CountryId=1},
                new Holiday {Id=5,Name="Independence Day",HolidayMonth=6,HolidayDay=4,HolidayTypeId=1,CountryId=1},
                new Holiday {Id=6,Name="Labor Day",HolidayMonth=8,HolidayDay=1,HolidayTypeId=1,CountryId=1},
                new Holiday {Id=7,Name="Columbus Day",HolidayMonth=10,HolidayDay=13,HolidayTypeId=1,CountryId=1},
                new Holiday {Id=8,Name="Veterans Day",HolidayMonth=11,HolidayDay=11,HolidayTypeId=1,CountryId=1},
                new Holiday {Id=9,Name="Thanksgiving Day",HolidayMonth=11,HolidayDay=27,HolidayTypeId=1,CountryId=1},
                new Holiday {Id=10,Name="Christmas Day",HolidayMonth=12,HolidayDay=25,HolidayTypeId=1,CountryId=1},
                new Holiday {Id=11,Name="Valentine's DAY",HolidayMonth=2,HolidayDay=14,HolidayTypeId=3,CountryId=1},
                new Holiday {Id=12,Name="Thomas Jefferson's Birthday",HolidayMonth=4,HolidayDay=13,HolidayTypeId=3,CountryId=1},
                new Holiday {Id=13,Name="Easter Sunday",HolidayMonth=4,HolidayDay=20,HolidayTypeId=3,CountryId=1},
                new Holiday {Id=14,Name="Mother's Day",HolidayMonth=5,HolidayDay=11,HolidayTypeId=3,CountryId=1},
                new Holiday {Id=15,Name="Father's Day",HolidayMonth=6,HolidayDay=15,HolidayTypeId=3,CountryId=1},
                new Holiday {Id=16,Name="Halloween",HolidayMonth=10,HolidayDay=31,HolidayTypeId=3,CountryId=1},
                new Holiday {Id=17,Name="Christmas Eve",HolidayMonth=12,HolidayDay=24,HolidayTypeId=3,CountryId=1},
                new Holiday {Id=18,Name="New Year's Eve",HolidayMonth=12,HolidayDay=31,HolidayTypeId=3,CountryId=1},
                new Holiday {Id=19,Name="Good Friday",HolidayMonth=4,HolidayDay=18,HolidayTypeId=4,CountryId=1},
                new Holiday {Id=20,Name="Easter Monday",HolidayMonth=4,HolidayDay=21,HolidayTypeId=4,CountryId=1}
            };
            foreach (var holiday in holidays)
            {
                context.Holidays.AddOrUpdate(holiday);
            }
        }
    }
}