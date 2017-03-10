using PunchClock.Model;
using PunchClock.Objects.Core;

namespace PunchClock.Interface
{
    public interface IEmailRepository : IEntityRepository<User>
    {
        string ComposeContactEmail(ContactOL contact, GeoPluginOL geo);
        bool SendEmail(string msgBody, string msgSubject);
    }
}
