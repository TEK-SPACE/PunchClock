using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PunchClock.Domain.Model
{
    public class State
    {
        [Key]
        public int Id { get; set; }
        public Guid StateGuid { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public string Abbreviation { get; set; }
        public bool IsPublished { get; set; }
        public int DisplayOrder { get; set; }
        public string ExtensionData { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime CreatedOnUtc { get; set; }
    }
}
