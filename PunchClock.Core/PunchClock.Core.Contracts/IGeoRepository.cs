﻿using PunchClock.Domain.Model;
using PunchClock.Core.Models.Common;
using System.Web;

namespace PunchClock.Core.Contracts
{
    public interface IGeoRepository : IEntityRepository<User>
    {
        GeoPlugin GetUserGeo(string clientIp = null);
        
        string IpAddress(HttpContextBase httpContext);

        string MacAddress(HttpContextBase httpContext);
    }
}