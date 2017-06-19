using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PunchClock.Core.Models.Common;

namespace PunchClock.Cms.Model
{
    public  class ArticleComment:CommonEntity
    {
        [Key]
        public int Id { get; set; }

        public string Description { get; set; }

        public int ArticleId { get; set; }

        [ForeignKey("ArticleId")]
        public virtual Article Article { get; set; }
    }
}
