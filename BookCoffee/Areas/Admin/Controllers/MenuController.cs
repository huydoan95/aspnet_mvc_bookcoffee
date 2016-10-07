using Models.DAO;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookCoffee.Areas.Admin.Controllers
{
    public class MenuController : BaseController
    {
        // GET: Admin/Menu
        public ActionResult Index()
        {
            var dao = new MenuDAO();
            var model = dao.listMenu();
            return View(model);
        }

        //Phương thức này để khởi tạo cho Dropdownlist TypeID trong View CreateMenu
        public void SetViewBag(long? selectedID = null)
        {
            var dao = new MenuTypeDAO();
            ViewBag.TypeID = new SelectList(dao.listMenuType(), "id", "Name", selectedID);
        }

        //Tạo mới Menu
        [HttpGet]
        public ActionResult CreateMenu() {
            SetViewBag();
            return View();
        }
        [HttpPost]
        public ActionResult CreateMenu(Menu entity) {
            if (ModelState.IsValid)
            {
                //Kiểm tra xem đã chọn Target chưa
                if (!entity.Target.Equals("0"))
                {
                    //trường hợp đã chọn 
                    var dao = new MenuDAO();
                    //Kiểm tra sự tồn tại cửa Menu
                    bool Exist = dao.MenuExits(entity.Text);
                    if (!Exist)
                    {
                        //trường hợp chưa tồn tại
                        bool result = dao.CreateMenu(entity);
                        if (result)
                        {
                            SetAltert("Tạo Menu thành công", 1);
                            return RedirectToAction("Index", "Menu");
                        }
                        else {
                            //trường hợp chưa tạo được
                            SetAltert("Chưa tạo được Menu",2);
                        }
                    }
                    else {
                        //trường hợp Menu đã tồn tại
                        SetAltert("Menu này đã có", 2);
                    }
                }
                else {
                    //trường hợp chưa chọn
                    SetAltert("Vui lòng chọn kiểu mở trang", 2);
                }
            }
            else {

            }
            SetViewBag();
            return View(entity);
        }

        //Update Menu
        [HttpGet]
        public ActionResult UpdateMenu(int id) {
            var model = new MenuDAO().menuDetail(id);
            SetViewBag();
            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateMenu(Menu entity) {
            if (ModelState.IsValid)
            {
                //Kiểm tra xe đã chọn target chưa
                if (!entity.Target.Equals("0"))
                {
                    var dao = new MenuDAO();
                    //kiểm tra sự tồn tại của Menu trước khi Update
                    bool Exist = dao.MenuExitsForUpdate(entity.Text, entity.ID);
                    if (!Exist)
                    {
                        //Trường hợp Menu chưa tồn tại
                        bool result = dao.UpdateMenu(entity);
                        if (result)
                        {
                            SetAltert("Update Menu thành công",0);
                            return RedirectToAction("Index","Menu");
                        }
                        else {
                            SetAltert("Chưa Update được",1);
                        }
                    }
                    else {
                        //Trường hợp Menu đã tồn tại
                        SetAltert("Menu này đã có",2);
                    }
                }
                else {
                    SetAltert("Vui lòng chọn kiểu mở trang",2);
                }
            }
            else {

            }
            SetViewBag();
            return View(entity);
        }

        //Phương thức này dùng cho lời gọi Ajax từ trạng thái user trong trang Index
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new MenuDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}