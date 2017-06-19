using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PunchClock.Cms.Model
{
   public class CommonEntity
    {
        [Column(TypeName = "datetime2")]
        public DateTime CreatedDate { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? ModifiedDate { get; set; }
        public int LastModifiedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
