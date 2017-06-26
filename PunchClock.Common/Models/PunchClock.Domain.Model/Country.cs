using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PunchClock.Domain.Model
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        public Guid CountryGuid { get; set; }
        public string Name { get; set; }
        public string TwoLetterIsoCode { get; set; }
        public string ThreeLetterIsoCode { get; set; }
        public string NumericIsoCode { get; set; }
        public byte Published { get; set; }
        public int DisplayOrder { get; set; }
        public string ExtensionData { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime CreatedOnUtc { get; set; }

        public virtual ICollection<Holiday> Holidays { get; set; }
    }
}
