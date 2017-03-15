using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PunchClock.Domain.Model
{
    public class EmploymentType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }

        public static explicit operator EmploymentType(int v)
        {
            throw new NotImplementedException();
        }
    }
}
