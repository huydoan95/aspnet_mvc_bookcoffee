using Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookCoffee.Controllers
{
    public class BooksController : Controller
    {
        // GET: Books
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BookDetails(long id) {
            var dao = new BooksDAO();
            int ID = Convert.ToInt32(id);
            var model = dao.bookDetail(ID);
            return View(model);
        }
    }
}