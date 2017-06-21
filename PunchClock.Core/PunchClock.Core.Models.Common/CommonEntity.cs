using System;
using System.ComponentModel.DataAnnotations.Schema;
using PunchClock.Domain.Model;

namespace PunchClock.Core.Models.Common
{
    public class CommonEntity
    {
        public int CompanyId { get; set; }

        public string CreatedById { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreatedDateUtc { get; set; } = DateTime.UtcNow;

        public string ModifiedById { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime ModifiedDateUtc { get; set; } = DateTime.UtcNow;

        public bool IsDeleted { get; set; }



        [ForeignKey("CreatedById")]
        public virtual User CreatedBy { get; set; }

        [ForeignKey("ModifiedById")]
        public virtual User ModifiedBy { get; set; }

        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }

    }
}
