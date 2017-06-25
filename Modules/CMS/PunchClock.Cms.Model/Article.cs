using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PunchClock.Domain.Model;

namespace PunchClock.Cms.Model
{
    [Table("Articles", Schema = "cms")]
    public class Article: CommonEntity
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }
        public bool IsPublished { get; set; }
        public string Tag { get; set; }
        [NotMapped]
        public string[] Tags { get; set; }
        public bool IsPrivate { get; set; } = false;
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual ArticleCategory Category { get; set; }
        public virtual List<ArticleComment> Comments { get; set; }
    }
}
