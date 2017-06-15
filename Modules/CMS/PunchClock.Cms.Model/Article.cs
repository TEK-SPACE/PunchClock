using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PunchClock.Cms.Model
{
    public class Article:CommonEntity
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }
        public bool IsPublished { get; set; }
        public string Tags { get; set; }
    
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        public virtual List<ArticleComments> Comments { get; set; }
    }
}
