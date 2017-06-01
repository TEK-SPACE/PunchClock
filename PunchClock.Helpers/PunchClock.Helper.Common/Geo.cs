using System.Net.NetworkInformation;

namespace PunchClock.Helper.Common
{
    public static class Geo
    {
        public static string GetIpAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string sIpAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(sIpAddress))
            {
                return context.Request.ServerVariables["REMOTE_ADDR"];
            }
            else
            {
                string[] ipArray = sIpAddress.Split(',');
                if (ipArray[0].Contains(":"))
                    ipArray[0] = ipArray[0].Split(':')[0];
                return ipArray[0];
            }
        }

        public static string GetMac()
        {
            string macAddresses = "";

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    macAddresses += nic.GetPhysicalAddress().ToString();
                    break;
                }
            }
            return macAddresses;
        }

    }
}
