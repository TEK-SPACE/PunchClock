using PunchClock.Domain.Model;
using PunchClock.Core.Models.Common;

namespace PunchClock.Core.Contracts
{
    public interface IContactRepository : IEntityRepository<User>
    {
        bool Submit(View.Model.ContactView obj, GeoPlugin geo);
    }
}
