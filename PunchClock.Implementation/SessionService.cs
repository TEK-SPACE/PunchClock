using PunchClock.Common;
using PunchClock.Objects.Core;
using System.Web;

namespace PunchClock.Implementation
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
