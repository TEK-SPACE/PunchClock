
using PunchClock.Implementation;
using PunchClock.Objects.Core;
using System.Web.Mvc;

namespace PunchClock.UI.Web.Controllers
{
    public class BaseController : Controller
    {
        public SessionObjLibrary userSession = new SessionObjLibrary();
        public UserObjLibrary operatingUser = new UserObjLibrary();
        public BaseController()
        {
            SessionService session = new SessionService();
            userSession = session.GetCurrentSession(HttpContext);
            
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (User != null && User.Identity.IsAuthenticated)
            {
                UserService ub = new UserService();
                operatingUser = ub.Details(User.Identity.Name);
            }
            base.OnActionExecuting(filterContext);
        }
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }
    }
}
