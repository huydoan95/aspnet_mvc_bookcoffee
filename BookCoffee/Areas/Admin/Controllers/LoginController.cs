using BookCoffee.Areas.Admin.Models;
using BookCoffee.Common;
using Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using encode;

namespace BookCoffee.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {// GET: /Admin/Login/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                //Khởi dạo đối tượng mã hóa
                Endcode_Decode ed = new Endcode_Decode();
                //Mã hóa password để so sánh
                var EnCryptedPassword = ed.EncodePassword(model.UserName, model.Password);
                //Gọi chức năng login trong UserDAO
                var Result = dao.login(model.UserName, EnCryptedPassword);
                if (Result == 1)
                {
                    var user = dao.GetUserID(model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.userID = user.ID;
                    Session.Add(Common.CommonConstants.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }
                else if (Result == 0)
                {
                    ModelState.AddModelError("loginfail", "Username không tồn tại");
                }
                else if (Result == -1)
                {
                    ModelState.AddModelError("loginfail", "Username đã bị khóa");
                }
                else if (Result == -2)
                {
                    ModelState.AddModelError("loginfail", "Password không đúng");
                }
                else
                {
                    ModelState.AddModelError("loginfail", "Đăng nhập không thành công");
                }

            }
            else
            {
                //ModelState.AddModelError("loginfail", "Model is Invalid");
            }
            return View("Index");

        }

    }
}