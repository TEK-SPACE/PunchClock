using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PunchClock.Core.Contracts;
using PunchClock.Core.DataAccess;
using PunchClock.Core.Implementation;
using PunchClock.Domain.Model;

namespace PunchClock.UI.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly ICompany _companyRepository;

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
            var menu =  _companyRepository.GetSiteMap(companyId: OperatingUser.CompanyId);
            foreach (var item in menu)
            {
                if(item.UserAccesses.Any(x=>x.UserRoleId == ((OperatingUser != null && OperatingUser.Uid > 0) ? OperatingUser.UserTypeId : (int)Domain.Model.Enum.UserType.Guest)))
                {
                    item.IsUserAccessable = true;
                    foreach (var child in item.Children)
                    {
                        if (child.UserAccesses.Any(x => x.UserRoleId == ((OperatingUser != null && OperatingUser.Uid > 0) ? OperatingUser.UserTypeId : (int)Domain.Model.Enum.UserType.Guest)))
                        {
                            child.IsUserAccessable = true;
                        }
                    }
                }
            }

            ViewBag.Menu = menu;
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
        protected void AddIdentityErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}
