using System;
using System.Web.Mvc;

namespace PunchClock.UI.Web.Controllers
{
    public class ErrorsController : BaseController
    {
        //
        // GET: /Error/
        [ActionName("General")]
        public ActionResult General(Exception exception)
        {
            ViewBag.Message = "There was an unknown error";
            return View("General");
        }
        [ActionName("403")]
        public ActionResult Http403()
        {
            ViewBag.Message = "Un-authorized";
            return View("Http403");
        }
        [ActionName("404")]
        public ActionResult Http404()
        {
            ViewBag.Message = "The page you are requesting is either removed or moved";
            return View("Http404");
        }
        [ActionName("500")]
        public ActionResult Http500()
        {
            ViewBag.Message = "There was a error to complete your request";
            return View("Http500");
        }

    }
}
