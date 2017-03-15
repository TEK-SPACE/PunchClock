using PunchClock.Domain.Model;
using PunchClock.Objects.Core;
using System.Web;

namespace PunchClock.Interface
{
    public interface IGeoRepository : IEntityRepository<User>
    {
        GeoPlugin GetUserGeo(string clientIp = null);
        
        string IpAddress(HttpContextBase httpContext);

        string MacAddress(HttpContextBase httpContext);
    }
}
