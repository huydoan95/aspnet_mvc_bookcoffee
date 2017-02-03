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
            var dao = new BooksDAO();
            var model = dao.ListBooksShowOnHome();
            ViewBag.BookCategoryName = new BookCategoryDAO().BookCategoryShowOnHome().Name;
            return View(model);
        }

        public ActionResult BookDetails(long id) {
            var dao = new BooksDAO();
            int ID = Convert.ToInt32(id);
            var model = dao.bookDetail(ID);
            //Update viewcount
            int viewcount = dao.currentViewcount(id);
            viewcount++;
            model.ViewCount = viewcount;
            dao.UpdateBookViewcount(model);
            return View(model);
        }

        public ActionResult MenuBookCategory() {
            var dao = new BookCategoryDAO();
            var model = dao.listBookCategoryClient();
            return PartialView(model);
        }

        public ActionResult SliderTopHotBooks() {
            var dao = new BooksDAO();
            var model = dao.TopHotForClient();
            return PartialView(model);
        }

        public ActionResult ListNewBooks() {
            var dao = new BooksDAO();
            var model = dao.ListNewBooks(4);
            return PartialView(model);
        }

        public ActionResult BestPreviewBooks() {
            var dao = new BooksDAO();
            //lấy ra 6 sách được xem nhiều nhất
            var model = dao.BestPreviewBooks(8);
            return PartialView(model);
        }

        //Tạo Menu nhà xuất bản
        public ActionResult MenuPublisher() {
            var dao = new PublisherDAO();
            var model = dao.listPublisherClient();
            return PartialView(model);
        }

        //Tạo Menu tác giả
        public ActionResult MenuAuthor() {
            var dao = new AuthorDAO();
            var model = dao.listAuthorsClient();
            return PartialView(model);
        }
    }
}