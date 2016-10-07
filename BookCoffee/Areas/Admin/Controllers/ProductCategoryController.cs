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
    public class ProductCategoryController : BaseController
    {
        // GET: Admin/ProductCategory
        public ActionResult Index()
        {
            var dao = new ProductCategoryDAO();
            var listProductCategory = dao.listProdCategory();
            return View(listProductCategory);
        }


        //Tạo mới loại sản phẩm 
        [HttpGet]
        public ActionResult CreateProductCategory() {

            return View();
        }
        [HttpPost]
        public ActionResult CreateProductCategory(ProductCategory model) {
            if (ModelState.IsValid)
            {
                //Kiểm tra xem tên loại sản phẩm đã có chư
                var dao = new ProductCategoryDAO();
                bool exsits = dao.ProductCategoryExist(model.Name);
                if (!exsits)
                {
                    UserLogin userlogin = (UserLogin)Session["USER_SESSION"];
                    model.CreatedBy = userlogin.UserName;
                    model.CreatedDate = DateTime.Now;
                    string metatitle = CastString.Cast(model.Name);
                    model.MetaTitle = metatitle;
                    bool result = dao.CreateProductCategory(model);
                    if (result)
                    {
                        return RedirectToAction("Index", "ProductCategory");
                    }
                    else
                    {
                        ModelState.AddModelError("CreateProdCategoryError", "Tạo mới không thành công");
                    }
                }
                else {
                    //trường hợp tên đã có
                    ModelState.AddModelError("CreateProdCategoryError", "Tên loại sản phẩm này đã có");
                }
               
            }
            else {

            }
            return View(model);
        }

        //Update loại sản phẩm
        [HttpGet]
        public ActionResult UpdateProductCategory(int id) {
            var dao = new ProductCategoryDAO();
            var model = dao.productCategoryDetail(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateProductCategory (ProductCategory model) {
            if (ModelState.IsValid)
            {
                var dao = new ProductCategoryDAO();
                UserLogin userlogin = (UserLogin)Session["USER_SESSION"];
                model.ModifiedBy = userlogin.UserName;
                string metatitle = CastString.Cast(model.Name);
                model.MetaTitle = metatitle;
                bool result = dao.UpdateProductCategory(model);
                if (result)
                {
                    return RedirectToAction("Index", "ProductCategory");
                }
                else
                {
                    ModelState.AddModelError("UpdateProdCategoryError", "Chưa cập nhật được");
                }
            }
            else {

            }

            return View(model);
        }

        //Xóa loại  sản phẩm
        [HttpDelete]
        public ActionResult DeleteProductCategory(int id)
        {
            new ProductCategoryDAO().DeleteProductCategory(id);
            return RedirectToAction("Index");

        }


        //Hiển thị chi tiết loại sản phẩm
        public ActionResult ProductCategoryDetail(int id) {
            var dao = new ProductCategoryDAO();
            var model = dao.productCategoryDetail(id);
            return View(model);
        }

    }
}