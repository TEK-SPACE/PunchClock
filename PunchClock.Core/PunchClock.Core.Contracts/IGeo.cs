using PunchClock.Domain.Model;
using System.Web;

namespace PunchClock.Core.Contracts
{
    public interface IGeo 
    {
        GeoLocation GetUserGeo(string clientIp = null);

        string IpAddress(HttpContextBase httpContext);

        string MacAddress(HttpContextBase httpContext);
        GeoLocation GetGeoLocation(string ipaddress);

    }
}
