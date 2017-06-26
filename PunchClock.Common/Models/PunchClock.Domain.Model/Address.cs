using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace PunchClock.Domain.Model
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
        public int CountryId { get; set; }
        public string Zip { get; set; }
        public string FullAddress => new Regex("[ ]{2,}", RegexOptions.None).Replace($"{Address1} {Address2}, {City} {State.Abbreviation} {Country.Name} {Zip}"," ");
        [ForeignKey("StateId")]
        public virtual State State { get; set; }
        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }
    }
}
