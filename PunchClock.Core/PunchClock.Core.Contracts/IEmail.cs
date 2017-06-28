using PunchClock.Domain.Model;

namespace PunchClock.Core.Contracts
{
    public interface IEmail //: IEntityRepository<User>
    {
        string ComposeContactEmail(View.Model.ContactView contact, GeoLocation geo);
        bool SendEmail(string msgBody, string msgSubject, string [] recipients, bool includeGeo = false);
    }
}
