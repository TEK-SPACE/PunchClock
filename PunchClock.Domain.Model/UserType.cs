using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PunchClock.Domain.Model
{
   public class UserType
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
