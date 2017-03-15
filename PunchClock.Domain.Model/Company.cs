using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PunchClock.Domain.Model
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        public Guid GlobalId { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public string LogoUrl { get; set; }
        public byte[] LogoBinary { get; set; }
        public int DeltaPunchTime { get; set; }
        public string RegisterCode { get; set; }
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
