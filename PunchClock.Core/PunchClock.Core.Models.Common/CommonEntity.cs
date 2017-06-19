using System;
using System.ComponentModel.DataAnnotations.Schema;
using PunchClock.Domain.Model;

namespace PunchClock.Core.Models.Common
{
    public class CommonEntity
    {
        [Column(TypeName = "datetime2")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Column(TypeName = "datetime2")]
        public DateTime ModifiedDate { get; set; } = DateTime.Now;

        public string LastModifiedBy { get; set; }

        [ForeignKey("LastModifiedBy")]
        public virtual User User { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
