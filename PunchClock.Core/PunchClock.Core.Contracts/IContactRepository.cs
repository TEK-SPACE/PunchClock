using PunchClock.Domain.Model;

namespace PunchClock.Core.Contracts
{
    public interface IContactRepository : IEntityRepository<User>
    {
        bool Submit(View.Model.ContactView obj, GeoPlugin geo);
    }
}
