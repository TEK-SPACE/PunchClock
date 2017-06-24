using System.Collections.Generic;
using PunchClock.Domain.Model;

namespace PunchClock.Core.Contracts
{
    public interface IUserRepository //: IEntityRepository<User>
    {
        User DetailsByKey(string userId);
        User DetailsById(int userId);
        User Details(string userName);
        int Update(User user, bool b);
        int Add(User user);
        string SeedPasswordReset(string id);
        User ByGuid(string uid);
        string RandomString();
        string RandomNumber();
        List<UserType> GetTypes();
        User ByEmail(string modelEmail);
        List<User> All(int companyId);
    }
}
