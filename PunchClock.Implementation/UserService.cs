using PunchClock.DAL;
using PunchClock.Objects.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Omu.ValueInjecter;
using PunchClock.Model;
using PunchClock.Objects.Core.Enum;
using UserType = PunchClock.Objects.Core.Enum.UserType;

namespace PunchClock.Implementation
{
    public class UserService
    {
        public int Add(UserObjLibrary userObjLibrary)
        {
            userObjLibrary.PasswordSalt = PasswordService.GenerateSalt();
            userObjLibrary.PasswordHash = PasswordService.EncodePassword(userObjLibrary.Password, userObjLibrary.PasswordSalt);

            using (var unitOfWork = new UnitOfWork())
            {
                if (unitOfWork.UserRepository.Get(x => x.UserName.ToLower() == userObjLibrary.UserName.ToLower()).Any())
                    return (int)RegistrationStatus.UserNameNotAvailable;

                var company = unitOfWork.CompanyRepository.Get(x => x.RegisterCode.ToLower() == userObjLibrary.RegistrationCode.ToLower()).FirstOrDefault();
                if (company == null)
                    return (int)RegistrationStatus.InvalidRegistrationCode;
                userObjLibrary.CompanyId = company.CompanyId;
                if (userObjLibrary.UserTypeId == (int)UserType.Manager)
                    userObjLibrary.IsActive = false;

                User user = new User();
                user.InjectFrom(userObjLibrary);
                unitOfWork.UserRepository.Insert(user);
                unitOfWork.Save();
            }
            return userObjLibrary.UserId;
        }

        public static bool Login(string userName, string password, string ipAddress, string macAddress)
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
            UserObjLibrary userObjLibrary = new UserObjLibrary();
            using (var unitOfWork = new UnitOfWork())
            {
                var user = unitOfWork.UserRepository.Get(x => x.UserName.ToLower() == userName.ToLower()).FirstOrDefault();
                if (user != null)
                {
                    user.PasswordHash = PasswordService.EncodePassword(newPassword, userObjLibrary.PasswordSalt);
                    user.LastActive_MAC_Address = macAddress;
                    user.LastActivity_IP = ipAddress;
                    unitOfWork.UserRepository.Update(user);
                }
                unitOfWork.Save();
            }
        }

        public UserObjLibrary Details(string userName)
        {
            UserObjLibrary userObjLibrary = new UserObjLibrary();
            using (var unitOfWork = new UnitOfWork())
            {
                var user = unitOfWork.UserRepository.Get(u => u.UserName.ToLower() == userName.ToLower()).SingleOrDefault();
                if (user != null)
                    userObjLibrary.InjectFrom(user);
            }
            return userObjLibrary;
        }

        public UserObjLibrary Details(int userId)
        {
            UserObjLibrary userObjLibrary = new UserObjLibrary();
            using (var unitOfWork = new UnitOfWork())
            {
                var user = unitOfWork.UserRepository.GetById(userId);
                if (user != null)
                    userObjLibrary.InjectFrom(user);
                if (user != null && user.UserId > 0)
                {
                    var punch = unitOfWork.PunchRepository.Get(p => p.UserId == userId);
                    if (punch == null) return userObjLibrary;
                    userObjLibrary.LastPunch = new PunchObjectLibrary();
                    userObjLibrary.LastPunch.InjectFrom(punch);
                }
            }
            return userObjLibrary;
        }
        public int Update(UserObjLibrary userObjLibrary, bool adminUpdate)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                User user = unitOfWork.UserRepository.GetById(userObjLibrary.UserId);
                user.FirstName = userObjLibrary.FirstName;
                user.LastName = userObjLibrary.LastName;
                user.MiddleName = userObjLibrary.MiddleName;
                user.Email = userObjLibrary.Email;
                user.Telephone = userObjLibrary.Telephone;
                if (!adminUpdate)
                {
                    user.LastActive_MAC_Address = userObjLibrary.LastActive_MAC_address;
                    user.LastActivity_IP = userObjLibrary.LastActivity_ip;
                    user.LastActivityDate_utc = DateTime.UtcNow;
                }
                else
                {
                    user.RegisteredTimeZone = userObjLibrary.RegisteredTimeZone;
                    if (userObjLibrary.UserTypeId > 0)
                        user.UserTypeId = userObjLibrary.UserTypeId;
                    if (userObjLibrary.EmploymentType > 0)
                        user.EmploymentType = userObjLibrary.EmploymentType;
                }
                unitOfWork.UserRepository.Update(user);
                unitOfWork.Save();
            }
            return userObjLibrary.UserId;
        }

        public string getTimeZoneOfUser(int userId)
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

                if (opUserTypeId == (int)UserType.CompanyAdmin || opUserTypeId == (int)UserType.Admin)
                    employees = (from e in users
                                 select new SelectListItem
                                 {
                                     Text = $"{e.FirstName} {e.MiddleName} {e.LastName}",
                                     Value = $"{e.UserId}"
                                 }).ToList();
                else
                    employees = (from e in users
                                 where e.UserTypeId != (int)UserType.CompanyAdmin
                                 && e.UserTypeId != (int)UserType.Admin
                                 select new SelectListItem
                                 {
                                     Text = $"{e.FirstName} {e.MiddleName} {e.LastName}",
                                     Value = $"{e.UserId}"
                                 }).ToList();
            }
            
            return employees;
        }

        public List<UserObjLibrary> GetAllUsers(int companyId)
        {
            List<UserObjLibrary> userObjLibraries;
            using (var unitOfWork = new UnitOfWork())
            {
                var users = unitOfWork.UserRepository.Get(x => x.CompanyId == companyId);
                userObjLibraries = users
                    .Select(x =>
                        new UserObjLibrary().InjectFrom(x))
                        .Cast<UserObjLibrary>().ToList();
            }
            return userObjLibraries;
        }
    }
}
