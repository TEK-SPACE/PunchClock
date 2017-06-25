using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PunchClock.Language.Model;
using PunchClock.Domain.Model;

namespace PunchClock.Cms.Model
{
    public  class ArticleType:CommonEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } //masterTask
        public string Description { get; set; }
     
        public List<ArticleTypeResource> Resources { get; set; }
    }
}
