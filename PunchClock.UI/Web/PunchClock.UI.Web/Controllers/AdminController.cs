using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PunchClock.UI.Web.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Config()
        {
            return View();
        }
    }
}