using PunchClock.Interface;
using PunchClock.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PunchClock.DAL.Models
{
    public class ComplexTypeRepository<T> : IEntityRepository<T>
    {
        private readonly PunchClockDbContext _context;

        public ComplexTypeRepository(UnitOfWork uow)
        {
            _context = uow.Context;
        }

        public IEnumerable<T> ExecQuery(string query, params object[] parameters)
        {
            return _context.Database.SqlQuery<T>(query, parameters);
        }

        public IEnumerable<T> ExecWithStoreProcedure(string query, params object[] parameters)
        {
            return _context.Database.SqlQuery<T>(query, parameters);
        }

        //public IEnumerable<usp_GetCompanyHolidaysForEmployee_Result> usp_GetCompanyHolidaysForEmployee(int companyId, int employeeId)
        //{
        //    if (_context != null) return _context.usp_GetCompanyHolidaysForEmployee(companyId, employeeId).ToList();
        //}
        public List<Holiday> GetCompanyHolidays(int companyId)
        {
            return _context.GetCompanyHolidays(companyId);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        #region Dummy Implementation
        public IQueryable<T> All
        {
            get { throw new NotImplementedException(); }
        }

        public IQueryable<T> AllIncluding(params System.Linq.Expressions.Expression<Func<T, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public T Find(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public void InsertOrUpdate(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        } 
        #endregion
    }
}