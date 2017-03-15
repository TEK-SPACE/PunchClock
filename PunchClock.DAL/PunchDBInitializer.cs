using System.Data.Entity;

namespace PunchClock.DAL
{
    public class PunchDbInitializer : DropCreateDatabaseAlways<PunchClockContext>
    {
        protected override void Seed(PunchClockContext context)
        {
            base.Seed(context);
        }
    }
}
