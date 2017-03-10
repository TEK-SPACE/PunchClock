using System;
using System.ComponentModel.DataAnnotations;

namespace PunchClock.Objects.Core
{
     [Serializable]
    public class GeoPluginOL
    {
        [Display(Name = "IP")]
        public string geoplugin_request { get; set; }
        public string geoplugin_status { get; set; }
        [Display(Name = "City")]
        public string geoplugin_city { get; set; }
        [Display(Name = "State")]
        public string geoplugin_region { get; set; }
        [Display(Name = "Area Code")]
        public string geoplugin_areaCode { get; set; }
        public string geoplugin_dmaCode { get; set; }
        [Display(Name = "Countyry Code")]
        public string geoplugin_countryCode { get; set; }
        [Display(Name = "Country")]
        public string geoplugin_countryName { get; set; }
        public string geoplugin_continentCode { get; set; }
        [Display(Name = "Latitude")]
        public string geoplugin_latitude { get; set; }
        [Display(Name = "Longitude")]
        public string geoplugin_longitude { get; set; }
        public string geoplugin_regionCode { get; set; }
        public string geoplugin_regionName { get; set; }
        [Display(Name = "Currency Code")]
        public string geoplugin_currencyCode { get; set; }
        public string geoplugin_currencySymbol { get; set; }
        public string geoplugin_currencySymbol_UTF8 { get; set; }
        public string geoplugin_currencyConverter { get; set; }
    }
}
