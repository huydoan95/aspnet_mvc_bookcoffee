using Models.DAO;
using Models.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookCoffee.Controllers
{
    public class GalleryController : Controller
    {
        private readonly BookCoffeeShopDbContext db = new BookCoffeeShopDbContext();
        public ActionResult Index()
        {
            var dao = new GalleryDAO();
            var model = dao.listGallery(0,0);
            return View(model);
        }
     
    }
}
