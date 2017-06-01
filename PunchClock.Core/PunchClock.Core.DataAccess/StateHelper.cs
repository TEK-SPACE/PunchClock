using System.Data.Entity;

namespace PunchClock.Core.DataAccess
{
    public static class StateHelper
    {
        public static EntityState ConverState(EntityState state)
        {
            switch (state)
            {
                case EntityState.Added:
                    return EntityState.Added;
                case EntityState.Deleted:
                    return EntityState.Deleted;
                case EntityState.Modified:
                    return EntityState.Modified;
                default:
                    return EntityState.Unchanged;
            }
        }
    }
}
