﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PunchClock.Cms.Model
{
  
    public  class ArticleComments:CommonEntity
    {
        [Key]
        public int Id { get; set; }

        public string Description { get; set; }

        public int ArticleId { get; set; }

        [ForeignKey("ArticleId")]
        public virtual Article Article { get; set; }
    }
}
