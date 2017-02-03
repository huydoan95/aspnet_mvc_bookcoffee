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
    public class AuthorController : BaseController
    {
        // GET: Admin/Author
        public ActionResult Index()
        {
            var dao = new AuthorDAO();
            var model = dao.listAuthorsAdmin();
            return View(model);
        }

        //Tạo mới tác giả sách
        [HttpGet]
        public ActionResult CreateAuthor()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateAuthor(Author entity)
        {
            var dao = new AuthorDAO();
            if (ModelState.IsValid)
            {
                //Kiểm tra xem tên Author dự định tạo đã có chưa
                if (dao.ExistAuthorForCreate(entity.Name) == false)
                {
                    string metatitle = CastString.Cast(entity.Name);
                    entity.MetaTitle = metatitle;
                    entity.CreatedDate = DateTime.Now;
                    bool result = dao.CreateAuthor(entity);
                    if (result)
                    {
                        SetAltert("Tạo mới tác giả thành công", 0);
                        return RedirectToAction("Index", "Author");
                    }
                    else
                    {
                        SetAltert("Chưa tạo được tác giả, vui lòng thử lại", 1);
                    }
                }
                else
                {
                    SetAltert("Tác giả này đã có trong hệ thống", 2);
                }
            }
            return View(entity);
        }

        //Sửa thông tin Author
        [HttpGet]
        public ActionResult UpdateAuthor(long id)
        {
            var model = new AuthorDAO().AuthorDetails(id);
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateAuthor(Author entity)
        {
            var dao = new AuthorDAO();
            if (ModelState.IsValid)
            {
                //Kiểm tra xem tên tác giả để cập nhật đã có trong hệ thống chưa?
                bool exist = dao.ExistAuthorForUpdate(entity.Name, entity.ID);
                if (!exist)
                {
                    string metatitle = CastString.Cast(entity.Name);
                    entity.MetaTitle = metatitle;
                    bool result = dao.UpdateAuthor(entity);
                    if (result)
                    {
                        SetAltert("Cập nhật thông tin tác giả thành công", 0);
                        return RedirectToAction("Index", "Author");
                    }
                    else {
                        SetAltert("Chưa cập nhật được, vui lòng thử lại", 2);
                    }
                }
                else {
                    SetAltert("Tác giả này đã có trong hệ thống, vui lòng chọn tác giả khác", 2);
                }
            }
            return View(entity);
        }

        //Phương thức này dùng cho lời gọi Ajax từ trạng thái user trong trang Index
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new AuthorDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}