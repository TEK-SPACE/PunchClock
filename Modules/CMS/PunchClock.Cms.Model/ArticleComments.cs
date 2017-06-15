using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
