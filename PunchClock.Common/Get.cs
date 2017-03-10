using PunchClock.DAL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using PunchClock.Objects.Core.Enum;

namespace PunchClock.Common
{
    public static class Get
    {
        public static List<int> AdminUsers()
        {
            List<int> uList = new List<int>
            {
                (int) UserType.CompanyAdmin,
                (int) UserType.Manager,
                (int) UserType.Admin
            };
            return uList;
        }

        public static int RandomNumber()
        {
            int count = 0;
            Random random = new Random(100000);
            var nexRanNum = random.Next(999999);
            using (var unitOfWork = new UnitOfWork())
            {
                var num = nexRanNum;
                bool isAny = unitOfWork.CompanyRepository.Get(x => x.RegisterCode == num.ToString()).Any();
                while (isAny)
                {
                    count++;
                    nexRanNum = random.Next(999999);
                    if (count > 3)
                        isAny = false;
                }
            }
            return nexRanNum;
        }

        public static List<SelectListItem> UserTypes(bool adminCall = false)
        {
            List<SelectListItem> userTypes = new List<SelectListItem>
            {
                new SelectListItem {Text = "Employee", Value = Convert.ToString((int) UserType.Employee)},
                new SelectListItem {Text = "Manager", Value = Convert.ToString((int) UserType.Manager)}
            };
            if (adminCall)
            {
                userTypes.Add(new SelectListItem { Text = "Company Admin", Value = Convert.ToString((int)UserType.CompanyAdmin) });
                userTypes.Add(new SelectListItem { Text = "Admin", Value = Convert.ToString((int)UserType.Admin) });
            }
            userTypes.Add(new SelectListItem { Text = "Human Resources", Value = Convert.ToString((int)UserType.HumanResources) });

            return userTypes;
        }

        public static List<SelectListItem> EmploymentTypes()
        {
            List<SelectListItem> employmentTypes = new List<SelectListItem>
            {
                new SelectListItem {Text = "FullTime", Value = Convert.ToString((int) EmploymentType.FullTime)},
                new SelectListItem
                {
                    Text = "Contract Hourly",
                    Value = Convert.ToString((int) EmploymentType.ContractHourly)
                },
                new SelectListItem
                {
                    Text = "Contract Flat",
                    Value = Convert.ToString((int) EmploymentType.ContractFlat)
                },
                new SelectListItem {Text = "Part Time", Value = Convert.ToString((int) EmploymentType.PartTime)}
            };
            return employmentTypes;
        }

        public static List<SelectListItem> YearMonths()
        {
            List<SelectListItem> yearMonths = new List<SelectListItem>();
            if (DateTimeFormatInfo.CurrentInfo != null)
            {
                string[] months = DateTimeFormatInfo.CurrentInfo.MonthNames;
                int i = 1;
                foreach (var item in months)
                {
                    yearMonths.Add(new SelectListItem
                    {
                        Text = item + " - "+DateTime.Now.Year,
                        Value = i.ToString()
                    });
                    i++;
                }
            }
            yearMonths.First(x => x.Value == DateTime.Now.Month.ToString()).Selected = true;
            return yearMonths;
        }
    }
}
