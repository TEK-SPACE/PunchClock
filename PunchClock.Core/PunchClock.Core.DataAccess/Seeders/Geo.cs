using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using PunchClock.Domain.Model;
using System.Linq;

namespace PunchClock.Core.DataAccess.Seeders
{
    public partial class PunchDbInitializer
    {
        private List<Country> _countries;
        private List<HolidayType> _holidayTypes;

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
    }
}