using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PunchClock.Domain.Model;
using PunchClock.Language.Model;

namespace PunchClock.Cms.Model
{
    [Table("ArticleType")]
  public  class Type:CommonEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } //masterTask
        public string Description { get; set; }
        public int CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }
        public List<ArticleTypeResource> Resources { get; set; }
    }
}
