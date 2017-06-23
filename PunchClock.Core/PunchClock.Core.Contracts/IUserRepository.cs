using PunchClock.Domain.Model;
namespace PunchClock.Core.Contracts
{
    public interface IUserRepository //: IEntityRepository<User>
    {
        User DetailsByKey(string userId);
        User DetailsById(int userId);
    }
}