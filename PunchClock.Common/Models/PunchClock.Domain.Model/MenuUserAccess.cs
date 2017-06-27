using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PunchClock.Domain.Model
{
    public class MenuUserAccess
    {
        [Key]
        public int Id { get; set; }
        public int MenuId { get; set; }
        public int UserRoleId { get; set; }
        [ForeignKey("MenuId")]
        public virtual SiteMap Menu { get; set; }
    }
}
