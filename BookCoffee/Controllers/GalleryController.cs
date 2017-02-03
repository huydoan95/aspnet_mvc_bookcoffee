using BookCoffee.Areas.Admin.Models;
using Models.DAO;
using Models.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace BookCoffee.Controllers
{
    public class GalleryController : Controller
    {
       // private readonly BookCoffeeShopDbContext db = new BookCoffeeShopDbContext();
        public ActionResult Index()
        {
            return View();
        }

        //Tạo slider (từ 1 gallery có tophot = true)
        public ActionResult slider() {
            var dao = new GalleryDAO();
            var TopHotGallery = dao.getTopHotGallery();
            //Lấy ra hình ảnh trong album không gian quán thuộc tophot
            EachGalleryImages model = new EachGalleryImages();
            List<string> listImageReturn = new List<string>();
            var images = TopHotGallery.ImageDetail;
            XElement xElement = XElement.Parse(images);
            foreach (XElement element in xElement.Elements())
            {
                listImageReturn.Add(element.Value);
            }
            model.ID = TopHotGallery.ID;
            model.images = listImageReturn;
            model.Name = TopHotGallery.Name;
            model.MetaTitle = TopHotGallery.MetaTitle;
            return PartialView(model);
        }

        //Tạo danh sách Gallery
        public ActionResult lstGallery() {
            var dao = new GalleryDAO();
            var model = dao.listGallery(2, 0);
            return PartialView(model);
        }

    }
}
