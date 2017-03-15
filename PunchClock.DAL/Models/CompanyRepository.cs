using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using PunchClock.Domain.Model;
using PunchClock.Interface;
using EntityState = System.Data.Entity.EntityState;

namespace PunchClock.DAL.Models
{ 
    public class CompanyRepository : ICompanyRepository
    {
        private readonly PunchClockContext _context;

        public CompanyRepository(UnitOfWork uow)
        {
            _context = uow.Context;
        }

        #region Entity Implementation
        public IQueryable<Company> All => _context.Company;

        public IQueryable<Company> AllIncluding(params Expression<Func<Company, object>>[] includeProperties)
        {
            IQueryable<Company> query = _context.Company;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Company Find(int id)
        {
            return _context.Company.Find(id);
        }

        public void Insert(Company company)
        {
            _context.Entry(company).State = EntityState.Added;
        }

        public void Update(Company company)
        {
            _context.Entry(company).State = EntityState.Modified;
        }

        public void InsertOrUpdate(Company company)
        {
            if (company.Id == default(int))
            {
                // New entity
                _context.Entry(company).State = EntityState.Added;
            }
            else
            {
                // Existing entity
                _context.Company.Add(company);
                //context.Entry(company).State = StateHelper.ConverState(company.State);
                //context.Entry(company.Users.First()).State = StateHelper.ConverState(company.State);
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
    }
}