using PunchClock.DAL.Models;
using PunchClock.Model;
using System;

namespace PunchClock.DAL
{
    public sealed class UnitOfWork : IDisposable
    {
        private readonly PunchClockEntities _context;
        public UnitOfWork()
        {
            _context = new PunchClockEntities();
        }
        public UnitOfWork(PunchClockEntities context)
        {
            _context = context;
        }

        internal PunchClockEntities Context => _context;

        private GenericRepository<User> _userRepository;
        private GenericRepository<Punch> _punchRepository;
        private GenericRepository<Company> _companyRepository;
        private GenericRepository<EmploymentType> _employmentTypeRepository;
        private GenericRepository<CompanyEmployeeHolidayPaid> _companyEmployeeHolidayPaidsRepository;
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
        public GenericRepository<Punch> PunchRepository
        {
            get
            {
                if (this._punchRepository == null)
                {
                    this._punchRepository = new GenericRepository<Punch>(_context);
                }
                return _punchRepository;
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

        public GenericRepository<CompanyEmployeeHolidayPaid> CompanyEmployeeHolidayPaidRepository
        {
            get
            {
                if (this._companyEmployeeHolidayPaidsRepository == null)
                {
                    this._companyEmployeeHolidayPaidsRepository = new GenericRepository<CompanyEmployeeHolidayPaid>(_context);
                }
                return _companyEmployeeHolidayPaidsRepository;
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
