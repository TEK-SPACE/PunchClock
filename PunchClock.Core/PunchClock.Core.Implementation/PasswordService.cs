using System;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using PunchClock.Core.DataAccess;
using PunchClock.Core.DataAccess.Models;

namespace PunchClock.Core.Implementation
{
    public static class PasswordService
    {
        private static bool ValidatePassword(View.Model.UserView user, string password)
        {
            return user.PasswordHash == EncodePassword(password, user.PasswordSalt);
        }

        public static int ValidatePassword(string userName, string password, string ipAddress, string macAddress)
        {
            View.Model.UserView userView = new View.Model.UserView();
            using (var uow = new UnitOfWork())
            {
                using (var userRepo = new UserRepository(uow))
                {
                    var user = userRepo.All.FirstOrDefault(x => x.UserName.ToLower() == userName.ToLower());
                    if (user == null)
                        return -1;

                    user.LastActivityIp = ipAddress;
                    user.LastActiveMacAddress = macAddress;
                    userRepo.Update(user);
                    uow.Save(); 
                    var mapper = new Model.Mapper.Map();
                    mapper.DomainToView(userView, user);
                }
            }
            return ValidatePassword(userView, password) ? userView.UserId : -2;
        }

        public static string GenerateSalt()
        {
            byte[] buf = new byte[16];
            (new RNGCryptoServiceProvider()).GetBytes(buf);
            return Convert.ToBase64String(buf);
        }

        public static string EncodePassword(string pass, string salt)
        {
            try
            {
                byte[] bytes = Encoding.Unicode.GetBytes(pass);
                byte[] src = Convert.FromBase64String(salt);
                byte[] dst = new byte[src.Length + bytes.Length];
                Buffer.BlockCopy(src, 0, dst, 0, src.Length);
                Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);

                HashAlgorithm algorithm = HashAlgorithm.Create("SHA512");
                if (algorithm != null)
                {
                    var inArray = algorithm.ComputeHash(dst);
                    return Convert.ToBase64String(inArray);
                }
            }
            catch (Exception)
            {
                // This gets thrown if the salt is invalid
                return "--Invalid--";   // Any non empty value is fine to make sure the match fails
            }
            return null;
        }
    }
}
