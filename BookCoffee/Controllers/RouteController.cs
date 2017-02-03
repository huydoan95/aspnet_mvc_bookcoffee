using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookCoffee.Controllers
{
    public class RouteController : Controller
    {
        // GET: Route
        public ActionResult Index()
        {
            Session.Add(Common.CommonConstants.FIRSTLOAD_SESSION, "NO");
            return RedirectToAction("index", "home");
        }
    }
}