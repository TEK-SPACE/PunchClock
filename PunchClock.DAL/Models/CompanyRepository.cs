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
        private readonly PunchClockDbContext _context;

        public CompanyRepository(UnitOfWork uow)
        {
            _context = uow.Context;
        }

        #region Entity Implementation
        public IQueryable<Company> All => _context.Companies;

        public IQueryable<Company> AllIncluding(params Expression<Func<Company, object>>[] includeProperties)
        {
            IQueryable<Company> query = _context.Companies;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Company Find(int id)
        {
            return _context.Companies.Find(id);
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
                _context.Companies.Add(company);
                //context.Entry(company).States = StateHelper.ConverState(company.States);
                //context.Entry(company.Users.First()).States = StateHelper.ConverState(company.States);
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