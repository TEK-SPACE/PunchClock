using PunchClock.Implementation;
using System.Security.Principal;
using PunchClock.Objects.Core.Enum;

namespace PunchClock.UI.Web.Helpers
{
    public static class UserHelpers
    {
        public static bool IsEditor(this IPrincipal user)
        {
            return false; //Do some stuff
        }
        public static bool IsAdmin(this IPrincipal user)
        {
            if (user.Identity.IsAuthenticated)
            {
                UserService userService = new UserService();
                var uDetails = userService.Details(user.Identity.Name);
                return uDetails.UserTypeId != 1;
            }
            return false; //Do some stuff
        }

        public static bool IsHumanResource(IPrincipal user)
        {
            if (user.Identity.IsAuthenticated)
            {
                UserService userService = new UserService();
                var uDetails = userService.Details(user.Identity.Name);
                return uDetails.UserTypeId == (int)UserType.HumanResources;
            }
            return false; //Do some stuff
        }
    }
}