using System.Web;
using PunchClock.Core.Models.Common;
using PunchClock.Helper.Common;

namespace PunchClock.Core.Implementation
{
    public class SessionService
    {
        public UserSession GetCurrentSession(HttpContextBase httpContext)
        {
            UserSession sess = new UserSession
            {
                IpAddress = Geo.GetIpAddress(),
                MacAddress = Geo.GetMac()
            };
            return sess;
        }
    }
}
