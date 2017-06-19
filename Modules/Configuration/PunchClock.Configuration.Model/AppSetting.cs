using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PunchClock.Core.Models.Common;

namespace PunchClock.Configuration.Model
{
    public class AppSetting
    {
        [Key]
        public int Id { get; set; }
        public Guid GlobalId { get; set; } = Guid.NewGuid();
        public int ModuleId { get; set; }
        [ForeignKey("ModuleId")]
        public virtual Module Module { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public KeyValueType ValueType { get; set; }
        public bool IsPrivate { get; set; }
    }
}
