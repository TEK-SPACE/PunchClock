using PunchClock.Common;
using PunchClock.Objects.Core;
using System.Web;

namespace PunchClock.Implementation
{
    public class SessionService
    {
        public SessionObjLibrary GetCurrentSession(HttpContextBase httpContext)
        {
            SessionObjLibrary sess = new SessionObjLibrary
            {
                IpAddress = Geo.GetIpAddress(),
                MacAddress = Geo.GetMac()
            };
            return sess;
        }
    }
}
