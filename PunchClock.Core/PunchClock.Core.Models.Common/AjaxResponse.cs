using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PunchClock.Core.Models.Common
{
    public class AjaxResponse
    {
        public bool Success { get; set; }
        public string ResponseText { get; set; }
        public int ResponseId { get; set; } = 0;
    }
}
