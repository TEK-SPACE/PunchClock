using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PunchClock.Domain.Model
{
    public class CompanyLanguage
    {
        [Key]
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int LanguageId { get; set; }
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }
        [ForeignKey("LanguageId")]
        public virtual Language Language { get; set; }

    }
}
