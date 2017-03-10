using System.Web.Mvc;

namespace PunchClock.UI.Web.Areas.admin.Controllers
{
    public class UsersController : Controller
    {
        //
        // GET: /admin/Users/

        public ActionResult Index()
        {
            return View();
        }

    }
}
