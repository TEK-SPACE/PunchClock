using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PunchClock.Domain.Model
{
    public class Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Guid GlobalId { get; set; } = Guid.NewGuid();
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
        public string CreatedById { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        //[ForeignKey("CreatedById")]
        //public User CreatedBy { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<CompanyLanguage> Languages { get; set; }
    }
}
