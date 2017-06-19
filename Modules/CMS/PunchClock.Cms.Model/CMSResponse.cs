namespace PunchClock.Cms.Model
{
   public class CmsResponse
    {
        public bool Success { get; set; }
        public string ResponseText { get; set; }
        public int ResponseId { get; set; } = 0;
    }
}
