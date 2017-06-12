using PunchClock.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Omu.ValueInjecter;
using PunchClock.Domain.Model;
using PunchClock.Model.Mapper;
using PunchClock.Core.Models.Common.Enum;
using UserType = PunchClock.Core.Models.Common.Enum.UserType;

namespace PunchClock.Core.Implementation
{
    public class UserService
    {
        public int Add(View.Model.UserView userView)
        {
            userView.PasswordSalt = PasswordService.GenerateSalt();
            userView.PasswordHash = PasswordService.EncodePassword(userView.Password, userView.PasswordSalt);
            userView.DateCreatedUtc = DateTimeOffset.UtcNow;
            userView.LastActivityDateUtc = DateTimeOffset.UtcNow;
            userView.LastUpdatedUtc = DateTimeOffset.UtcNow;
            userView.PasswordLastChanged = DateTime.UtcNow;
            using (var unitOfWork = new UnitOfWork())
            {
                if (unitOfWork.UserRepository.Get(x => x.UserName.ToLower() == userView.UserName.ToLower()).Any())
                    return (int) RegistrationStatus.UserNameNotAvailable;

                var company =
                    unitOfWork.CompanyRepository.Get(
                        x => x.RegisterCode.ToLower() == userView.RegistrationCode.ToLower()).FirstOrDefault();
                if (company == null)
                    return (int) RegistrationStatus.InvalidRegistrationCode;

                userView.CompanyId = company.Id;

                if (userView.UserTypeId == (int) UserType.Manager)
                    userView.IsActive = false;

                var user = new User();
                new Map().ViewToDomain(userView, user);
                unitOfWork.UserRepository.Insert(user);
                unitOfWork.Save();
                userView.UserId = user.Uid;
            }
            return userView.UserId;
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
            View.Model.UserView userObjLibrary = new View.Model.UserView();
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

        //public View.Model.UserView Details1(string guid)
        //{
        //    View.Model.UserView userView = new View.Model.UserView();
        //    using (var unitOfWork = new UnitOfWork())
        //    {
        //        var user = unitOfWork.UserRepository.Get(u => u.Id == guid).SingleOrDefault();
        //        new Map().DomainToView(userView, user);
        //    }
        //    return userView;
        //}
        public View.Model.UserView Details(string userName)
        {
            View.Model.UserView userView = new View.Model.UserView();
            using (var unitOfWork = new UnitOfWork())
            {
                var user = unitOfWork.UserRepository.Get(u => u.UserName.ToLower() == userName.ToLower()).SingleOrDefault();
                new Map().DomainToView(userView, user);
            }
            return userView;
        }
        public View.Model.UserView ByGuid(string guid)
        {
            View.Model.UserView userView = new View.Model.UserView();
            using (var unitOfWork = new UnitOfWork())
            {
                var user = unitOfWork.UserRepository.Get(u => u.Id.ToLower() == guid.ToLower()).SingleOrDefault();
                new Map().DomainToView(userView, user);
            }
            return userView;
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

        public View.Model.UserView Details(int userId)
        {
            View.Model.UserView userObjLibrary = new View.Model.UserView();
            using (var unitOfWork = new UnitOfWork())
            {
                var user = unitOfWork.UserRepository.Get(x => x.Uid == userId).SingleOrDefault();
                if (user != null)
                {
                    new Map().DomainToView(userObjLibrary, user);
                }
                if (user != null)
                {
                    var punch = unitOfWork.PunchRepository.Get(p => p.UserId == userId);
                    if (punch == null) return userObjLibrary;
                    userObjLibrary.LastPunch = new View.Model.PunchView ();
                    new Map().DomainToView(userObjLibrary.LastPunch, punch.Last());
                }
            }
            return userObjLibrary;
        }

        public View.Model.UserView ByEmail(string email)
        {
            View.Model.UserView userObjLibrary = new View.Model.UserView();
            using (var unitOfWork = new UnitOfWork())
            {
                var user = unitOfWork.UserRepository.Get(x => x.Email == email).SingleOrDefault();
                if (user != null)
                {
                    new Map().DomainToView(userObjLibrary, user);
                }
                if (user != null)
                {
                    var punch = unitOfWork.PunchRepository.Get(p => p.UserId == user.Uid);
                    if (punch == null) return userObjLibrary;
                    userObjLibrary.LastPunch = new View.Model.PunchView();
                    new Map().DomainToView(userObjLibrary.LastPunch, punch.Last());
                }
            }
            return userObjLibrary;
        }

        public int Update(View.Model.UserView userView, bool adminUpdate)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                User user = unitOfWork.UserRepository.GetById(userView.Id);
                user.FirstName = userView.FirstName;
                user.LastName = userView.LastName;
                user.MiddleName = userView.MiddleName;
                user.Email = userView.Email;
                user.PhoneNumber = userView.PhoneNumber;
                if (!adminUpdate)
                {
                    user.LastActiveMacAddress = userView.LastActiveMacAddress;
                    user.LastActivityIp = userView.LastActivityIp;
                    user.LastActivityDateUtc = DateTime.UtcNow;
                }
                else
                {
                    user.RegisteredTimeZone = userView.RegisteredTimeZone;
                    if (userView.UserTypeId > 0)
                        user.UserTypeId = userView.UserTypeId;
                    if (userView.EmploymentTypeId > 0)
                        user.EmploymentType = (Domain.Model.EmploymentType)userView.EmploymentTypeId;
                }
                unitOfWork.UserRepository.Update(user);
                unitOfWork.Save();
                userView.UserId = user.Uid;
            }
            return userView.UserId;
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
                                     Value = $"{user.Uid}"
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

        public List<View.Model.UserView> Get(string username)
        {
            List<View.Model.UserView> userViews;
            using (var unitOfWork = new UnitOfWork())
            {
                var users = unitOfWork.UserRepository.Get(x => x.UserName == username);
                userViews = users
                    .Select(x =>
                        new View.Model.UserView().InjectFrom(x))
                        .Cast<View.Model.UserView>().ToList();
            }
            return userViews;
        }

        public List<View.Model.UserView> GetAllUsers(int companyId)
        {
            List<View.Model.UserView> userObjLibraries;
            using (var unitOfWork = new UnitOfWork())
            {
                var users = unitOfWork.UserRepository.Get(x => x.CompanyId == companyId);
                userObjLibraries = users
                    .Select(x =>
                        new View.Model.UserView().InjectFrom(x))
                        .Cast<View.Model.UserView>().ToList();
            }
            return userObjLibraries;
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
    }
}
