using PunchClock.Domain.Model;
using PunchClock.Objects.Core;

namespace PunchClock.Interface
{
    public interface IEmailRepository : IEntityRepository<User>
    {
        string ComposeContactEmail(View.Model.ContactView contact, GeoPlugin geo);
        bool SendEmail(string msgBody, string msgSubject);
    }
}
