using System.Collections.Generic;

namespace PunchClock.Interface
{
    public interface IGenericRepository<T>
    {
        IEnumerable<T> ExecWithStoreProcedure(string query, params object[] parameters);
    }
}
