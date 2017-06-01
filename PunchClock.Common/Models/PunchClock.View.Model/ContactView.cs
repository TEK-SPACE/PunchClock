using System.ComponentModel.DataAnnotations;

namespace PunchClock.View.Model
{
    public class ContactView
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "{0} is Required")]
        [RegularExpression("[A-z]{2,15}", ErrorMessage = "{0} is Invalid")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "{0} is Required")]
        [RegularExpression(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$", ErrorMessage = "{0} is Invalid")]
        public string Email { get; set; }

        [Display(Name = "Telephone")]
        [Required(ErrorMessage = "{0} is Required")]
        [RegularExpression(@"^\D?(\d{3})\D?\D?(\d{3})\D?(\d{4})$", ErrorMessage = "{0} is Invalid")]
        public string Telephone { get; set; }

        [Display(Name = "Message")]
        //[Required(ErrorMessage = "{0} is Required")]
        //[RegularExpression(@"?<!<[^>]*", ErrorMessage="{0} is Invalid")]
        [StringLength(500, MinimumLength = 4, ErrorMessage = "Min {2}, Max {1} chars")]
        public string Message { get; set; }

        public string Subject { get; set; }
    }
}
