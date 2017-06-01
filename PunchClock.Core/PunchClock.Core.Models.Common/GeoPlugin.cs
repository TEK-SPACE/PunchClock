using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace PunchClock.Core.Models.Common
{
     [Serializable]
    public class GeoPlugin
    {
        [Display(Name = "IP")]
        [JsonProperty("geoplugin_request")]
        public string Request { get; set; }
        [JsonProperty("geoplugin_status")]
        public string Status { get; set; }
        [Display(Name = "City")]
        [JsonProperty("geoplugin_city")]
        public string City { get; set; }
        [Display(Name = "State")]
        [JsonProperty("geoplugin_region")]
        public string Region { get; set; }
        [Display(Name = "Area Code")]
        [JsonProperty("geoplugin_areaCode")]
        public string AreaCode { get; set; }
        [JsonProperty("geoplugin_dmaCode")]
        public string DmaCode { get; set; }
        [Display(Name = "Countyry Code")]
        [JsonProperty("geoplugin_countryCode")]
        public string CountryCode { get; set; }
        [Display(Name = "Country")]
        [JsonProperty("geoplugin_continentCode")]
        public string CountryName { get; set; }
        [JsonProperty("")]
        public string ContinentCode { get; set; }
        [Display(Name = "Latitude")]
        [JsonProperty("geoplugin_latitude")]
        public string Latitude { get; set; }
        [Display(Name = "Longitude")]
        [JsonProperty("geoplugin_longitude")]
        public string Longitude { get; set; }
        [JsonProperty("geoplugin_regionCode")]
        public string RegionCode { get; set; }
        [JsonProperty("geoplugin_regionName")]
        public string RegionName { get; set; }
        [Display(Name = "Currency Code")]
        [JsonProperty("geoplugin_currencyCode")]
        public string CurrencyCode { get; set; }
        [JsonProperty("geoplugin_currencySymbol")]
        public string CurrencySymbol { get; set; }
        [JsonProperty("geoplugin_currencySymbol_UTF8")]
        public string CurrencySymbolUtf8 { get; set; }
        [JsonProperty("geoplugin_currencyConverter")]
        public string CurrencyConverter { get; set; }
    }
}
