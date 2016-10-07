using Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookCoffee.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.listContent = new ContentDAO().listContent();
            ViewBag.NewestContent = new ContentDAO().listNewestContent();
            ViewBag.TopHotContent = new ContentDAO().TopHotConten();

            //Danh sách sách
            ViewBag.listBooks = new BooksDAO().ListBooksClient();
            //Sách Tophot
            ViewBag.TopHotBook = new BooksDAO().TopHotForClient();
            //Danh sách thực đơn bán chạy
            ViewBag.TopHotProducts = new ProductDAO().listProductBestSale(1);

            //Lấy ra 3 sự kiện mới nhất đưa lên trang chủ
            ViewBag.NewestEvents = new EventsDAO().newestEvents(3);

            //Lấy ra 3 sự kiện TopHOt
            ViewBag.EventTopHot = new EventsDAO().TopHotEvents(1,3);

            ///Lấy ra danh sách tin LawCorner thuộc TopHot
            ViewBag.TopHotLawCorner = new LawCornerDAO().TopHotForClient();

            //Lấy ra 3 sự tin luật mới nhất đưa lên Index
            ViewBag.NewestLawCorner = new LawCornerDAO().listNewestLawCorner(3);

            //Lấy ra Gallery
            ViewBag.listGallery = new GalleryDAO().listGallery(1,9);

            return View();
        }


        //Lấy ra Menu bên trái Logo
        [ChildActionOnly]
        public ActionResult MenuLeftTop() {
            var dao = new MenuDAO();
            var model = dao.listMenuByMenuType(1);
            return PartialView(model);
        }


        //Lấy ra Menu bên phải Logo
        [ChildActionOnly]
        public ActionResult MenuRightTop()
        {
            var dao = new MenuDAO();
            var model = dao.listMenuByMenuType(2);
            return PartialView(model);
        }
    }
}