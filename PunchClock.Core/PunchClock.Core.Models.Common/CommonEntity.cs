using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PunchClock.Domain.Model;

namespace PunchClock.Core.Models.Common
{
    public class CommonEntity
    {
        [ScaffoldColumn(false)]
        public int CompanyId { get; set; }

        [ScaffoldColumn(false)]
        public string CreatedById { get; set; }

        [ScaffoldColumn(false)]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedDateUtc { get; set; } = DateTime.UtcNow;

        [ScaffoldColumn(false)]
        public string ModifiedById { get; set; }

        [ScaffoldColumn(false)]
        [Column(TypeName = "datetime2")]
        public DateTime ModifiedDateUtc { get; set; } = DateTime.UtcNow;

        [ScaffoldColumn(false)]
        public bool IsDeleted { get; set; }



        [ForeignKey("CreatedById")]
        public virtual User CreatedBy { get; set; }

        [ForeignKey("ModifiedById")]
        public virtual User ModifiedBy { get; set; }

        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }

    }
}
