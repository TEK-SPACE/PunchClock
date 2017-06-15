using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PunchClock.Cms.Model
{
   public class CommonEntity
    {
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int LastModifiedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
