using System.Web.Mvc;

namespace PunchClock.UI.Web.Controllers
{
    [Authorize]
    public class ArticleController : Controller
    {
        // GET: CMS
        public ActionResult Add()
        {
            return View();
        }
    }
}