using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using PunchClock.Domain.Model.Constants;

namespace PunchClock.Domain.Model
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        [Display(Name = RegistrationTooltip.State)]
        public int StateId { get; set; }
        [Required]
        [Display(Name = RegistrationTooltip.Country)]
        [DefaultValue(0)]
        public int CountryId { get; set; }
        public string Zip { get; set; }
        public string FullAddress => new Regex("[ ]{2,}", RegexOptions.None).Replace($"{Address1} {Address2}, {City} {State.Abbreviation} {Country.Name} {Zip}"," ");

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("StateId")]
        public virtual State State { get; set; } = new State();
        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; } = new Country();
    }
}
