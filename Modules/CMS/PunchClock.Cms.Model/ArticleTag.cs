using System.ComponentModel.DataAnnotations;

namespace PunchClock.Cms.Model
{
   public class ArticleTag:CommonEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

    }
}
