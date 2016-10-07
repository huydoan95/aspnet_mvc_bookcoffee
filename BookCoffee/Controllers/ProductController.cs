using Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookCoffee.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProductDetails(long id) {
            var dao = new ProductDAO();
            int ID = Convert.ToInt32(id);
            var model = dao.productDetail(ID);
            return View(model);
        }
    }
}