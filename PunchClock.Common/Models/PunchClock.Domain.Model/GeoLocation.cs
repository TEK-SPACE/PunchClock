using System;
using Newtonsoft.Json;

namespace PunchClock.Domain.Model
{
     [Serializable]
     public class GeoLocation
     {
         [JsonProperty(PropertyName = "city")]
         public string City { get; set; }

         [JsonProperty(PropertyName = "country")]
         public string Country { get; set; }

         [JsonProperty(PropertyName = "countryCode")]
         public string CountryCode { get; set; }

         [JsonProperty(PropertyName = "isp")]
         public string Isp { get; set; }

         [JsonProperty(PropertyName = "lat")]
         public float Latitude { get; set; }

         [JsonProperty(PropertyName = "lon")]
         public float Longitude { get; set; }

         [JsonProperty(PropertyName = "region")]
         public string Region { get; set; }

         [JsonProperty(PropertyName = "regionName")]
         public string RegionName { get; set; }

         [JsonProperty(PropertyName = "timezone")]
         public string Timezone { get; set; }

         [JsonProperty(PropertyName = "zip")]
         public string Zip { get; set; }

         [JsonProperty(PropertyName = "query")]
         public string IpAddress { get; set; }
     }
}
