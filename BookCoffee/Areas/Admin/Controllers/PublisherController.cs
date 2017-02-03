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
    public class PublisherController : BaseController
    {
        // GET: Admin/Author
        public ActionResult Index()
        {
            var dao = new PublisherDAO();
            var model = dao.listPublisherAdmin();
            return View(model);
        }

        //Tạo mới NXB
        [HttpGet]
        public ActionResult CreatePublisher()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreatePublisher(Publisher entity)
        {
            var dao = new  PublisherDAO();
            if (ModelState.IsValid)
            {
                //Kiểm tra xem tên NXB dự định tạo đã có chưa
                if (dao.ExistPublisherForCreate(entity.Name) == false)
                {
                    string metatitle = CastString.Cast(entity.Name);
                    entity.MetaTitle = metatitle;
                    entity.CreatedDate = DateTime.Now;
                    bool result = dao.CreatePublisher(entity);
                    if (result)
                    {
                        SetAltert("Tạo mới nhà xuất bản thành công", 0);
                        return RedirectToAction("Index", "Publisher");
                    }
                    else
                    {
                        SetAltert("Chưa tạo được nhà xuất bản, vui lòng thử lại", 1);
                    }
                }
                else
                {
                    SetAltert("Nhà xuất bản này đã có trong hệ thống", 2);
                }
            }
            return View(entity);
        }

        //Sửa thông tin Author
        [HttpGet]
        public ActionResult UpdatePublisher(long id)
        {
            var model = new PublisherDAO().PublisherDetails(id);
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdatePublisher(Publisher entity)
        {
            var dao = new PublisherDAO();
            if (ModelState.IsValid)
            {
                //Kiểm tra xem tên tác giả để cập nhật đã có trong hệ thống chưa?
                bool exist = dao.ExistPublisherForUpdate(entity.Name, entity.ID);
                if (!exist)
                {
                    string metatitle = CastString.Cast(entity.Name);
                    entity.MetaTitle = metatitle;
                    bool result = dao.UpdatePublisher(entity);
                    if (result)
                    {
                        SetAltert("Cập nhật thông tin nhà xuất bản thành công", 0);
                        return RedirectToAction("Index", "Publisher");
                    }
                    else
                    {
                        SetAltert("Chưa cập nhật được, vui lòng thử lại", 2);
                    }
                }
                else
                {
                    SetAltert("Nhà xuất bản này đã có trong hệ thống, vui lòng chọn nhà xuất bản khác", 2);
                }
            }
            return View(entity);
        }

        //Phương thức này dùng cho lời gọi Ajax từ trạng thái user trong trang Index
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new PublisherDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}