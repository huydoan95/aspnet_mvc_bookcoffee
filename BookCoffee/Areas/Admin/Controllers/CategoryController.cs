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
    public class CategoryController : BaseController
    {
        // GET: Admin/Category
        //public ActionResult Index(string searchString, int page = 1, int pagesize = 10)
        //{
        //    var dao = new CategoryDAO();
        //    var model = dao.ListAllPaging(searchString, page, pagesize);
        //    //Tạo viewBag để giữ lại nội dung trên textbox search và để làm tham số cho phân trang
        //    ViewBag.SearchString = searchString;
        //    return View(model);
        //}

        public ActionResult Index() {
            var dao = new CategoryDAO();
            var model = dao.listAllCategory();
            return View(model);
        }

        //Tạo mới Loại tin tức
        [HttpGet]
        public ActionResult CreateCategory() {

            return View();
        }

        [HttpPost]
        public ActionResult CreateCategory(Category model) {
            var dao = new CategoryDAO();
            if (ModelState.IsValid)
            {
                //kiểm tra xem tên loại tin này đã được tao chưa
                if (!dao.CategoryExits(model.Name))
                {
                    string metatitle = CastString.Cast(model.Name);
                    model.MetaTitle = metatitle;
                    var createby = Session["USER_SESSION"];
                    UserLogin userlogin = (UserLogin)Session["USER_SESSION"];
                    model.CreatedBy = userlogin.UserName;
                    long result = dao.CreateCategory(model);
                    if (result > 0)
                    {
                        return RedirectToAction("Index", "Category");
                    }
                    else
                    {
                        ModelState.AddModelError("CreateCategoryError", "Chưa tạo được loại tin");
                    }
                }
                else {
                    //Trường hợp đã tồn tại 
                    ModelState.AddModelError("CreateCategoryError", "Tên loại tin này đã có");
                }
                
            }
            else {
               // ModelState.AddModelError("CreateCategoryError", "Thông tin không hợp lệ");
            }

            return View(model);

        }


        //Xóa loại tin
        [HttpDelete]
        public ActionResult DeleteCategory(int id) {
            new CategoryDAO().DeleteCategory(id);
            return RedirectToAction("Index");

        }


        //Update thông tin loại tin
        [HttpGet]
        public ActionResult UpdateCategory(int id) {
            var category = new CategoryDAO().ViewCategory(id);
            return View(category);

        }
        [HttpPost]
        public ActionResult UpdateCategory(Category model) {
            var dao = new CategoryDAO();
            if (ModelState.IsValid)
            {
                UserLogin modifiedby = (UserLogin)Session["USER_SESSION"];
                model.ModifiedBy = modifiedby.UserName;
                string metatitle = CastString.Cast(model.Name);
                model.MetaTitle = metatitle;
                bool result = dao.UpdateCategory(model);
                if (result)
                {
                    return RedirectToAction("Index", "Category");
                }
                else {
                    ModelState.AddModelError("UpdateCategoryError", "Chưa cập nhật được ");
                }
            }
            else {
                ModelState.AddModelError("UpdateCategoryError","Thông tin không hợp lệ");
            }
            return View(model);
        }


        //Hiển thị chi tiết loại tin
        public ActionResult CategoryDetail(int id) {
            Category categorydetail = new CategoryDAO().ViewCategory(id);
            return View(categorydetail);
        }
    }
}