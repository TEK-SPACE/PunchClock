namespace PunchClock.Domain.Model
{
    public class SiteMenu : CommonEntity 
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string Target { get; set; }
        public string Description { get; set; }
    }
}
