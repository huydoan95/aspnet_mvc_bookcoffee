using Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookCoffee.Controllers
{
    public class EventsController : Controller
    {
        // GET: Events
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EventDetails(long id) {
            var dao = new EventsDAO();
            int ID = Convert.ToInt32(id);
            var model = dao.EventDetail(ID);
            return View(model);
        }
    }
}