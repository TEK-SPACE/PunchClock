using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PunchClock.Core.Contracts;
using PunchClock.Core.DataAccess;
using PunchClock.Core.Implementation;
using PunchClock.Domain.Model;

namespace PunchClock.UI.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly ICompanyRepository _companyRepository;

        protected readonly UserSession UserSession;
        protected User OperatingUser = new User();
        public BaseController()
        {
            _companyRepository = new CompanyService();
            UserSession = new SessionService().GetCurrentSession(HttpContext);  
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (User != null && User.Identity.IsAuthenticated)
            {
                UserService userService = new UserService();
                OperatingUser = userService.Details(User.Identity.Name);
                using (var unitOfWork = new UnitOfWork())
                {
                    OperatingUser.Company = unitOfWork.CompanyRepository.GetById(OperatingUser.CompanyId);
                }
            }
            ViewBag.Menu =  _companyRepository.GetSiteMap(companyId: OperatingUser.CompanyId);

            base.OnActionExecuting(filterContext);
        }
        // ReSharper disable once RedundantOverriddenMember
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        protected void ReadModelStateError(ModelStateDictionary modelState)
        {
            IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
            ViewData["ModelError"] = string.Join("<br/>", allErrors.Select(x => x.ErrorMessage));
        }
    }
}
