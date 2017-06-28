using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PunchClock.Domain.Model
{
    public class SiteMap : CommonEntity 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string RouteObjects { get; set; }
        public string Target { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
        //public bool OnAuthorizationOnly { get; set; } = true;
        //public bool IsCoreItem { get; set; } = false;
        public bool IsMenuItem { get; set; } = true;
        [NotMapped]
        public bool IsUserAccessable { get; set; } = false;

        public int? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public virtual SiteMap Parent { get; set; }
        public List<SiteMap> Children { get; set; } = new List<SiteMap>();
        public virtual ICollection<MenuUserAccess> UserAccesses { get; set; }
        public bool IsCoreItem { get; set; } = true;
    }
}
