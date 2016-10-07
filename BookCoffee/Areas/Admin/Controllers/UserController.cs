using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.EF;
using Models.DAO;
using BookCoffee.Common;
using encode;

namespace BookCoffee.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        //// GET: Admin/User
        //public ActionResult Index(string searchString,int page = 1, int pagesize = 1)
        //{
        //    var dao = new UserDAO();
        //    var model = dao.ListAllPaging(searchString,page, pagesize);
        //    //Tạo viewBag để giữ lại nội dung trên textbox search và để làm tham số cho phân trang
        //    ViewBag.SearchString = searchString;
        //    return View(model);
        //}

        public ActionResult Index() {
            var dao = new UserDAO();
            var model = dao.listUsers();
            return View(model);
        }


        //Tạo  mới người dùng
        [HttpGet]
        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateUser(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                //Kiểm tra xem Username này đã có trong CSDL chưa
                if (dao.UserExists(user.UserName) == false)
                {
                    //Khởi tạo đối tượng mã hóa
                    Endcode_Decode ed = new Endcode_Decode();
                    //Mã hóa password trước khi insert
                    string encryptedPassword = ed.EncodePassword(user.UserName, user.Password);
                    user.Password = encryptedPassword;

                    long id = dao.CreateUser(user);
                    if (id > 0)
                    {
                        //Gọi phương thức SetAlert của BaseController để hiển thị thông báo
                        SetAltert("Tạo người dùng thành công", 0);
                        return RedirectToAction("Index", "User");
                    }
                    else
                    {
                        ModelState.AddModelError("CreateUserErrors", "Chưa tạo được user, vui lòng kiểm tra lại ");
                    }
                }
                else
                {
                    //Trường hợp đã tồn tại Username
                    ModelState.AddModelError("CreateUserErrors", "Username nảy đã tồn tại, chọn Username khác! ");
                }

            }
            else
            {
                //ModelState.AddModelError("CreateUserErrors", "Thông tin user không hợp lệ, vui lòng kiểm tra lại ");
            }
            return View("CreateUser");
        }

        //Chỉnh sửa thông tin người dùng

        [HttpGet]
        public ActionResult EditUser(int id)
        {
            Endcode_Decode ed = new Endcode_Decode();
            //Decode password để trả lại ra view trước khi update thông tin user
            var user = new UserDAO().ViewUserDetail(id);
            user.Password = ed.DecodePassword(user.UserName, user.Password);
            return View(user);
        }

        [HttpPost]
        public ActionResult EditUser(User user)
        {
            //Khởi tạo đối tượng UserDAO
            var dao = new UserDAO();
            //Khởi tạo đối tượng mã hóa
            Endcode_Decode ed = new Endcode_Decode();
            if (ModelState.IsValid)
            {                
                //Mã hóa password trước khi update
                string encryptedPassword = ed.EncodePassword(user.UserName, user.Password);
                user.Password = encryptedPassword;
                bool result = dao.UpdateUser(user);
                if (result)
                {
                    //Gọi phương thức SetAlert của BaseController để hiển thị thông báo
                    SetAltert("Cập nhật người dùng thành công", 0);
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("EditUserErrors", "Cập nhật không thành công, vui lòng kiểm tra lại ");
                    user.Password = ed.DecodePassword(user.UserName, user.Password);
                    return View(user);
                }

            }
            else
            {
                return View(user);
            } 
        }

        //Xóa người dùng

        [HttpDelete]
        public ActionResult DeleteUser(int id) {
            new UserDAO().DeleteUser(id);
            //Gọi phương thức SetAlert của BaseController để hiển thị thông báo
            SetAltert("Đã xóa người dùng", 2);
            return RedirectToAction("Index");
        }


        //hiển thị chi tiết người dùng
        public ActionResult UserDetails(int id) {
            var user = new UserDAO().ViewUserDetail(id);
            return View(user);
        }


        //Phương thức này dùng cho lời gọi Ajax từ trạng thái user trong trang Index
        [HttpPost]
        public JsonResult ChangeStatus(long id) {
            var result = new UserDAO().ChangeStatus(id);
            return Json(new {
                status = result
            });
        }
    }
}