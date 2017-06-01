using System.Collections.Generic;

namespace PunchClock.Core.Contracts
{
    public interface IGenericRepository<out TEntity>
    {
        IEnumerable<TEntity> ExecWithStoreProcedure(string query, params object[] parameters);
    }
}
