using PunchClock.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Omu.ValueInjecter;
using PunchClock.Domain.Model;
using PunchClock.Model.Mapper;
using PunchClock.Objects.Core.Enum;
using UserType = PunchClock.Objects.Core.Enum.UserType;

namespace PunchClock.Implementation
{
    public class UserService
    {
        public int Add(View.Model.User userObjLibrary)
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
                userObjLibrary.CompanyId = company.Id;
                if (userObjLibrary.UserTypeId == (int)UserType.Manager)
                    userObjLibrary.IsActive = false;

                var user = new Domain.Model.User();
               new Map().ViewToDomain(userObjLibrary, user);
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
            View.Model.User userObjLibrary = new View.Model.User();
            using (var unitOfWork = new UnitOfWork())
            {
                var user = unitOfWork.UserRepository.Get(x => x.UserName.ToLower() == userName.ToLower()).FirstOrDefault();
                if (user != null)
                {
                    user.PasswordHash = PasswordService.EncodePassword(newPassword, userObjLibrary.PasswordSalt);
                    user.LastActiveMacAddress = macAddress;
                    user.LastActivityIp = ipAddress;
                    unitOfWork.UserRepository.Update(user);
                }
                unitOfWork.Save();
            }
        }

        public View.Model.User Details(string userName)
        {
            View.Model.User userObjLibrary = new View.Model.User();
            using (var unitOfWork = new UnitOfWork())
            {
                var user = unitOfWork.UserRepository.Get(u => u.UserName.ToLower() == userName.ToLower()).SingleOrDefault();
                if (user != null)
                    userObjLibrary.InjectFrom(user);
            }
            return userObjLibrary;
        }

        public View.Model.User Details(int userId)
        {
            View.Model.User userObjLibrary = new View.Model.User ();
            using (var unitOfWork = new UnitOfWork())
            {
                var user = unitOfWork.UserRepository.GetById(userId);
                if (user != null)
                    userObjLibrary.InjectFrom(user);
                if (user != null && user.Id > 0)
                {
                    var punch = unitOfWork.PunchRepository.Get(p => p.UserId == userId);
                    if (punch == null) return userObjLibrary;
                    userObjLibrary.LastPunch = new View.Model.Punch ();
                    new Map().DomainToView(userObjLibrary.LastPunch, punch.Last());
                }
            }
            return userObjLibrary;
        }
        public int Update(View.Model.User userObjLibrary, bool adminUpdate)
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
                    user.LastActiveMacAddress = userObjLibrary.LastActiveMacAddress;
                    user.LastActivityIp = userObjLibrary.LastActivityIp;
                    user.LastActivityDateUtc = DateTime.UtcNow;
                }
                else
                {
                    user.RegisteredTimeZone = userObjLibrary.RegisteredTimeZone;
                    if (userObjLibrary.UserTypeId > 0)
                        user.UserTypeId = userObjLibrary.UserTypeId;
                    if (userObjLibrary.EmploymentTypeId > 0)
                        user.EmploymentType = (Domain.Model.EmploymentType)userObjLibrary.EmploymentTypeId;
                }
                unitOfWork.UserRepository.Update(user);
                unitOfWork.Save();
            }
            return userObjLibrary.UserId;
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

                if (opUserTypeId == (int)UserType.CompanyAdmin || opUserTypeId == (int)UserType.Admin)
                    employees = (from user in users
                                 select new SelectListItem
                                 {
                                     Text = $"{user.FirstName} {user.MiddleName} {user.LastName}",
                                     Value = $"{user.Id}"
                                 }).ToList();
                else
                    employees = (from user in users
                                 where user.UserTypeId != (int)UserType.CompanyAdmin
                                 && user.UserTypeId != (int)UserType.Admin
                                 select new SelectListItem
                                 {
                                     Text = $"{user.FirstName} {user.MiddleName} {user.LastName}",
                                     Value = $"{user.Id}"
                                 }).ToList();
            }
            
            return employees;
        }

        public List<View.Model.User> GetAllUsers(int companyId)
        {
            List<View.Model.User> userObjLibraries;
            using (var unitOfWork = new UnitOfWork())
            {
                var users = unitOfWork.UserRepository.Get(x => x.CompanyId == companyId);
                userObjLibraries = users
                    .Select(x =>
                        new View.Model.User().InjectFrom(x))
                        .Cast<View.Model.User>().ToList();
            }
            return userObjLibraries;
        }
    }
}
