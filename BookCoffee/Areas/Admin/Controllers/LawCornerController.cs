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
    public class LawCornerController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index()
        {
            var dao = new LawCornerDAO();
            var model = dao.listLawCorner();
            return View(model);
        }


        //Tạo mới tin cafe luật
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(LawCorner model)
        {
            if (ModelState.IsValid)
            {
                var dao = new LawCornerDAO();
                //Lấy ra user đăng nhập để gán vào CreatedBY
                UserLogin userlogin = (UserLogin)Session["USER_SESSION"];
                model.CreatedBy = userlogin.UserName;
                bool exits = dao.LawCornerExist(model.Name);
                if (!exits)
                {
                    //Nếu chọn tin này là TopHot
                    if (model.TopHot == true)
                    {
                        bool TopHot = dao.TopHotForCreate();
                        if (!TopHot)
                        {
                            string metatitle = CastString.Cast(model.Name);
                            model.MetaTitle = metatitle;
                            model.CreatedDate = DateTime.Now;
                            bool result = dao.CreateLawCorner(model);
                            if (result)
                            {
                                SetAltert("Tạo tin  thành công", 0);
                                return RedirectToAction("Index", "LawCorner");
                            }
                            else
                            {
                                SetAltert("Tạo mới không thành công", 2);
                            }
                        }
                        else
                        {
                            SetAltert("Tạm thời không thể thiết lập TopHot cho ti này, vì hiện đang có 1 tin thuộc TopHot", 2);
                        }
                    }
                    else
                    {
                        string metatitle = CastString.Cast(model.Name);
                        model.MetaTitle = metatitle;
                        model.CreatedDate = DateTime.Now;
                        bool result = dao.CreateLawCorner(model);
                        if (result)
                        {
                            SetAltert("Tạo mới tin thành công", 0);
                            return RedirectToAction("Index", "LawCorner");
                        }
                        else
                        {
                            SetAltert("Tạo mới không thành công", 2);
                        }
                    }
                }
                else
                {
                    SetAltert("Tiêu đề này đã có", 2);
                }

            }
            return View(model);
        }

        //Xóa sản phẩm
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new LawCornerDAO().DeleteLawCorner(id);
            return RedirectToAction("Index");

        }

        //Update sản phẩm
        [HttpGet]
        public ActionResult UpdateLawCorner(long id)
        {
            var dao = new LawCornerDAO();
            var model = dao.LawCornerDetail(id);
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateLawCorner(LawCorner model)
        {
            if (ModelState.IsValid)
            {
                var dao = new LawCornerDAO();
                UserLogin userlogin = (UserLogin)Session["USER_SESSION"];
                model.ModifiedBy = userlogin.UserName;
                //Kiểm tra xem tên sách này đã có chưa
                bool exist = dao.LawCornerExistForUpdate(model.Name, model.ID);
                if (!exist)
                {
                    //Trường hợp chọn sách là Tophot
                    if (model.TopHot == true)
                    {
                        bool tophot = dao.TopHotForUpdate(model.ID);
                        //Kiểm tra xem hiện tại đang có sách nào thuộc TopHot chưa
                        if (!tophot)
                        {
                            string metatitle = CastString.Cast(model.Name);
                            model.MetaTitle = metatitle;
                            bool result = dao.UpdateLawCorner(model);
                            if (result)
                            {
                                SetAltert("Cập nhật thành công", 0);
                                return RedirectToAction("Index", "LawCorner");
                            }
                            else
                            {
                                SetAltert("Cập nhật không thành công", 2);
                            }
                        }
                        else
                        {
                            SetAltert("Tạm thời không thể thiết lập TopHot cho tin này, vì hiện đang có 1 tin thuộc TopHot", 2);
                        }
                    }
                    else
                    {
                        string metatitle = CastString.Cast(model.Name);
                        model.MetaTitle = metatitle;
                        bool result = dao.UpdateLawCorner(model);
                        if (result)
                        {
                            SetAltert("Cập nhật thành công", 0);
                            return RedirectToAction("Index", "LawCorner");
                        }
                        else
                        {
                            SetAltert("Cập nhật không thành công", 2);
                        }
                    }

                }
                else
                {
                    SetAltert("Tiêu đề tin này đã có", 2);
                }
            }
            else
            {

            }
            return View(model);
        }

        //Xem chi tiết sản phẩm
        public ActionResult LawCornerDetail(int id)
        {
            var dao = new LawCornerDAO();
            var model = dao.LawCornerDetail(id);
            return View(model);
        }


        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new LawCornerDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }

        [HttpPost]
        public JsonResult ChangeTopHot(long id)
        {
            var result = new LawCornerDAO().ChangeTopHot(id);
            return Json(new
            {
                status = result
            });
        }


    }
}