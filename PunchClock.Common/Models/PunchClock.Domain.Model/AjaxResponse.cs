namespace PunchClock.Domain.Model
{
    public class AjaxResponse
    {
        public bool Success { get; set; }
        public string ResponseText { get; set; }
        public int ResponseId { get; set; } = 0;
    }
}
