using System.Collections.Generic;

namespace PunchClock.Interface
{
    public interface IGenericRepository<out TEntity>
    {
        IEnumerable<TEntity> ExecWithStoreProcedure(string query, params object[] parameters);
    }
}
