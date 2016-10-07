using BookCoffee.Common;
using Models.DAO;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookCoffee.Areas.Admin.Controllers
{
    public class BooksController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index()
        {
            var dao = new BooksDAO();
            var model = dao.listBooks();
            return View(model);
        }

        //Phương thức này để khởi tạo cho Dropdownlist CategoryID trong View CreateProduct
        public void SetViewBag(long? selectedID = null)
        {
            var dao = new BookCategoryDAO();
            ViewBag.CategoryID = new SelectList(dao.listBookCategoryClient(), "id", "Name", selectedID);
        }

        //Tạo mới sản phẩm
        [HttpGet]
        public ActionResult CreateBook()
        {
            SetViewBag();
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateBook(Book model)
        {
            if (ModelState.IsValid)
            {
                var dao = new BooksDAO();
                //Lấy ra user đăng nhập để gán vào CreatedBY
                UserLogin userlogin = (UserLogin)Session["USER_SESSION"];
                model.CreatedBy = userlogin.UserName;
                bool exits = dao.bookExist(model.Name);
                if (!exits)
                {
                    //Nếu chọn sách này là TopHot
                    if (model.TopHot == true)
                    {
                        bool TopHot = dao.TopHotForCreate();
                        if (!TopHot)
                        {
                            string metatitle = CastString.Cast(model.Name);
                            model.MetaTitle = metatitle;
                            model.CreatedDate = DateTime.Now;
                            bool result = dao.CreateBook(model);
                            if (result)
                            {
                                SetAltert("Tạo mới sách thành công", 0);
                                return RedirectToAction("Index", "Books");
                            }
                            else
                            {
                                SetAltert("Tạo mới không thành công", 2);
                            }
                        }
                        else
                        {
                            SetAltert("Tạm thời không thể thiết lập TopHot cho sách này, vì hiện đang có 1 sách thuộc TopHot", 2);
                        }
                    }
                    else
                    {
                        string metatitle = CastString.Cast(model.Name);
                        model.MetaTitle = metatitle;
                        model.CreatedDate = DateTime.Now;
                        bool result = dao.CreateBook(model);
                        if (result)
                        {
                            SetAltert("Tạo mới sách thành công", 0);
                            return RedirectToAction("Index", "Books");
                        }
                        else
                        {
                            SetAltert("Tạo mới không thành công", 2);
                        }
                    }
                }
                else
                {
                    SetAltert("Tên sách này đã có", 2);
                }

            }
            SetViewBag();
            return View(model);
        }

        //Xóa sản phẩm
        [HttpDelete]
        public ActionResult DeleteBook(int id)
        {
            new BooksDAO().DeleteBook(id);
            return RedirectToAction("Index");

        }

        //Update sản phẩm
        [HttpGet]
        public ActionResult UpdateBook(int id)
        {
            var dao = new BooksDAO();
            var model = dao.bookDetail(id);
            SetViewBag();
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateBook(Book model)
        {
            if (ModelState.IsValid)
            {
                var dao = new BooksDAO();
                UserLogin userlogin = (UserLogin)Session["USER_SESSION"];
                model.ModifiedBy = userlogin.UserName;
                //Kiểm tra xem tên sách này đã có chưa
                bool exist = dao.bookExistForUpdate(model.Name, model.ID);
                if (!exist)
                {
                    //Trường hợp chọn sách là Tophot
                    if (model.TopHot == true)
                    {
                        bool tophot = dao.TopHotForUpdate(model.ID);
                        //Kiểm tra xem hiện tại đang có sách nào thuộc TopHot chưa
                        if (!tophot)
                        {
                            string metatitle = CastString.Cast(model.Name);
                            model.MetaTitle = metatitle;
                            bool result = dao.UpdateBook(model);
                            if (result)
                            {
                                SetAltert("Cập nhật thành công", 0);
                                return RedirectToAction("Index", "Books");
                            }
                            else
                            {
                                SetAltert("Cập nhật không thành công", 2);
                            }
                        }
                        else
                        {
                            SetAltert("Tạm thời không thể thiết lập TopHot cho sách này, vì hiện đang có 1 sách thuộc TopHot", 2);
                        }
                    }
                    else
                    {
                        string metatitle = CastString.Cast(model.Name);
                        model.MetaTitle = metatitle;
                        bool result = dao.UpdateBook(model);
                        if (result)
                        {
                            SetAltert("Cập nhật thành công", 0);
                            return RedirectToAction("Index", "Books");
                        }
                        else
                        {
                            SetAltert("Cập nhật không thành công", 2);
                        }
                    }

                }
                else
                {
                    SetAltert("Tên sách này đã có", 2);
                }
            }
            else
            {

            }
            SetViewBag();
            return View(model);
        }

        //Xem chi tiết sản phẩm
        public ActionResult BookDetail(int id)
        {
            var dao = new BooksDAO();
            var model = dao.bookDetail(id);
            return View(model);
        }

        //Phương thức này dùng cho lời gọi Ajax từ trạng thái user trong trang Index
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new BooksDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }

    }
}