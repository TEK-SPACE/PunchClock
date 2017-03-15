using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using PunchClock.Domain.Model;
using PunchClock.Interface;
using EntityState = System.Data.Entity.EntityState;

namespace PunchClock.DAL.Models
{ 
    public class PunchRepository : IPunchRepository
    {
        private readonly PunchClockContext _context;

        public PunchRepository(UnitOfWork uow)
        {
            _context = uow.Context;
        }

        #region Entity Implementation
        public IQueryable<Punch> All => _context.Punch;

        public IQueryable<Punch> AllIncluding(params Expression<Func<Punch, object>>[] includeProperties)
        {
            IQueryable<Punch> query = _context.Punch;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Punch Find(int id)
        {
            return _context.Punch.Find(id);
        }

        public void Insert(Punch punch)
        {
            _context.Entry(punch).State = EntityState.Added;
        }

        public void Update(Punch punch)
        {
            _context.Entry(punch).State = EntityState.Modified;
        }

        /// <summary>
        /// This is not fully implemented. So do not us it
        /// </summary>
        /// <param name="punch"></param>
        public void InsertOrUpdate(Punch punch)
        {
            if (punch.Id == default(int))
            {
                // New entity
                _context.Entry(punch).State = EntityState.Added;
            }
            else
            {
                // Existing entity
                _context.Punch.Add(punch);
                //context.Entry(punch).State = StateHelper.ConverState(punch.State);
            }
        }

        public void Delete(int id)
        {
            var user = _context.User.Find(id);
            if (user != null) _context.User.Remove(user);
        }

        public void Dispose()
        {
            _context.Dispose();
        } 
        #endregion

        //public Punch LastPunch(int userId)
        //{
        //    return context.Punches.Where(x=>x.UserId == userId).OrderByDescending(x=>x.PunchDate).FirstOrDefault();
        //}
    }
}