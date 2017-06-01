using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PunchClock.Model
{
    public class ObjectWithState
    {
        public EntityState State { get; set; }
    }
    /// <summary>
    /// Entity State
    /// </summary>
    public enum EntityState
    {
        Added,
        Unchanged,
        Modified,
        Deleted
    }
}
