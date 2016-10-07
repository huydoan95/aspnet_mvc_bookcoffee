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
    public class EventsController : BaseController
    {
        // GET: Admin/Product

        public ActionResult Index()
        {
            var dao = new EventsDAO();
            var model = dao.listEvents();
            return View(model);
        }


        //Tạo mới sự kiện
        [HttpGet]
        
        public ActionResult CreateEvent()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateEvent(_event model)
        {
            if (ModelState.IsValid)
            {
                var dao = new EventsDAO();
                bool exits = dao.EventExist(model.Name);
                if (!exits)
                {
                    //Lấy ra user đăng nhập để gán vào CreatedBY
                    UserLogin userlogin = (UserLogin)Session["USER_SESSION"];
                    model.CreatedBy = userlogin.UserName;
                    string metatitle = CastString.Cast(model.Name);
                    model.MetaTitle = metatitle;
                    bool result = dao.CreateEvent(model);
                    if (result)
                    {
                        SetAltert("Tạo sự kiện thành công",0);
                        return RedirectToAction("Index", "Events");
                    }
                    else
                    {
                        SetAltert("Chưa tạo được sự kiện",2);
                    }
                }
                else
                {
                    SetAltert("Sự kiện này đã có",2);
                }

            }
            return View(model);
        }

        //Xóa sự kiện
        [HttpDelete]
        public ActionResult DeleteEvent(int id)
        {
            new EventsDAO().DeleteEvent(id);
            return RedirectToAction("Index");

        }

        //Update sự kiện
        [HttpGet]
        public ActionResult UpdateEvent(int id)
        {
            var dao = new EventsDAO();
            var model = dao.EventDetail(id);
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateEvent(_event model)
        {
            if (ModelState.IsValid)
            {
                var dao = new EventsDAO();
                //Kiểm tra xem tên sự kiện này đã có chưa
                bool exist = dao.EventExistForUpdate(model.Name, Convert.ToInt32(model.ID));
                if (!exist)
                {
                    UserLogin userlogin = (UserLogin)Session["USER_SESSION"];
                    model.ModifiedBy = userlogin.UserName;
                    string metatitle = CastString.Cast(model.Name);
                    model.MetaTitle = metatitle;
                    bool result = dao.UpdateEvent(model);
                    if (result)
                    {
                        SetAltert("Cập nhật sự kiện thành công", 0);
                        return RedirectToAction("Index", "Events");
                    }
                    else
                    {
                        SetAltert("Chưa cập nhật sự kiện được", 2);
                    }
                }
                else {
                    SetAltert("Sự kiện này đã tồn tại",2);
                }
            }
            else
            {

            }
            return View(model);
        }

        //Xem chi tiết sự kiện
        public ActionResult EventDetail(int id)
        {
            var dao = new EventsDAO();
            var model = dao.EventDetail(id);
            return View(model);
        }


        //Phương thức này dùng cho lời gọi Ajax từ trạng thái Events Index
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new EventsDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }

        //Phương thức này dùng cho lời gọi Ajax từ tophot Events Index
        [HttpPost]
        public JsonResult ChageTopHot(long id)
        {
            var result = new EventsDAO().ChageTopHot(id);
            return Json(new
            {
                status = result
            });
        }
    }
}