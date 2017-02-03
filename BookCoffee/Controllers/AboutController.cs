using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.DAO;

namespace BookCoffee.Controllers
{
    public class AboutController : Controller
    {
        // GET: About
        public ActionResult Index()
        {
            var dao = new AboutDAO();
            //ViewBag.aboutDAO = dao.aboutDetail(1);      
            return View(dao.aboutDetail(1));
        }
    }
}