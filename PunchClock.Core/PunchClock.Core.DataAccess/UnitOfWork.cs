﻿using PunchClock.Core.DataAccess.Models;
using System;
using PunchClock.Domain.Model;

namespace PunchClock.Core.DataAccess
{
    public sealed class UnitOfWork : IDisposable
    {
        private readonly PunchClockDbContext _context;
        public UnitOfWork()
        {
            _context = new PunchClockDbContext();
        }
        public UnitOfWork(PunchClockDbContext context)
        {
            _context = context;
        }

        internal PunchClockDbContext Context => _context;

        private GenericRepository<User> _userRepository;
        private GenericRepository<Company> _companyRepository;
        private GenericRepository<EmploymentType> _employmentTypeRepository;
        private GenericRepository<EmployeePaidHoliday> _employeePaidHoliday;
        private GenericRepository<Holiday> _holidayRepository;
        private GenericRepository<HolidayTypeHoliday> _holidayTypeHolidayRepository;
        private GenericRepository<HolidayType> _holidayTypeRepository;
        private GenericRepository<CompanyHoliday> _companyHolidayRepository;


        public GenericRepository<User> UserRepository
        {
            get
            {
                if (this._userRepository == null)
                {
                    this._userRepository = new GenericRepository<User>(_context);
                }
                return _userRepository;
            }
        }
       
        public GenericRepository<Company> CompanyRepository
        {
            get
            {
                if (this._companyRepository == null)
                {
                    this._companyRepository = new GenericRepository<Company>(_context);
                }
                return _companyRepository;
            }
        }

        public GenericRepository<EmploymentType> EmployeeTypeRepository
        {
            get
            {
                if (this._employmentTypeRepository == null)
                {
                    this._employmentTypeRepository = new GenericRepository<EmploymentType>(_context);
                }
                return _employmentTypeRepository;
            }
        }

        public GenericRepository<EmployeePaidHoliday> EmployeePaidHolidayPaidRepository
        {
            get
            {
                if (this._employeePaidHoliday == null)
                {
                    this._employeePaidHoliday = new GenericRepository<EmployeePaidHoliday>(_context);
                }
                return _employeePaidHoliday;
            }
        }

        public GenericRepository<Holiday> HolidayRepository
        {
            get
            {
                if (this._holidayRepository == null)
                {
                    this._holidayRepository = new GenericRepository<Holiday>(_context);
                }
                return _holidayRepository;
            }
        }

        public GenericRepository<HolidayTypeHoliday> HolidayTypeHolidayRepository
        {
            get
            {
                if (this._holidayTypeHolidayRepository == null)
                {
                    this._holidayTypeHolidayRepository = new GenericRepository<HolidayTypeHoliday>(_context);
                }
                return _holidayTypeHolidayRepository;
            }
        }

        public GenericRepository<HolidayType> HolidayTypeRepository
        {
            get
            {
                if (this._holidayTypeRepository == null)
                {
                    this._holidayTypeRepository = new GenericRepository<HolidayType>(_context);
                }
                return _holidayTypeRepository;
            }
        }

        public GenericRepository<CompanyHoliday> CompanyHolidayRepository
        {
            get
            {
                if (this._companyHolidayRepository == null)
                {
                    this._companyHolidayRepository = new GenericRepository<CompanyHoliday>(_context);
                }
                return _companyHolidayRepository;
            }
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        private bool _disposed;

        private void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            object thisObject = this;
            Dispose(true);
            GC.SuppressFinalize(thisObject);
        }

    }
}
