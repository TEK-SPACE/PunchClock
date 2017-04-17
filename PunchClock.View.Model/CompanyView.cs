using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PunchClock.View.Model
{
    public class CompanyView
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CompanyId { get; set; }
        public Guid GlobalId { get; set; }
        [Required]
        [Display(Name = "Company Name")]
        public string Name { get; set; }
        [Display(Name = "Summary")]
        public string Summary { get; set; }
        [Display(Name = "Company Logo")]
        public string LogoUrl { get; set; }
        public byte[] LogoBinary { get; set; }
        [Display(Name = "Delta Time")]
        public int DeltaPunchTime { get; set; }
        [Required]
        [Display(Name = "Register code")]
        public string RegisterCode { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
       
        public UserView User { get; set; }
    }
}
