using BookCoffee.Common;
using Models.DAO;
using Models.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookCoffee.Areas.Admin.Controllers
{
    public class GalleryController : BaseController
    {
        private readonly BookCoffeeShopDbContext db = new BookCoffeeShopDbContext();
        public ActionResult Index(string searchString,int page = 1, int pagesize = 20)
        {
            var dao = new GalleryDAO();
            var model = dao.ListAllPaging(searchString, page, pagesize);
            return View(model);

            //var imagesModel = new ImageGallery();
            //var imageFiles = Directory.GetFiles(Server.MapPath("~/Data/Galleries/"));
            //foreach (var item in imageFiles)
            //{
            //    imagesModel.ImageList.Add(Path.GetFileName(item));
            //}
            //return View(list);
        }
        [HttpGet]
        public ActionResult UploadImage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadImageMethod()
        {
            UserLogin userlogin = (UserLogin)Session["USER_SESSION"];
            if (Request.Files.Count != 0)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase file = Request.Files[i];
                    int fileSize = file.ContentLength;
                    string fileName = file.FileName;
                    file.SaveAs(Server.MapPath("/Data/Galleries/" + fileName));
                    ImageGallery imageGallery = new ImageGallery();
                    //imageGallery.ID = Guid.NewGuid();
                    imageGallery.Name = fileName;
                    imageGallery.CreatedDate = DateTime.Now;
                    imageGallery.CreatedBy = userlogin.UserName;
                    imageGallery.ImagePath = "/Data/Galleries/" + fileName;
                    db.ImageGalleries.Add(imageGallery);
                    db.SaveChanges();
                }
                return Content("Success");
            }
            return Content("failed");
        }
    }
}
