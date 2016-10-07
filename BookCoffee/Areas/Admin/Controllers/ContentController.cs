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
    public class ContentController : BaseController
    {
        // GET: Admin/Content
        public ActionResult Index()
        {
            var dao = new ContentDAO();
            List<Content> model = dao.listContentAdmin();
            return View(model);
        }


        [HttpGet]
        public ActionResult CreateContent()
        {
            SetViewBag();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateContent(Content model)
        {
            var dao = new ContentDAO();
            if (ModelState.IsValid)
            {
                //Trường hợp chọn tin là TopHot
                if (model.TopHot == true)
                {
                    //Kiểm tra xem hiện tại đang có tin nào thuộc TopHot không
                    bool TopHotExist = dao.TopHotForCreate();
                    if (!TopHotExist)
                    {
                        //Lấy ra username đang sử dụng
                        UserLogin userlogin = (UserLogin)Session["USER_SESSION"];
                        model.CreatedBy = userlogin.UserName;
                        string metatitle = CastString.Cast(model.Name);
                        model.MetaTitle = metatitle;
                        long id = dao.CreateContent(model);
                        if (id > 0)
                        {
                            SetAltert("Tạo mới tin tức thành công", 0);
                            return RedirectToAction("Index", "Content");
                        }
                        else
                        {
                            SetAltert("Chưa tạo mới được tin tức, vui lòng thử lại", 2);
                        }
                    }
                    else
                    {
                        SetAltert("Tạm thời không thể thiết lập tin này là TopHot, vì đang có tin TopHop", 2);
                    }
                }
                else
                {
                    //Lấy ra username đang sử dụng
                    UserLogin userlogin = (UserLogin)Session["USER_SESSION"];
                    model.CreatedBy = userlogin.UserName;
                    long id = dao.CreateContent(model);
                    if (id > 0)
                    {
                        SetAltert("Tạo mới tin tức thành công", 0);
                        return RedirectToAction("Index", "Content");
                    }
                    else
                    {
                        SetAltert("Chưa tạo mới được tin tức, vui lòng thử lại", 2);
                    }
                }

            }

            SetViewBag();
            return View();
        }

        //Phương thức này để khởi tạo cho Dropdownlist CategoryID trong View CreateContent
        public void SetViewBag(long? selectedID = null)
        {
            var dao = new CategoryDAO();
            ViewBag.CategoryID = new SelectList(dao.listAllCategory(), "id", "Name", selectedID);
        }


        //Xóa bản tin
        [HttpDelete]
        public ActionResult DeleteContent(int id)
        {
            new ContentDAO().DeleteContent(id);
            return RedirectToAction("Index");

        }



        //Cập nhật nội dung tin tức
        [HttpGet]
        public ActionResult UpdateContent(int id)
        {
            var model = new ContentDAO().ContentDetail(id);
            SetViewBag();
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateContent(Content model)
        {

            if (ModelState.IsValid)
            {
                var dao = new ContentDAO();
                if (model.TopHot == true)
                {
                    bool TopHotExist = dao.TopHotForUpdate(model.ID);
                    if (!TopHotExist)
                    {
                        UserLogin userlogin = (UserLogin)Session["USER_SESSION"];
                        model.ModifiedBy = userlogin.UserName;
                        model.ModifiedDate = DateTime.Now;
                        string metatitle = CastString.Cast(model.Name);
                        model.MetaTitle = metatitle;
                        bool result = dao.UpdateContent(model);
                        if (result)
                        {
                            SetAltert("Cập nhật thành công", 0);
                            return RedirectToAction("Index", "Content");
                        }
                        else
                        {
                            //Trường hợp cập nhật không thành công
                            SetAltert("Cập nhật không thành công, vui lòng thử lại", 2);
                        }
                    }
                    else
                    {
                        SetAltert("Tạm thời không thể thiết lập tin này là TopHot, vì đang có tin TopHop", 2);
                    }
                }
                else
                {
                    UserLogin userlogin = (UserLogin)Session["USER_SESSION"];
                    model.ModifiedBy = userlogin.UserName;
                    model.ModifiedDate = DateTime.Now;
                    string metatitle = CastString.Cast(model.Name);
                    model.MetaTitle = metatitle;
                    bool result = dao.UpdateContent(model);
                    if (result)
                    {
                        SetAltert("Cập nhật thành công", 0);
                        return RedirectToAction("Index", "Content");
                    }
                    else
                    {
                        //Trường hợp cập nhật không thành công
                        SetAltert("Cập nhật không thành công, vui lòng thử lại", 2);
                    }
                }

            }
            else
            {

            }

            SetViewBag();
            return View();
        }


        //HIển thị chi tiết tin
        public ActionResult ContentDetail(int id)
        {
            var model = new ContentDAO().ContentDetail(id);
            return View(model);
        }

    }
}