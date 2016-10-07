using Models.DAO;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookCoffee.Areas.Admin.Controllers
{
    public class MenuTypeController : BaseController
    {
        // GET: Admin/MenuType
        public ActionResult Index()
        {
            var dao = new MenuTypeDAO();
            var model = dao.listMenuType();
            return View(model);
        }

        //Tạo mới MenuType
        [HttpGet]
        public ActionResult CreateMenuType()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateMenuType(MenuType model)
        {
            if (ModelState.IsValid)
            {
                var dao = new MenuTypeDAO();
                //Kiểm tra xem tên Menu này dã có chưa
                bool exist = dao.MenuTypeExist(model.Name);
                if (!exist)
                {
                    bool result = dao.CreateMenuType(model);
                    if (result)
                    {
                        SetAltert("Tạo MenuType thành công", 0);
                        return RedirectToAction("Index", "MenuType");
                    }
                    else
                    {
                        SetAltert("Chưa tạo MenuType được", 1);
                    }
                }
                else
                {
                    SetAltert("MenuType này đã có", 2);
                }

            }
            return View(model);
        }

        //Update MenuType
        [HttpGet]
        public ActionResult UpdateMenuType(int id) {
            var dao = new MenuTypeDAO();
            var model = dao.menuTypeDetail(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateMenuType(MenuType entity) {
            var dao = new MenuTypeDAO();
            bool exists = dao.MenuTypeExistforUpdate(entity.Name, entity.ID);
            if (!exists)
            {
                bool result = dao.UpdateMenuType(entity);
                if (result)
                {
                    SetAltert("Cập nhật MenuType thành công", 0);
                    return RedirectToAction("Index", "MenuType");
                }
                else {
                    SetAltert("Cập nhật không thành công",1);
                }
            }
            else {
                SetAltert("MenuType đã có", 2);
            }
            return View(entity);
        }

        //Phương thức đổi status cho MenuType gọi từ Ajax
        [HttpPost]
        public JsonResult ChangeStatus(long id) {
            var result = new MenuTypeDAO().ChangeStatus(id);
            return Json(new {
                status = result
            });
        }
    }
}