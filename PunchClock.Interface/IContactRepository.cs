using PunchClock.Domain.Model;
using PunchClock.Objects.Core;

namespace PunchClock.Interface
{
    public interface IContactRepository : IEntityRepository<User>
    {
        bool Submit(View.Model.ContactView obj, GeoPlugin geo);
    }
}
