using Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookCoffee.Controllers
{
    public class ContentController : Controller
    {
        // GET: Content
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ViewContentDetail(long id) {
            var dao = new ContentDAO();
            int ID = Convert.ToInt32(id);
            var model = dao.ContentDetail(ID);
            return View(model);
        }
    }
}