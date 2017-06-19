using System.ComponentModel.DataAnnotations;

namespace PunchClock.Cms.Model
{
  public  class ArticleType:CommonEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
     
    }
}
