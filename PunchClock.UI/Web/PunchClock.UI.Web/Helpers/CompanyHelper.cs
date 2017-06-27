using System;

namespace PunchClock.UI.Web.Helpers
{
    public class CompanyHelper
    {
        public static string NewRegistrationCode()
        {
            return new Random().Next(1, 1000000).ToString("D6");
        }
    }
}