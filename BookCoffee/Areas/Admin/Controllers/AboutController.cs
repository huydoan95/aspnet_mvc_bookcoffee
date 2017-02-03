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
    public class AboutController : BaseController
    {
        // GET: Admin/About
        public ActionResult Index()
        {
            var dao = new AboutDAO();
            var model = dao.lstAboutAdmin();
            return View(model);
        }


        //Tạo bài giới thiệu mới
        [HttpGet]
        public ActionResult CreateAbout()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateAbout(About model)
        {
            if (ModelState.IsValid)
            {
                var dao = new AboutDAO();
                //Lấy ra user đăng nhập để gán vào CreatedBY
                UserLogin userlogin = (UserLogin)Session["USER_SESSION"];
                model.CreatedBy = userlogin.UserName;
                bool exits = dao.AboutExistForCreate(model.Name);
                if (!exits)
                {
                    string metatitle = CastString.Cast(model.Name);
                    model.MetaTitle = metatitle;
                    model.CreatedDate = DateTime.Now;
                    bool result = dao.CreateABout(model);
                    if (result)
                    {
                        SetAltert("Tạo mới bài giới thiệu thành công", 0);
                        return RedirectToAction("Index", "About");
                    }
                    else
                    {
                        SetAltert("Tạo mới không thành công", 2);
                    }
                }
                else
                {
                    SetAltert("Bài giới thiệu này đã có", 2);
                }

            }
            return View(model);
        }

        //Xóa sản phẩm
        [HttpDelete]
        public ActionResult DeleteAbout(long id)
        {
            new AboutDAO().DeleteAbout(id);
            return RedirectToAction("Index");

        }

        //Update about
        [HttpGet]
        public ActionResult UpdateAbout(long id)
        {
            var dao = new AboutDAO();
            var model = dao.aboutDetail(id);
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateAbout(About model)
        {
            if (ModelState.IsValid)
            {
                var dao = new AboutDAO();
                UserLogin userlogin = (UserLogin)Session["USER_SESSION"];
                model.ModifiedBy = userlogin.UserName;
                //Kiểm tra xem tên sách này đã có chưa
                bool exist = dao.AboutExistForUpdate(model.Name, model.ID);
                if (!exist)
                {
                    string metatitle = CastString.Cast(model.Name);
                    model.MetaTitle = metatitle;
                    bool result = dao.UpdateAbout(model);
                    if (result)
                    {
                        SetAltert("Cập nhật thành công", 0);
                        return RedirectToAction("Index", "ABout");
                    }
                    else
                    {
                        SetAltert("Cập nhật không thành công", 2);
                    }
                }
                else
                {
                    SetAltert("Bài giới thiệu này đã có", 2);
                }
            }
            else
            {

            }
            return View(model);
        }

        //Xem chi tiết bài giơi thiệu
        public ActionResult AboutDetail(long id)
        {
            var dao = new AboutDAO();
            var model = dao.aboutDetail(id);
            return View(model);
        }

        //Phương thức này dùng cho lời gọi Ajax từ trạng thái user trong trang Index
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new AboutDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }

    }
}