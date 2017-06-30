using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PunchClock.Core.Contracts;
using PunchClock.Core.DataAccess;
using PunchClock.Core.Implementation;
using PunchClock.Domain.Model;
using PunchClock.UI.Web.Helpers;
using UserType = PunchClock.Domain.Model.Enum.UserType;

namespace PunchClock.UI.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly ICompany _companyRepository;
        private readonly Core.Contracts.IUser _userService;
        protected readonly UserSession UserSession;
        private readonly ISite _siteService;

        protected User OperatingUser = new User();

        public BaseController()
        {
            _userService = new UserService();
            _companyRepository = new CompanyService();
            UserSession = new SessionService().GetCurrentSession(HttpContext);
            _siteService = new SiteService();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            OperatingUser = User.Identity.IsAuthenticated
                ? _userService.Details(User.Identity.Name)
                : new User { CompanyId = 1, UserTypeId = (int)UserType.Guest };

            var descriptor = filterContext.ActionDescriptor;
            var actionName = descriptor.ActionName;
            var controllerName = descriptor.ControllerDescriptor.ControllerName;
            var menu = _companyRepository.GetSiteMap(companyId: OperatingUser.CompanyId);

            var rules = menu.Flatten(x=>x.Children ?? new List<SiteMap>()).Where(x => x.Controller.Equals(controllerName, StringComparison.OrdinalIgnoreCase) &&
                            x.Action.Equals(actionName, StringComparison.OrdinalIgnoreCase)).ToList();

            if (rules.Any())
            {
                if (!rules.Any(x => x.UserAccesses.Any(z => z.UserRoleId == OperatingUser.UserTypeId)))
                {
                    filterContext.Result = new RedirectResult(Url.Action("403","Error"));
                    return;
                }
            }

           
            foreach (var item in menu)
            {
                if (item.UserAccesses != null && item.UserAccesses.Any(x => x.UserRoleId == OperatingUser.UserTypeId))
                {
                    item.IsUserAccessable = true;
                    foreach (var child in item.Children)
                    {
                        if (child.UserAccesses != null &&  child.UserAccesses.Any(x => x.UserRoleId ==
                                                        ((OperatingUser != null && OperatingUser.Uid > 0)
                                                            ? OperatingUser.UserTypeId
                                                            : (int) UserType.Guest)))
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
