using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PunchClock.Domain.Model;

namespace PunchClock.Language.Model
{
    public class BaseResource
    {
        [Key]
        public int Id { get; set; }
        public string Value { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string LastModifiedBy { get; set; }
        [ForeignKey("LastModifiedBy")]
        public virtual User User { get; set; }
    }
}
