using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using PunchClock.Core.Contracts;
using PunchClock.Domain.Model;

namespace PunchClock.Core.Implementation
{
    public class GeoService : IGeo
    {
        public GeoLocation GetUserGeo(string clientIp = null)
        {
            throw new NotImplementedException();
        }

        public string IpAddress(HttpContextBase httpContext)
        {
            throw new NotImplementedException();
        }

        public string MacAddress(HttpContextBase httpContext)
        {
            throw new NotImplementedException();
        }

        public GeoLocation GetGeoLocation(string ipaddress)
        {
            GeoLocation returnObj = new GeoLocation();
            string ipApiUrl = $"http://ip-api.com/json/{ipaddress}";
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = client.GetAsync(ipApiUrl).Result)
            using (HttpContent content = response.Content)
            {
                // ... Read the string.
                string result = content.ReadAsStringAsync().Result;

                // ... Display the result.
                if (result != null)
                {
                    returnObj = JsonConvert.DeserializeObject<GeoLocation>(result);
                }
            }
            return returnObj;
        }
    }
}
