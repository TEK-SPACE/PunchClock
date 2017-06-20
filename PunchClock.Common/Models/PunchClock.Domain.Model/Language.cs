using System.ComponentModel.DataAnnotations;

namespace PunchClock.Domain.Model
{
    public class Language
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
