using System;
using System.ComponentModel.DataAnnotations;

namespace PunchClock.Domain.Model
{
    public class Module
    {
        [Key]
        public int Id { get; set; }
        public Guid GlobalId { get; set; } = Guid.NewGuid();
        [StringLength(150)]
        public string Name { get; set; }
        [StringLength(500)]
        public string Description { get; set; }

        public bool IsActive { get; set; } = true;
        public string LicenseKey { get; set; }
        public bool IsEditable { get; set; }
    }
}
