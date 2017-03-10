using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using PunchClock.Model;
using PunchClock.Interface;
using EntityState = System.Data.Entity.EntityState;

namespace PunchClock.DAL.Models
{ 
    public class UserRepository : IUserRepository
    {
        private readonly PunchClockEntities _context;
        
        public UserRepository(UnitOfWork uow)
        {
            _context = uow.Context;
        }

        #region Entity Implementation
        public IQueryable<User> All => _context.Users;

        public IQueryable<User> AllIncluding(params Expression<Func<User, object>>[] includeProperties)
        {
            IQueryable<User> query = _context.Users;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public User Find(int id)
        {
            return _context.Users.Find(id);
        }

        public void Insert(User user)
        {
            user.GlobalId = Guid.NewGuid();
            user.DateCreated_utc = DateTime.UtcNow;
            user.LastActivityDate_utc = DateTime.UtcNow;
            user.IsActive = true;
            user.IsAdmin = null;
            user.IsDeleted = false;
            user.PasswordDisabled = false;
            _context.Entry(user).State = EntityState.Added;
        }

        public void Update(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public void InsertOrUpdate(User user)
        {
            if (user.UserId == default(int))
            {
                // New entity
                _context.Entry(user).State = EntityState.Added;
            }
            else
            {
                // Existing entity
                _context.Users.Add(user);
                _context.Entry(user).State = StateHelper.ConverState(user.State);
                _context.Entry(user.Punches.First()).State = StateHelper.ConverState(user.State);
            }
        }

        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null) _context.Users.Remove(user);
        }

        public void Dispose()
        {
            _context.Dispose();
        } 
        #endregion
    }
}