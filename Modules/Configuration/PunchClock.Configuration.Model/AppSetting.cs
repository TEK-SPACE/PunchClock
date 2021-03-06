﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PunchClock.Domain.Model;

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
        public bool IsDeleted { get; set; } = false;
        public bool IsEditable { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
}
