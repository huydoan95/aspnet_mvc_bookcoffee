using Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookCoffee.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TableBooking() {
            var dao = new OrderDAO();
            int quanlity = dao.quanlityOfTableBookingOrder();
            ViewBag.quanlity = quanlity;
            return PartialView();
        }
        //Hiển thị danh sách đặt bàn
        public ActionResult listTableBooking() {
            var dao = new OrderDAO();
            var model = dao.listTableBooking();
            return PartialView(model);
        }
    }
}