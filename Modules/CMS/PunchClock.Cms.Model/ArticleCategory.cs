using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PunchClock.Domain.Model;

namespace PunchClock.Cms.Model
{
    [Table("ArticleCategory", Schema = "cms")]
    public class ArticleCategory:CommonEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public List<ArticleCategoryResource> Resources { get; set; }
    }
}
