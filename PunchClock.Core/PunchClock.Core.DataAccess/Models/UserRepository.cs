using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using PunchClock.Domain.Model;
using PunchClock.Core.Contracts;
using EntityState = System.Data.Entity.EntityState;

namespace PunchClock.Core.DataAccess.Models
{ 
    //public class UserRepository : IUserRepository, IDisposable
    //{
    //    private readonly PunchClockDbContext _context;
        
    //    public UserRepository(UnitOfWork uow)
    //    {
    //        _context = uow.Context;
    //    }

    //    #region Entity Implementation
    //    public IQueryable<User> All => _context.Users;

    //    public IQueryable<User> AllIncluding(params Expression<Func<User, object>>[] includeProperties)
    //    {
    //        IQueryable<User> query = _context.Users;
    //        foreach (var includeProperty in includeProperties)
    //        {
    //            query = query.Include(includeProperty);
    //        }
    //        return query;
    //    }

    //    public User Find(int id)
    //    {
    //        return _context.Users.Find(id);
    //    }

    //    public void Insert(User user)
    //    {
    //        user.DateCreatedUtc = DateTime.UtcNow;
    //        user.LastActivityDateUtc = DateTime.UtcNow;
    //        user.IsActive = true;
    //        user.IsAdmin = null;
    //        user.IsDeleted = false;
    //        user.PasswordDisabled = false;
    //        _context.Entry(user).State = EntityState.Added;
    //    }

    //    public void Update(User user)
    //    {
    //        _context.Entry(user).State = EntityState.Modified;
    //    }

    //    public void InsertOrUpdate(User user)
    //    {
    //        if (user.Id == default(string))
    //        {
    //            // New entity
    //            _context.Entry(user).State = EntityState.Added;
    //        }
    //        else
    //        {
    //            // Existing entity
    //            _context.Users.Add(user);
    //            //_context.Entry(user).States = StateHelper.ConverState(user.States);
    //            //_context.Entry(user.Punches.First()).States = StateHelper.ConverState(user.States);
    //        }
    //    }

    //    public void Delete(int id)
    //    {
    //        var user = _context.Users.Find(id);
    //        if (user != null) _context.Users.Remove(user);
    //    }

    //    public void Dispose()
    //    {
    //        _context.Dispose();
    //    } 
    //    #endregion

    //    public User DetailsByKey(string userId)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public User DetailsById(int userId)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}