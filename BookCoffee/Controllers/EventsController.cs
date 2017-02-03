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

        //Tạo slider
        public ActionResult Eventslider() {
            var dao = new EventsDAO();
            var model = dao.TopHotEvents(0, 0);
            return PartialView(model);
        }

        //Các nội dung cột bên phải
        public ActionResult rightsidebar() {
            return PartialView();
        }

        //Tạo list các sự kiện sắp diễn ra
        public ActionResult NextToEvents() {
            var dao = new EventsDAO();
            var model = dao.listEventsClient();
            return PartialView(model);
        }

        //Load hình các sự kiện lên rightsidebar
        public ActionResult listEventImages() {
            var dao = new EventsDAO();
            var model = dao.newestEvents(9);
            return PartialView(model);
        }

        public ActionResult EventDetails(long id) {
            var dao = new EventsDAO();
            int ID = Convert.ToInt32(id);
            var model = dao.EventDetail(ID);
            return View(model);
        }
    }
}