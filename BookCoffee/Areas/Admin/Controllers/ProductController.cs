using BookCoffee.Common;
using Models.DAO;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml.Linq;
using BookCoffee.Areas.Admin.Models;

namespace BookCoffee.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index()
        {
            var dao = new ProductDAO();
            var model = dao.listProductView();
            return View(model);
        }



        //Phương thức này để khởi tạo cho Dropdownlist CategoryID trong View CreateProduct
        public void SetViewBag(long? selectedID = null)
        {
            var dao = new ProductCategoryDAO();
            ViewBag.CategoryID = new SelectList(dao.listProdCategory(), "id", "Name", selectedID);
        }
 
        //Tạo mới sản phẩm
        [HttpGet]
        public ActionResult CreateProduct() {
            SetViewBag();
            return View();
        }
        [HttpPost]
        public ActionResult CreateProduct(Product model) {
            if (ModelState.IsValid) {
                var dao = new ProductDAO();
                bool exits = dao.ProductExist(model.Name);
                if (!exits)
                {
                    //Lấy ra user đăng nhập để gán vào CreatedBY
                    UserLogin userlogin = (UserLogin)Session["USER_SESSION"];
                    model.CreatedBy = userlogin.UserName;
                    string metatitle = CastString.Cast(model.Name);
                    model.MetaTitle = metatitle;
                    bool result = dao.CreateProduct(model);
                    if (result)
                    {
                        return RedirectToAction("Index", "Product");
                    }
                    else
                    {
                        ModelState.AddModelError("CreateProductError", "Tạo mới không thành công");
                    }
                }
                else {
                    ModelState.AddModelError("CreateProductError", "Tên sản phẩm này đã có");
                }
                
            }
            SetViewBag();
            return View(model);
        }

        //Xóa sản phẩm
        [HttpDelete]
        public ActionResult DeleteProduct(int id)
        {
            new ProductDAO().DeleteProduct(id);
            return RedirectToAction("Index");

        }

        //Update sản phẩm
        [HttpGet]
        public ActionResult UpdateProduct(int id) {
            var dao = new ProductDAO();
            var model = dao.productDetail(id);
            SetViewBag();
            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateProduct(Product model) {
            if (ModelState.IsValid)
            {
                var dao = new ProductDAO();
                UserLogin userlogin = (UserLogin)Session["USER_SESSION"];
                model.ModifiedBy = userlogin.UserName;
                string metatitle = CastString.Cast(model.Name);
                model.MetaTitle = metatitle;
                bool result = dao.UpdateProduct(model);
                if (result)
                {
                    return RedirectToAction("Index","Product");
                }
                else {
                    ModelState.AddModelError("UpdateProductError","Cập nhật chưa thành công");
                }
            }
            else {

            }
            SetViewBag();
            return View(model);
        }

        //Xem chi tiết sản phẩm
        public ActionResult ProductDetail(int id) {
            var dao = new ProductDAO();
            var model = dao.productDetail(id);
            return View(model);
        }


        //Phương thức này dùng cho lời gọi Ajax từ trạng thái user trong trang Index
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new ProductDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }

        //Phương thức này dùng cho lời gọi Ajax từ trạng thái user trong trang Index
        [HttpPost]
        public JsonResult ChageTopHot(long id)
        {
            var result = new ProductDAO().ChageTopHot(id);
            return Json(new
            {
                status = result
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="images"></param>
        /// <returns></returns>
        public JsonResult saveImages(long id,string images) {
            //tên biến phải trùng với tên data truyền qua từ Ajax

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var listImages = serializer.Deserialize<List<string>>(images);
            XElement xElement = new XElement("Images");
            foreach (var item in listImages) {
                xElement.Add(new XElement("image",item));
            }
            try
            {
                var dao = new ProductDAO();
                dao.updateImage(id, xElement.ToString());
                return Json(new
                {
                    status = true
                });
            }
            catch (Exception ex) {
                return Json(new
                {
                    status = false
                });
            }
           
        }

        public JsonResult LoadImage(long id) {
            Product product = new ProductDAO().productDetail(Convert.ToInt32(id));
            var images = product.MoreImage;
            XElement xElement = XElement.Parse(images);
            List<string> listImageReturn = new List<string>();
            foreach (XElement element in xElement.Elements()) {
                listImageReturn.Add(element.Value);
            }
            return Json(new {
                data = listImageReturn
            },JsonRequestBehavior.AllowGet);
        }


       
        public ActionResult loadImages(long ? id) {
            Product product = new ProductDAO().productDetail(Convert.ToInt32(id));
            listImagesProductModel ListimageProduct = new listImagesProductModel();
            List<string> listImageReturn = new List<string>();
            try
            {
                var images = product.MoreImage;
                XElement xElement = XElement.Parse(images);
               
                foreach (XElement element in xElement.Elements())
                {
                    listImageReturn.Add(element.Value);
                }
                ListimageProduct.images = listImageReturn;
            }
            catch (Exception ex) {

            }
            return PartialView(ListimageProduct);
        }


        //Phương thức dùng thử nghiem
        [HttpGet]
        public ActionResult test()
        {
            return View();
        }
        [HttpPost]
        public ActionResult test(Product entity)
        {
            var dao = new ProductDAO();
            bool result = dao.create(entity);
            return View();
        }

    }
}