using PunchClock.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PunchClock.Core.Contracts;
using PunchClock.Domain.Model;
using PunchClock.Domain.Model.Enum;
using UserType = PunchClock.Domain.Model.Enum.UserType;

namespace PunchClock.Core.Implementation
{
    public class UserService : IUserRepository
    {
        [Obsolete("This method is not in use as we are using Identity Service")]
        public int Add(User user)
        {
            user.PasswordSalt = PasswordService.GenerateSalt();
            user.PasswordHash = PasswordService.EncodePassword(user.Password, user.PasswordSalt);
            user.DateCreatedUtc = DateTimeOffset.UtcNow;
            user.LastActivityDateUtc = DateTimeOffset.UtcNow;
            user.LastUpdatedUtc = DateTimeOffset.UtcNow;
            user.PasswordLastChanged = DateTime.UtcNow;
            using (PunchClockDbContext context = new PunchClockDbContext())
            {
                if (context.Users.Any(x => x.UserName.Equals(user.UserName, StringComparison.OrdinalIgnoreCase)))
                    return (int) RegistrationStatus.UserNameNotAvailable;
                var company =
                    context.Companies.FirstOrDefault(
                        x => x.RegisterCode.Equals(user.RegistrationCode, StringComparison.OrdinalIgnoreCase));
                if (company == null)
                    return (int) RegistrationStatus.InvalidRegistrationCode;
                user.CompanyId = company.Id;
                if (user.UserTypeId == (int) UserType.Manager)
                    user.IsActive = false;
                context.Users.Add(user);
                context.SaveChanges();
            }
            return user.Uid;
        }

        private static bool Login(string userName, string password, string ipAddress, string macAddress)
        {
            return PasswordService.ValidatePassword(userName: userName, password: password, ipAddress: ipAddress, macAddress: macAddress) > 0;
        }

        public static bool ChangePassword(string userName, string oldPassword, string newPassword, string macAddress, string ipAddress)
        {
            bool updated = false;
            if (Login(userName: userName, password: oldPassword, ipAddress: ipAddress,macAddress: macAddress))
            {
                UpdatePassword(userName, newPassword, macAddress, ipAddress);
                updated = true;
            }
            return updated;
        }

        private static void UpdatePassword(string userName, string newPassword, string macAddress, string ipAddress)
        {
            using (PunchClockDbContext context = new PunchClockDbContext())
            {
                var user =  context.Users.FirstOrDefault(
                    x => x.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));
                if (user != null)
                {
                    user.PasswordHash = PasswordService.EncodePassword(newPassword, user.PasswordSalt);
                    user.LastActiveMacAddress = macAddress;
                    user.LastActivityIp = ipAddress;
                    context.SaveChanges();
                }
            }
        }

        public User Details(string userName)
        {
            using (PunchClockDbContext context = new PunchClockDbContext())
            {
                return context.Users.FirstOrDefault(
                    x => x.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));
            }
        }

        public User ByGuid(string guid)
        {
            using (PunchClockDbContext context = new PunchClockDbContext())
            {
                return context.Users.FirstOrDefault(
                    x => x.Id.Equals(guid, StringComparison.OrdinalIgnoreCase));
            }
        }

        public void Update(string userId, string password)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var user = unitOfWork.UserRepository.GetById(userId);
                user.PasswordSalt = PasswordService.GenerateSalt();
                user.PasswordHash = PasswordService.EncodePassword(password, user.PasswordSalt);
              
                user.LastActivityDateUtc = DateTimeOffset.UtcNow;
                user.LastUpdatedUtc = DateTimeOffset.UtcNow;
                user.PasswordLastChanged = DateTime.UtcNow;

                unitOfWork.UserRepository.Update(user);
                unitOfWork.Save();
            }
        }

        public User Details(int userId)
        {
            using (PunchClockDbContext context = new PunchClockDbContext())
            {
                return context.Users.FirstOrDefault(
                    x => x.Uid.Equals(userId));
            }
        }

        public List<Domain.Model.UserType> GetTypes()
        {
            using (PunchClockDbContext context = new PunchClockDbContext())
            {
                return context.UserTypes.ToList();
            }
        }

        public User ByEmail(string email)
        {
            using (PunchClockDbContext context = new PunchClockDbContext())
            {
                return context.Users.FirstOrDefault(
                    x => x.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            }
        }

        public int Update(User user, bool adminUpdate)
        {
            using (PunchClockDbContext context = new PunchClockDbContext())
            {
                var u = context.Users.FirstOrDefault(
                    x => x.Uid.Equals(user.Uid));
                if (u == null) return user.Uid;
                u.FirstName = user.FirstName;
                u.LastName = user.LastName;
                u.MiddleName = user.MiddleName;
                u.Email = user.Email;
                u.PhoneNumber = user.PhoneNumber;
                if (!adminUpdate)
                {
                    u.LastActiveMacAddress = user.LastActiveMacAddress;
                    u.LastActivityIp = user.LastActivityIp;
                    u.LastActivityDateUtc = DateTime.UtcNow;
                }
                else
                {
                    u.RegisteredTimeZone = user.RegisteredTimeZone;
                    if (user.UserTypeId > 0)
                        u.UserTypeId = user.UserTypeId;
                    if (user.EmploymentTypeId > 0)
                        u.EmploymentTypeId = user.EmploymentTypeId;
                }
                try
                {
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            return user.Uid;
        }

        public string GetTimeZoneOfUser(int userId)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                User user = unitOfWork.UserRepository.GetById(userId);
                return user.RegisteredTimeZone;
            }
        }

        public List<SelectListItem> GetAllCompanyEmployees(int companyId, int opUserTypeId)
        {
            List<SelectListItem> employees;

            using (var unitOfWork = new UnitOfWork())
            {
                var users = unitOfWork.UserRepository.Get(x => x.CompanyId == companyId);

                if (opUserTypeId == (int)UserType.CompanyAdmin || opUserTypeId == (int)UserType.SuperAdmin)
                    employees = (from user in users
                                 select new SelectListItem
                                 {
                                     Text = $"{user.FirstName} {user.MiddleName} {user.LastName}",
                                     Value = $"{user.Uid}"
                                 }).ToList();
                else
                    employees = (from user in users
                                 where user.UserTypeId != (int)UserType.CompanyAdmin
                                 && user.UserTypeId != (int)UserType.SuperAdmin
                                 select new SelectListItem
                                 {
                                     Text = $"{user.FirstName} {user.MiddleName} {user.LastName}",
                                     Value = $"{user.Id}"
                                 }).ToList();
            }
            
            return employees;
        }

        public List<User> Get(string userName)
        {
            using (PunchClockDbContext context = new PunchClockDbContext())
            {
                return context.Users.Where(
                    x => x.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase)).ToList();
            }
        }

        public List<User> GetAllUsers(int companyId)
        {
            using(PunchClockDbContext context = new PunchClockDbContext())
            {
                return context.Users.Where(
                    x => x.CompanyId.Equals(companyId)).ToList();
            }
        }

        public string SeedPasswordReset(string userId)
        {
            var randomString = RandomString();

            using (var unitOfWork = new UnitOfWork())
            {
                var user = unitOfWork.UserRepository.GetById(userId);
                user.PasswordResetCode = randomString;
                user.PasswordResetValidityTill = DateTime.Now.AddHours(3);
                unitOfWork.UserRepository.Update(user);
                unitOfWork.Save();
            }
            return randomString;
        }

        public string RandomString()
        {
            Random random = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
            var randomString = new string(Enumerable.Repeat(chars, 5)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            return randomString;
        }
        public string RandomNumber()
        {
            Random rnd = new Random();
            return rnd.Next(1, 13).ToString();
        }

        public User DetailsByKey(string userId)
        {
            using (var context = new PunchClockDbContext())
            {
                return context.Users.FirstOrDefault(x => x.Id == userId);
            }
        }

        public User DetailsById(int userId)
        {
            using (var context = new PunchClockDbContext())
            {
                return context.Users.FirstOrDefault(x => x.Uid == userId);
            }
        }

        public List<User> All(int companyId)
        {
            using (var context = new PunchClockDbContext())
            {
                return context.Users.Where(x => x.CompanyId == companyId).ToList();
            }
        }
    }
}
