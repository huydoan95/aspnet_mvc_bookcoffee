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
    public class BookCategoryController : BaseController
    {
        // GET: Admin/ProductCategory
        public ActionResult Index()
        {
            var dao = new BookCategoryDAO();
            var listbookCategory = dao.listBookCategory();
            return View(listbookCategory);
        }


        //Tạo mới loại sản phẩm 
        [HttpGet]
        public ActionResult CreateBookCategory()
        {

            return View();
        }
        [HttpPost]
        public ActionResult CreateBookCategory(BookCategory model)
        {
            if (ModelState.IsValid)
            {
                //Kiểm tra xem tên loại sản phẩm đã có chư
                var dao =  new BookCategoryDAO();
                bool exsits = dao.BookCategoryExist(model.Name);
                if (!exsits)
                {
                    UserLogin userlogin = (UserLogin)Session["USER_SESSION"];
                    model.CreatedBy = userlogin.UserName;
                    model.CreatedDate = DateTime.Now;
                    string metatitle = CastString.Cast(model.Name);
                    model.MetaTitle = metatitle;
                    bool result = dao.CreateBookCategory(model);
                    if (result)
                    {
                        SetAltert("Tạo mới loại sách thành công",0);
                        return RedirectToAction("Index", "BookCategory");
                    }
                    else
                    {
                        SetAltert("Chưa tạo loại sách được",2);
                    }
                }
                else
                {
                    //trường hợp tên đã có
                    SetAltert("Tên loại sách này đã có",2);
                }

            }
            else
            {

            }
            return View(model);
        }

        //Update loại sản phẩm
        [HttpGet]
        public ActionResult UpdateBookCategory(int id)
        {
            var dao = new BookCategoryDAO();
            var model = dao.BookCategoryDetail(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateBookCategory(BookCategory model)
        {
            if (ModelState.IsValid)
            {
                var dao = new BookCategoryDAO();
                //Kiểm tra xem tên loại sách này đã có chưa
                var exist = dao.BookCategoryExistForUpdate(model.Name,model.ID);
                if (!exist)
                {
                    UserLogin userlogin = (UserLogin)Session["USER_SESSION"];
                    model.ModifiedBy = userlogin.UserName;
                    string metatitle = CastString.Cast(model.Name);
                    model.MetaTitle = metatitle;
                    bool result = dao.UpdateBookCategory(model);
                    if (result)
                    {
                        SetAltert("Update loại sách thành công", 0);
                        return RedirectToAction("Index", "BookCategory");
                    }
                    else
                    {
                        SetAltert("Chưa cập nhật được", 2);
                    }
                }
                else {
                    SetAltert("Tên loại sách này đã có", 2);
                }
               
            }
            else
            {

            }

            return View(model);
        }

        //Xóa loại  sản phẩm
        [HttpDelete]
        public ActionResult DeleteBookCategory(int id)
        {
            new BookCategoryDAO().DeletebookCategory(id);
            return RedirectToAction("Index");

        }


        //Hiển thị chi tiết loại sản phẩm
        public ActionResult BookCategoryDetail(int id)
        {
            var dao = new BookCategoryDAO();
            var model = dao.BookCategoryDetail(id);
            return View(model);
        }

        //Phương thức này dùng cho lời gọi Ajax từ trạng thái user trong trang Index
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new BookCategoryDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }

    }
}