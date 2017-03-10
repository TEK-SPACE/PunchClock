using PunchClock.Model;
using PunchClock.Objects.Core;

namespace PunchClock.Interface
{
    public interface IContactRepository : IEntityRepository<User>
    {
        bool Submit(ContactOL obj, GeoPluginOL geo);
    }
}
