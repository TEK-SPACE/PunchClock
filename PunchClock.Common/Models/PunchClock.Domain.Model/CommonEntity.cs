using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PunchClock.Domain.Model
{
    public class CommonEntity
    {
        [ScaffoldColumn(false)]
        [Display(Name = "Company")]

        public int CompanyId { get; set; }

        [Display(Name = "Created By")]
        [UIHint("UsersDropdownList")]
        //[ScaffoldColumn(false)]
        public string CreatedById { get; set; }

        //[ScaffoldColumn(false)]
        [Column(TypeName = "datetime2")]
        [Display(Name = "Created On")]

        public DateTime CreatedDateUtc { get; set; } = DateTime.UtcNow;

        //[ScaffoldColumn(false)]
        [UIHint("UsersDropdownList")]
        [Display(Name = "Modified By")]
        public string ModifiedById { get; set; }

        //[ScaffoldColumn(false)]
        [Column(TypeName = "datetime2")]
        [Display(Name = "Modified On")]

        public DateTime ModifiedDateUtc { get; set; } = DateTime.UtcNow;

        //[ScaffoldColumn(false)]
        public bool IsDeleted { get; set; }



        [ForeignKey("CreatedById")]
        public virtual User CreatedBy { get; set; }

        [ForeignKey("ModifiedById")]
        public virtual User ModifiedBy { get; set; }

        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }

    }
}
