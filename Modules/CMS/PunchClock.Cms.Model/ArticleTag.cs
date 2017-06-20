using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PunchClock.Core.Models.Common;
using PunchClock.Language.Model;

namespace PunchClock.Cms.Model
{
    public class ArticleTag:CommonEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public List<ArticleTagResource> Resources { get; set; }
    }
}
