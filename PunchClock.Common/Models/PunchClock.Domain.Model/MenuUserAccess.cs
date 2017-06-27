using System.ComponentModel.DataAnnotations.Schema;

namespace PunchClock.Domain.Model
{
    public class MenuUserAccess
    {
        public int MenuId { get; set; }
        public int UserRoleId { get; set; }
        [ForeignKey("MenuId")]
        public virtual SiteMap Menu { get; set; }
    }
}
