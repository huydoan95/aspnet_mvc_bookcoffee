using BookCoffee.Areas.Admin.Models;
using Models.DAO;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace BookCoffee.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProductDetails(long id) {
            var dao = new ProductDAO();
            int ID = Convert.ToInt32(id);
            var model = dao.productDetail(ID);
            //Update viewcount
            int viewcount = dao.currentViewcount(id);
            viewcount++;
            model.ViewCount = viewcount;
            dao.UpdateProduct(model);
            return View(model);
        }

        //Tạo menu các loại thực đơn
        public ActionResult menuProdCategory() {
            var dao = new ProductCategoryDAO();
            var model = dao.listProdCateClient();
            return PartialView(model);
        }

        //Phương thức lấy ra ID của 1 product thuộc top hot để gán hình lên slider khi trang load
        public JsonResult OneProductTopHot() {
            var dao = new ProductDAO();
            var model = dao.listProductBestSale(1, 1);
            long id=0;
            foreach (var item in model) {
                id = item.ID;
            }
            return Json(new {
                id = id
            });
        }


        //Load lên 3 product tophot phía dưới slider
        public ActionResult threeTophotProduct()
        {
            var dao = new ProductDAO();
            var model = dao.listProductBestSale(1, 3);
            return PartialView(model);
        }

        //Load hình lên slider khi open 
        public ActionResult loadImages(long? id)
        {
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
                ListimageProduct.ID = product.ID;
                ListimageProduct.images = listImageReturn;
                ListimageProduct.Name = product.Name;
                ListimageProduct.Price = product.Price;
                ListimageProduct.Description = product.Description;
                ListimageProduct.MetaTitle = product.MetaTitle;
            }
            catch (Exception ex)
            {

            }
            return PartialView(ListimageProduct);
        }

        //Lấy ra những sản phẩm mà ProductCategory có ShowOnHome là true để show lên trang client product index
        public ActionResult productsOfProdCateShowOnHome() {
            var dao = new ProductDAO();
            ViewBag.ProdCateName = new ProductCategoryDAO().ProCategoryShowOnHome().Name;
            var model = dao.productsOfProdCateforyShowOnHome(1,12);
            return PartialView(model);
        }


        //Tạo  danh sách các món mới
        public ActionResult lstNewProducts() {
            var dao = new ProductDAO();
            var model = dao.lstNewProducts(1, 3);//Lấy ra 3 product
            return PartialView(model);
        }

        //Tạo danh sách những món được ưa thích
        public ActionResult BestPreviewProduct() {
            var dao = new ProductDAO();
            var model = dao.BestPreviewProduct(3);
            return PartialView(model);
        }

        //Lấy ra danh dách product theo ProductCategor ID được truyền vào
        public ActionResult ProductByCategory(long id=0) {
            var model = new ProductDAO().ProductByCategory(id);
            return PartialView(model);
        }

    }
}