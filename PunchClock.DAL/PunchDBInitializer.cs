using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using PunchClock.Domain.Model;

namespace PunchClock.DAL
{
    public class PunchDbInitializer : DbMigrationsConfiguration<PunchClockDbContext>
    {

        public PunchDbInitializer()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
        protected override void Seed(PunchClockDbContext context)
        {
            SeedEmploymentType(context);
            SeedCountry(context);
            SeedHolidayType(context);
            SeedUserType(context);
            SeedHoliday(context);
            SeedState(context);
            base.Seed(context);
        }

        private void SeedState(PunchClockDbContext context)
        {
            List<State> states = new List<State>
                {
                    new State {Id=0,Name="None",Abbreviation="NA",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=1,Name="ALABAMA",Abbreviation="AL",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=2,Name="ALASKA",Abbreviation="AK",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=3,Name="AMERICAN SAMOA",Abbreviation="AS",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=4,Name="ARIZONA",Abbreviation="AZ",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=5,Name="ARKANSAS",Abbreviation="AR",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=6,Name="CALIFORNIA",Abbreviation="CA",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=7,Name="COLORADO",Abbreviation="CO",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=8,Name="CONNECTICUT",Abbreviation="CL",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=9,Name="DELAWARE",Abbreviation="DE",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=10,Name="DISTRICT OF COLUMBIA",Abbreviation="DC",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=11,Name="'FEDERATED STATES OF MICRONESIA",Abbreviation="FM",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=12,Name="FLORIDA",Abbreviation="FL",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=13,Name="GEORGIA",Abbreviation="GA",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=14,Name="'GUAM GU'",Abbreviation="GU",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=15,Name="HAWAII",Abbreviation="HI",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=16,Name="IDAHO",Abbreviation="ID",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=17,Name="ILLINOIS",Abbreviation="IL",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=18,Name="INDIANA",Abbreviation="IN",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=19,Name="IOWA",Abbreviation="IA",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=20,Name="KANSAS",Abbreviation="KS",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=21,Name="KENTUCKY",Abbreviation="KY",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=22,Name="LOUISIANA",Abbreviation="LA",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=23,Name="MAINE",Abbreviation="ME",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=24,Name="MARSHALL ",Abbreviation="MH",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=25,Name="MARYLAND",Abbreviation="MD",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=26,Name="MASSACHUSETTS",Abbreviation="MA",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=27,Name="MICHIGAN",Abbreviation="MI",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=28,Name="MINNESOTA",Abbreviation="MN",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=29,Name="MISSISSIPPI",Abbreviation="MS",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=30,Name="MISSOURI",Abbreviation="MO",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=31,Name="MONTANA",Abbreviation="MT",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=32,Name="NEBRASKA",Abbreviation="NE",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=33,Name="NEVADA",Abbreviation="NV",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=34,Name="NEW HAMPSHIRE",Abbreviation="NH",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=35,Name="NEW JERSEY",Abbreviation="NJ",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=36,Name="NEW MEXICO",Abbreviation="NM",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=37,Name="NEW YORK",Abbreviation="NY",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=38,Name="NORTH CAROLINA",Abbreviation="NC",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=39,Name="NORTH DAKOTA",Abbreviation="ND",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=40,Name="NORTHERN MARIANA ISLANDS",Abbreviation="MP",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=41,Name="OHIO",Abbreviation="OH",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=42,Name="OKLAHOMA",Abbreviation="OK",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=43,Name="OREGON",Abbreviation="OK",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=44,Name="PALAU",Abbreviation="PW",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=45,Name="PENNSYLVANIA",Abbreviation="PA",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=46,Name="'PUERTO RICO'",Abbreviation="PR",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=47,Name="RHODE ISLAND",Abbreviation="RI",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=48,Name="SOUTH CAROLINA",Abbreviation="SC",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=49,Name="SOUTH DAKOTA",Abbreviation="SD",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=50,Name="TENNESSEE",Abbreviation="TN",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=51,Name="TEXAS",Abbreviation="TX",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=52,Name="UTAH",Abbreviation="UT",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=53,Name="VERMONT",Abbreviation="VT",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=54,Name="VIRGIN ISLANDS",Abbreviation="VI",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=55,Name="VIRGINIA",Abbreviation="VA",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=56,Name="WASHINGTON",Abbreviation="WA",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=57,Name="WEST VIRGINIA",Abbreviation="WV",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=58,Name="WISCONSIN",Abbreviation="WI",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=59,Name="WYOMING",Abbreviation="WY",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=60,Name="Armed Forces Africa",Abbreviation="AE",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=61,Name="Armed Forces Americas(except Canada)",Abbreviation="AA",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=62,Name="Armed Forces Canada",Abbreviation="AE",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=63,Name="Armed Forces Europe",Abbreviation="AE",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=64,Name="Armed Forces Middle East",Abbreviation="AE",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=65,Name="Armed Forces Pacific",Abbreviation="AP",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=66,Name="Andhra Pradesh",Abbreviation="AP",CountryId=2,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=67,Name="Arunachal Pradesh",Abbreviation="AR",CountryId=2,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=68,Name="Assam",Abbreviation="AS",CountryId=2,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=69,Name="Bihar",Abbreviation="BR",CountryId=2,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=70,Name="Chhattisgarh",Abbreviation="CG",CountryId=2,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=71,Name="Goa",Abbreviation="GA",CountryId=2,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=72,Name="Gujarat",Abbreviation="GJ",CountryId=2,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=73,Name="Haryana",Abbreviation="HR",CountryId=2,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=74,Name="Himachal Pradesh",Abbreviation="HP",CountryId=2,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=75,Name="Jammu and Kashmir",Abbreviation="JK",CountryId=2,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=76,Name="Jharkhand",Abbreviation="JH",CountryId=2,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=77,Name="Karnataka",Abbreviation="KA",CountryId=2,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=78,Name="Kerala",Abbreviation="KL",CountryId=2,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=79,Name="Madhya Pradesh",Abbreviation="MP",CountryId=2,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=80,Name="Maharashtra",Abbreviation="MH",CountryId=2,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=81,Name="Manipur",Abbreviation="MN",CountryId=2,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=82,Name="Meghalaya",Abbreviation="ML",CountryId=2,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=83,Name="Mizoram",Abbreviation="MZ",CountryId=2,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=84,Name="Nagaland",Abbreviation="NL",CountryId=2,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=85,Name="Orissa",Abbreviation="OR",CountryId=2,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=86,Name="Punjab",Abbreviation="PB",CountryId=2,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=87,Name="Rajasthan",Abbreviation="RJ",CountryId=2,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=88,Name="Sikkim",Abbreviation="SK",CountryId=2,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=89,Name="Tamil Nadu",Abbreviation="TN",CountryId=2,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=90,Name="Tripura",Abbreviation="TR",CountryId=2,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=91,Name="Uttarakhand",Abbreviation="UK",CountryId=2,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=92,Name="Telangana",Abbreviation="TS",CountryId=2,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=93,Name="Uttar Pradesh",Abbreviation="UP",CountryId=2,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=94,Name="West Bengal",Abbreviation="WB",CountryId=2,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=95,Name="Andaman and Nicobar Islands",Abbreviation="AN",CountryId=2,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=96,Name="Chandigarh",Abbreviation="CH",CountryId=2,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=97,Name="Dadra and Nagar Haveli",Abbreviation="DH",CountryId=2,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=98,Name="Daman and Diu",Abbreviation="DD",CountryId=2,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=99,Name="Delhi",Abbreviation="DL",CountryId=2,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=100,Name="Lakshadweep",Abbreviation="LD",CountryId=2,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=101,Name="Pondicherry",Abbreviation="PY",CountryId=2,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=102,Name="Daman and Diu",Abbreviation="DD",CountryId=2,CreatedOnUtc=DateTime.UtcNow},
                    new State {Id=103,Name="DL",Abbreviation="JH",CountryId=2,CreatedOnUtc=DateTime.UtcNow}
                };
            foreach (var state in states)
            {
                // context.States.Add(state);
                context.States.AddOrUpdate(state);
            }

        }

        private void SeedCountry(PunchClockDbContext context)
        {
            List<Country> countries = new List<Country>
            {
                new Country {Id=1,Name="United States",TwoLetterIsoCode="US",ThreeLetterIsoCode="USA",NumericIsoCode="000",Published=1,DisplayOrder=1,CreatedOnUtc=DateTime.UtcNow},
                new Country {Id=2,Name="India",TwoLetterIsoCode="IN",ThreeLetterIsoCode="IND",NumericIsoCode="091",Published=1,DisplayOrder=1,CreatedOnUtc=DateTime.UtcNow}
            };
            foreach (var country in countries)
            {
                //context.Countries.Add(country);
                context.Countries.AddOrUpdate(country);
            }
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
                new Holiday {Id=19,Name="Easter Monday",HolidayMonth=4,HolidayDay=21,HolidayTypeId=4,CountryId=1}
            };
            foreach (var holiday in holidays)
            {
                //  context.Holidays.Add(holiday);
                context.Holidays.AddOrUpdate(holiday);
            }
        }

        private void SeedUserType(PunchClockDbContext context)
        {
            List<UserType> userTypes = new List<UserType>
            {
                new UserType {Id = 1, Description="Employee"},
                new UserType {Id = 2, Description = "Manager"},
                new UserType {Id = 3, Description = "CompanyAdmin"},
                new UserType {Id = 4, Description = "Admin"}
            };
            foreach (var userType in userTypes)
            {
                //context.UserTypes.Add(userType);
                context.UserTypes.AddOrUpdate(userType);
            }
        }

        private void SeedHolidayType(PunchClockDbContext context)
        {
            List<HolidayType> holidayTypes = new List<HolidayType>
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
            foreach (var holidayType in holidayTypes)
            {
                //  context.HolidayTypes.Add(holidayType);
                context.HolidayTypes.AddOrUpdate(holidayType);
            }
        }

        private void SeedEmploymentType(PunchClockDbContext context)
        {
            List<EmploymentType> employementTypes = new List<EmploymentType>
            {
                new EmploymentType {Id = 1, Name = "Full Time"},
                new EmploymentType {Id = 2, Name = "Full Time Contractor"},
                new EmploymentType {Id = 1, Name = "Part Time"}
            };
            foreach (var employmentType in employementTypes)
            {
                // context.EmploymentTypes.Add(employmentType);
                context.EmploymentTypes.AddOrUpdate(employmentType);
            }
        }
    }
}
