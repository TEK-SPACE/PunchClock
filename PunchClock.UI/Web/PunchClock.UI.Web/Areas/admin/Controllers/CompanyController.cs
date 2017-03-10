using PunchClock.Implementation;
using PunchClock.Objects.Core;
using System.Web.Mvc;

namespace PunchClock.UI.Web.Areas.admin.Controllers
{
    public class CompanyController : Controller
    {
        //
        // GET: /admin/Company/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Message = "Please enter your company information";
            return View();
        }

        [HttpPost]
        public ActionResult Add(CompanyObjLibrary obj)
        {
            ViewBag.Message = "Successfully Added";
            CompanyService cb = new CompanyService();
            obj.CompanyId = cb.Add(obj);
            return View();
        }

    }
}
