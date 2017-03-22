using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using PunchClock.Domain.Model;
using System.Linq;

namespace PunchClock.DAL
{
    public class PunchDbInitializer : DbMigrationsConfiguration<PunchClockDbContext>
    {
        private List<Country> _countries;
        private List<HolidayType> _holidayTypes;
        public PunchDbInitializer()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
        protected override void Seed(PunchClockDbContext context)
        {
            SeedEmploymentType(context);
            SeedUserType(context);

            SeedHolidayType(context);
            SeedHoliday(context);
            SeedHolidayTypeHoliday(context);

            SeedCountry(context);
            SeedState(context);

            SeedCompany(context);
            base.Seed(context);
        }

        private void SeedCompany(PunchClockDbContext context)
        {
            List<Company> companies = new List<Company>
            {
                new Company {Id=1,Name="Tekspace",RegisterCode="12345" }
            };
            foreach(var company in companies)
            {
                context.Companies.Add(company);
            }
        }

        private void SeedState(PunchClockDbContext context)
        {
            var countryIdForUs =
                _countries.First(x => x.Name.Equals("United States", StringComparison.OrdinalIgnoreCase)).Id;
            var countryIdForInd = _countries.First(x => x.Name.Equals("India", StringComparison.OrdinalIgnoreCase)).Id;
            List<State> states = new List<State>();
            states.AddRange(ConstructAmericaStates(countryIdForUs));
            states.AddRange(ConstructIndianStates(countryIdForInd));
            foreach (var state in states)
            {
                context.States.AddOrUpdate(state);
            }
        }

        private static List<State> ConstructIndianStates(int countryId)
        {
            return new List<State>
            {

                new State
                {
                    Id = 66,
                    Name = "Andhra Pradesh",
                    Abbreviation = "AP",
                    CountryId = countryId,
                    CreatedOnUtc = DateTime.UtcNow
                },
                new State
                {
                    Id = 67,
                    Name = "Arunachal Pradesh",
                    Abbreviation = "AR",
                    CountryId = countryId,
                    CreatedOnUtc = DateTime.UtcNow
                },
                new State
                {
                    Id = 68,
                    Name = "Assam",
                    Abbreviation = "AS",
                    CountryId = countryId,
                    CreatedOnUtc = DateTime.UtcNow
                },
                new State
                {
                    Id = 69,
                    Name = "Bihar",
                    Abbreviation = "BR",
                    CountryId = countryId,
                    CreatedOnUtc = DateTime.UtcNow
                },
                new State
                {
                    Id = 70,
                    Name = "Chhattisgarh",
                    Abbreviation = "CG",
                    CountryId = countryId,
                    CreatedOnUtc = DateTime.UtcNow
                },
                new State
                {
                    Id = 71,
                    Name = "Goa",
                    Abbreviation = "GA",
                    CountryId = countryId,
                    CreatedOnUtc = DateTime.UtcNow
                },
                new State
                {
                    Id = 72,
                    Name = "Gujarat",
                    Abbreviation = "GJ",
                    CountryId = countryId,
                    CreatedOnUtc = DateTime.UtcNow
                },
                new State
                {
                    Id = 73,
                    Name = "Haryana",
                    Abbreviation = "HR",
                    CountryId = countryId,
                    CreatedOnUtc = DateTime.UtcNow
                },
                new State
                {
                    Id = 74,
                    Name = "Himachal Pradesh",
                    Abbreviation = "HP",
                    CountryId = countryId,
                    CreatedOnUtc = DateTime.UtcNow
                },
                new State
                {
                    Id = 75,
                    Name = "Jammu and Kashmir",
                    Abbreviation = "JK",
                    CountryId = countryId,
                    CreatedOnUtc = DateTime.UtcNow
                },
                new State
                {
                    Id = 76,
                    Name = "Jharkhand",
                    Abbreviation = "JH",
                    CountryId = countryId,
                    CreatedOnUtc = DateTime.UtcNow
                },
                new State
                {
                    Id = 77,
                    Name = "Karnataka",
                    Abbreviation = "KA",
                    CountryId = countryId,
                    CreatedOnUtc = DateTime.UtcNow
                },
                new State
                {
                    Id = 78,
                    Name = "Kerala",
                    Abbreviation = "KL",
                    CountryId = countryId,
                    CreatedOnUtc = DateTime.UtcNow
                },
                new State
                {
                    Id = 79,
                    Name = "Madhya Pradesh",
                    Abbreviation = "MP",
                    CountryId = countryId,
                    CreatedOnUtc = DateTime.UtcNow
                },
                new State
                {
                    Id = 80,
                    Name = "Maharashtra",
                    Abbreviation = "MH",
                    CountryId = countryId,
                    CreatedOnUtc = DateTime.UtcNow
                },
                new State
                {
                    Id = 81,
                    Name = "Manipur",
                    Abbreviation = "MN",
                    CountryId = countryId,
                    CreatedOnUtc = DateTime.UtcNow
                },
                new State
                {
                    Id = 82,
                    Name = "Meghalaya",
                    Abbreviation = "ML",
                    CountryId = countryId,
                    CreatedOnUtc = DateTime.UtcNow
                },
                new State
                {
                    Id = 83,
                    Name = "Mizoram",
                    Abbreviation = "MZ",
                    CountryId = countryId,
                    CreatedOnUtc = DateTime.UtcNow
                },
                new State
                {
                    Id = 84,
                    Name = "Nagaland",
                    Abbreviation = "NL",
                    CountryId = countryId,
                    CreatedOnUtc = DateTime.UtcNow
                },
                new State
                {
                    Id = 85,
                    Name = "Orissa",
                    Abbreviation = "OR",
                    CountryId = countryId,
                    CreatedOnUtc = DateTime.UtcNow
                },
                new State
                {
                    Id = 86,
                    Name = "Punjab",
                    Abbreviation = "PB",
                    CountryId = countryId,
                    CreatedOnUtc = DateTime.UtcNow
                },
                new State
                {
                    Id = 87,
                    Name = "Rajasthan",
                    Abbreviation = "RJ",
                    CountryId = countryId,
                    CreatedOnUtc = DateTime.UtcNow
                },
                new State
                {
                    Id = 88,
                    Name = "Sikkim",
                    Abbreviation = "SK",
                    CountryId = countryId,
                    CreatedOnUtc = DateTime.UtcNow
                },
                new State
                {
                    Id = 89,
                    Name = "Tamil Nadu",
                    Abbreviation = "TN",
                    CountryId = countryId,
                    CreatedOnUtc = DateTime.UtcNow
                },
                new State
                {
                    Id = 90,
                    Name = "Tripura",
                    Abbreviation = "TR",
                    CountryId = countryId,
                    CreatedOnUtc = DateTime.UtcNow
                },
                new State
                {
                    Id = 91,
                    Name = "Uttarakhand",
                    Abbreviation = "UK",
                    CountryId = countryId,
                    CreatedOnUtc = DateTime.UtcNow
                },
                new State
                {
                    Id = 92,
                    Name = "Telangana",
                    Abbreviation = "TS",
                    CountryId = countryId,
                    CreatedOnUtc = DateTime.UtcNow
                },
                new State
                {
                    Id = 93,
                    Name = "Uttar Pradesh",
                    Abbreviation = "UP",
                    CountryId = countryId,
                    CreatedOnUtc = DateTime.UtcNow
                },
                new State
                {
                    Id = 94,
                    Name = "West Bengal",
                    Abbreviation = "WB",
                    CountryId = countryId,
                    CreatedOnUtc = DateTime.UtcNow
                },
                new State
                {
                    Id = 95,
                    Name = "Andaman and Nicobar Islands",
                    Abbreviation = "AN",
                    CountryId = countryId,
                    CreatedOnUtc = DateTime.UtcNow
                },
                new State
                {
                    Id = 96,
                    Name = "Chandigarh",
                    Abbreviation = "CH",
                    CountryId = countryId,
                    CreatedOnUtc = DateTime.UtcNow
                },
                new State
                {
                    Id = 97,
                    Name = "Dadra and Nagar Haveli",
                    Abbreviation = "DH",
                    CountryId = countryId,
                    CreatedOnUtc = DateTime.UtcNow
                },
                new State
                {
                    Id = 98,
                    Name = "Daman and Diu",
                    Abbreviation = "DD",
                    CountryId = countryId,
                    CreatedOnUtc = DateTime.UtcNow
                },
                new State
                {
                    Id = 99,
                    Name = "Delhi",
                    Abbreviation = "DL",
                    CountryId = countryId,
                    CreatedOnUtc = DateTime.UtcNow
                },
                new State
                {
                    Id = 100,
                    Name = "Lakshadweep",
                    Abbreviation = "LD",
                    CountryId = countryId,
                    CreatedOnUtc = DateTime.UtcNow
                },
                new State
                {
                    Id = 101,
                    Name = "Pondicherry",
                    Abbreviation = "PY",
                    CountryId = countryId,
                    CreatedOnUtc = DateTime.UtcNow
                },
                new State
                {
                    Id = 102,
                    Name = "Daman and Diu",
                    Abbreviation = "DD",
                    CountryId = countryId,
                    CreatedOnUtc = DateTime.UtcNow
                },
                new State
                {
                    Id = 103,
                    Name = "DL",
                    Abbreviation = "JH",
                    CountryId = countryId,
                    CreatedOnUtc = DateTime.UtcNow
                }
            };
        }

        private static List<State> ConstructAmericaStates(int countryId)
        {
            return new List<State> {  new State {Id=0,Name="None",Abbreviation="NA",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=1,Name="ALABAMA",Abbreviation="AL",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=2,Name="ALASKA",Abbreviation="AK",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=3,Name="AMERICAN SAMOA",Abbreviation="AS",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=4,Name="ARIZONA",Abbreviation="AZ",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=5,Name="ARKANSAS",Abbreviation="AR",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=6,Name="CALIFORNIA",Abbreviation="CA",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=7,Name="COLORADO",Abbreviation="CO",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=8,Name="CONNECTICUT",Abbreviation="CL",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=9,Name="DELAWARE",Abbreviation="DE",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=10,Name="DISTRICT OF COLUMBIA",Abbreviation="DC",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=11,Name="'FEDERATED STATES OF MICRONESIA",Abbreviation="FM",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=12,Name="FLORIDA",Abbreviation="FL",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=13,Name="GEORGIA",Abbreviation="GA",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=14,Name="'GUAM GU'",Abbreviation="GU",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=15,Name="HAWAII",Abbreviation="HI",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=16,Name="IDAHO",Abbreviation="ID",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=17,Name="ILLINOIS",Abbreviation="IL",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=18,Name="INDIANA",Abbreviation="IN",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=19,Name="IOWA",Abbreviation="IA",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=20,Name="KANSAS",Abbreviation="KS",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=21,Name="KENTUCKY",Abbreviation="KY",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=22,Name="LOUISIANA",Abbreviation="LA",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=23,Name="MAINE",Abbreviation="ME",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=24,Name="MARSHALL ",Abbreviation="MH",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=25,Name="MARYLAND",Abbreviation="MD",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=26,Name="MASSACHUSETTS",Abbreviation="MA",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=27,Name="MICHIGAN",Abbreviation="MI",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=28,Name="MINNESOTA",Abbreviation="MN",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=29,Name="MISSISSIPPI",Abbreviation="MS",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=30,Name="MISSOURI",Abbreviation="MO",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=31,Name="MONTANA",Abbreviation="MT",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=32,Name="NEBRASKA",Abbreviation="NE",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=33,Name="NEVADA",Abbreviation="NV",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=34,Name="NEW HAMPSHIRE",Abbreviation="NH",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=35,Name="NEW JERSEY",Abbreviation="NJ",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=36,Name="NEW MEXICO",Abbreviation="NM",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=37,Name="NEW YORK",Abbreviation="NY",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=38,Name="NORTH CAROLINA",Abbreviation="NC",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=39,Name="NORTH DAKOTA",Abbreviation="ND",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=40,Name="NORTHERN MARIANA ISLANDS",Abbreviation="MP",CountryId=1,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=41,Name="OHIO",Abbreviation="OH",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=42,Name="OKLAHOMA",Abbreviation="OK",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=43,Name="OREGON",Abbreviation="OK",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=44,Name="PALAU",Abbreviation="PW",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=45,Name="PENNSYLVANIA",Abbreviation="PA",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=46,Name="'PUERTO RICO'",Abbreviation="PR",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=47,Name="RHODE ISLAND",Abbreviation="RI",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=48,Name="SOUTH CAROLINA",Abbreviation="SC",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=49,Name="SOUTH DAKOTA",Abbreviation="SD",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=50,Name="TENNESSEE",Abbreviation="TN",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=51,Name="TEXAS",Abbreviation="TX",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=52,Name="UTAH",Abbreviation="UT",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=53,Name="VERMONT",Abbreviation="VT",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=54,Name="VIRGIN ISLANDS",Abbreviation="VI",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=55,Name="VIRGINIA",Abbreviation="VA",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=56,Name="WASHINGTON",Abbreviation="WA",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=57,Name="WEST VIRGINIA",Abbreviation="WV",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=58,Name="WISCONSIN",Abbreviation="WI",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=59,Name="WYOMING",Abbreviation="WY",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=60,Name="Armed Forces Africa",Abbreviation="AE",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=61,Name="Armed Forces Americas(except Canada)",Abbreviation="AA",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=62,Name="Armed Forces Canada",Abbreviation="AE",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=63,Name="Armed Forces Europe",Abbreviation="AE",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=64,Name="Armed Forces Middle East",Abbreviation="AE",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow},
                new State {Id=65,Name="Armed Forces Pacific",Abbreviation="AP",CountryId=countryId,CreatedOnUtc=DateTime.UtcNow}};
        }

        private void SeedCountry(PunchClockDbContext context)
        {
            _countries = new List<Country>
            {
                new Country
                {
                    Id = 1,
                    Name = "United States",
                    TwoLetterIsoCode = "US",
                    ThreeLetterIsoCode = "USA",
                    NumericIsoCode = "000",
                    Published = 1,
                    DisplayOrder = 1,
                    CreatedOnUtc = DateTime.UtcNow
                },
                new Country
                {
                    Id = 2,
                    Name = "India",
                    TwoLetterIsoCode = "IN",
                    ThreeLetterIsoCode = "IND",
                    NumericIsoCode = "091",
                    Published = 1,
                    DisplayOrder = 1,
                    CreatedOnUtc = DateTime.UtcNow
                }
            };
            foreach (var country in _countries)
            {
                context.Countries.AddOrUpdate(country);
            }
        }

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
                context.UserTypes.AddOrUpdate(userType);
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

        private void SeedEmploymentType(PunchClockDbContext context)
        {
            List<EmploymentType> employementTypes = new List<EmploymentType>
            {
                new EmploymentType {Id = 1, Name = "Full Time"},
                new EmploymentType {Id = 2, Name = "Contract Hourly"},
                new EmploymentType {Id = 3, Name = "Contract Flat"},
                new EmploymentType {Id = 4, Name = "Part Time" }
            };
            foreach (var employmentType in employementTypes)
            {
                context.EmploymentTypes.AddOrUpdate(x=>x.Id, employmentType);
            }
        }
    }
}
