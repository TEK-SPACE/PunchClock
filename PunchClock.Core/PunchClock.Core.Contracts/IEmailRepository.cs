using PunchClock.Domain.Model;
using PunchClock.Core.Models.Common;

namespace PunchClock.Core.Contracts
{
    public interface IEmailRepository : IEntityRepository<User>
    {
        string ComposeContactEmail(View.Model.ContactView contact, GeoPlugin geo);
        bool SendEmail(string msgBody, string msgSubject, string [] recipients);
    }
}
