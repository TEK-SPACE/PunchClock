namespace PunchClock.Domain.Model
{
    public class CompanyRegister
    {
        public Company Company { get; set; }
        public User CreatedBy { get; set; }
    }
}
