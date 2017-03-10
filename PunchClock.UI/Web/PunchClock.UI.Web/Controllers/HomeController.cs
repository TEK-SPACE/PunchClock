using System.Web.Mvc;

namespace PunchClock.UI.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Online Tool";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "software workflow";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "software contributors";

            return View();
        }
    }
}
