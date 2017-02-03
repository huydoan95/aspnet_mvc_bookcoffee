using BookCoffee.Common;
using Models.DAO;
using Models.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Linq;

namespace BookCoffee.Areas.Admin.Controllers
{
    public class GalleryController : BaseController
    {
        private readonly BookCoffeeShopDbContext db = new BookCoffeeShopDbContext();
        //public ActionResult Index(string searchString,int page = 1, int pagesize = 10)
        //{
        //    var dao = new GalleryDAO();
        //    var model = dao.ListAllPaging(searchString, page, pagesize);
        //    return View(model);

        //    //var imagesModel = new ImageGallery();
        //    //var imageFiles = Directory.GetFiles(Server.MapPath("~/Data/Galleries/"));
        //    //foreach (var item in imageFiles)
        //    //{
        //    //    imagesModel.ImageList.Add(Path.GetFileName(item));
        //    //}
        //    //return View(list);
        //}
        public ActionResult Index()
        {
            var dao = new GalleryDAO();
            var model = dao.listGallery(0, 0);
            return View(model);
        }

        //Tạo mới không gian quán (Album)
        [HttpGet]
        public ActionResult CreateGallery()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateGallery(ImageGallery model)
        {
            if (ModelState.IsValid)
            {
                var dao = new GalleryDAO();
                //Lấy ra user đăng nhập để gán vào CreatedBY
                UserLogin userlogin = (UserLogin)Session["USER_SESSION"];
                model.CreatedBy = userlogin.UserName;
                bool exits = dao.GalleryExist(model.Name);
                if (!exits)
                {
                    //Nếu chọn sách này là TopHot
                    if (model.TopHot == true)
                    {
                        bool TopHot = dao.TopHotForCreate();
                        if (!TopHot)
                        {
                            model.CreatedDate = DateTime.Now;
                            bool result = dao.CreateGallery(model);
                            if (result)
                            {
                                SetAltert("Tạo mới sách thành công", 0);
                                return RedirectToAction("Index", "Gallery");
                            }
                            else
                            {
                                SetAltert("Tạo mới không thành công", 2);
                            }
                        }
                        else
                        {
                            SetAltert("Tạm thời không thể thiết lập TopHot cho album này, vì hiện đang có 1 album thuộc TopHot", 2);
                        }
                    }
                    else
                    {
                        model.CreatedDate = DateTime.Now;
                        bool result = dao.CreateGallery(model);
                        if (result)
                        {
                            SetAltert("Tạo mới sách thành công", 0);
                            return RedirectToAction("Index", "Gallery");
                        }
                        else
                        {
                            SetAltert("Tạo mới không thành công", 2);
                        }
                    }
                }
                else
                {
                    SetAltert("Tên album này đã có", 2);
                }

            }
            return View(model);
        }

        //Update album không gian quán
        [HttpGet]
        public ActionResult UpdateGallery(long id) {
            var dao = new GalleryDAO();
            var model = dao.imageGalleryDetail(id);
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateGallery(ImageGallery entity) {
            var dao = new GalleryDAO();
            UserLogin userlogin = (UserLogin)Session["USER_SESSION"];
            entity.ModifiedBy = userlogin.UserName;
            bool result;
            if (ModelState.IsValid) {
                if (entity.TopHot == true)
                {
                    bool Existtophot = dao.TopHotForUpdate(entity.ID);
                    if (!Existtophot)
                    {
                        result = dao.UpdateGallery(entity);
                        if (result)
                        {
                            SetAltert("Cập nhật thành công", 0);
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            SetAltert("Chưa cập nhật được", 2);
                        }
                    }
                    else {
                        SetAltert("Không thiết lập Top Hot cho Album này được, vì hiện tại đang có 1 album thuộc Top Hot",2);
                    }
                }
                else {
                    result = dao.UpdateGallery(entity);
                    if (result)
                    {
                        SetAltert("Cập nhật thành công", 0);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        SetAltert("Chưa cập nhật được", 2);
                    }
                }
            }
            return View(entity);
        }



        public ActionResult GalleryDetail(long id)
        {
            var model = new GalleryDAO().imageGalleryDetail(id);
            return View(model);
        }

        //Phương thức upload hình cho từng album
        [HttpPost]
        public ActionResult UploadImageMethod()
        {
            if (Request.Files.Count != 0)
            {
                long id = Convert.ToInt64(Request["id"]);
                //lấy ra gallery cần upload hình
                var gallery = new GalleryDAO().imageGalleryDetail(id);
                //lấy ra tên album để làm folder và remove khoảng trắng
                var folder = CastString.Cast(gallery.Name.ToString());

                //Path lưu hình
                string path = "/Data/Galleries/" + folder + "/";

                List<string> listimage = new List<string>();
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase file = Request.Files[i];
                    int fileSize = file.ContentLength;
                    string fileName = file.FileName;
                    string extension = System.IO.Path.GetExtension(fileName);
                    string fileNameWithoutExt = fileName.Substring(0, fileName.Length - extension.Length);
                    fileNameWithoutExt = CastString.Cast(fileNameWithoutExt);
                    listimage.Add(path + fileNameWithoutExt + extension);
                }
               
                XElement xElement;
                //Kiểm tra xem hiện tại đang có hình chưa
                if (gallery.ImageDetail != null)
                {
                    //Nếu hiện tai đang có hình thìử dụng Root hiện tai
                    xElement = XElement.Parse(gallery.ImageDetail);
                }
                else
                {
                    //Nếu chưa có hình nào thì tạo Root là Images
                    xElement = new XElement("Images");
                }

                foreach (var item in listimage)
                {
                    xElement.Add(new XElement("image", item));
                }
                try
                {
                    var dao = new GalleryDAO();
                    bool result = dao.updateImage(id, xElement.ToString());
                    //Kiểm tra nếu như Add vào database thành công thì mới lưu hình lên server
                    if (result)
                    {
                        //kiểm tra xem hiện có folder nào chưa
                        bool exists = System.IO.Directory.Exists(Server.MapPath(path));
                        //Nếu đã có thì upload hình lên foler server
                        if (exists)
                        {
                            for (int i = 0; i < Request.Files.Count; i++)
                            {
                                HttpPostedFileBase file = Request.Files[i];
                                int fileSize = file.ContentLength;
                                string fileName = file.FileName;
                                string extension = System.IO.Path.GetExtension(fileName);
                                string fileNameWithoutExt = fileName.Substring(0, fileName.Length - extension.Length);
                                fileNameWithoutExt = CastString.Cast(fileNameWithoutExt);
                                file.SaveAs(Server.MapPath(path + fileNameWithoutExt + extension));
                            }
                        }
                        else {
                            //Nếu chưa có thì tạo folder với tên folder là tên Album
                            System.IO.Directory.CreateDirectory(Server.MapPath(path));
                            //Sau khi tạo folder xong, upload hình vào
                            for (int i = 0; i < Request.Files.Count; i++)
                            {
                                HttpPostedFileBase file = Request.Files[i];
                                int fileSize = file.ContentLength;
                                string fileName = file.FileName;
                                string extension = System.IO.Path.GetExtension(fileName);
                                string fileNameWithoutExt = fileName.Substring(0, fileName.Length - extension.Length);
                                fileNameWithoutExt = CastString.Cast(fileNameWithoutExt);
                                file.SaveAs(Server.MapPath(path + fileNameWithoutExt + extension));
                            }
                        }
                        
                    }
                    return Content("ok");
                }
                catch (Exception ex)
                {
                    return Content("failed");
                }

            }
            return Content("failed");
        }

        //public JsonResult saveImages(long id, string images)
        //{
        //    //tên biến phải trùng với tên data truyền qua từ Ajax

        //    JavaScriptSerializer serializer = new JavaScriptSerializer();
        //    var listImages = serializer.Deserialize<List<string>>(images);
        //    XElement xElement = new XElement("Images");
        //    foreach (var item in listImages)
        //    {
        //        xElement.Add(new XElement("image", item));
        //    }
        //    try
        //    {
        //        var dao = new GalleryDAO();
        //        dao.updateImage(id, xElement.ToString());
        //        return Json(new
        //        {
        //            status = true
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new
        //        {
        //            status = false
        //        });
        //    }

        //}






        //Xóa Album không gian quán
        public ActionResult DeleteGallery(long id)
        {
            //Lấy ra 1 dòng Gallery theo ID truyền vào
            var dao = new GalleryDAO();
            ImageGallery imageGallery = dao.imageGalleryDetail(id);
            string imagePath = Request.MapPath(imageGallery.ImagePath);
            if (System.IO.File.Exists(imagePath))
            {
                //Xóa luôn hình đại diện của Album trên server
                System.IO.File.Delete(imagePath);
            }
            //lấy ra tên album để làm folder và remove khoảng trắng
            var folder = CastString.Cast(imageGallery.Name.ToString());

            //tìm folder theo đường dẫn
            string path = "/Data/Galleries/" + folder;
            bool exists = System.IO.Directory.Exists(Server.MapPath(path));
            if (exists) {
                //Xóa Folder chứa hình chi của album
                DirectoryInfo dir = new DirectoryInfo(Server.MapPath(path));
                dir.GetFiles("*", SearchOption.AllDirectories).ToList().ForEach(file => file.Delete());
                Directory.Delete(Server.MapPath(path));
            }

            //Xóa Album trong Database
            dao.DeleteGallery(id);
            return RedirectToAction("Index");

        }

        //Xóa hình trong Album không gian quán theo hình được chọn
        public ActionResult DeleteImage(long id, string imageName)
        {
            var gallery = new GalleryDAO().imageGalleryDetail(id);
            XElement xelement = XElement.Parse(gallery.ImageDetail);
            XElement newxelement = new XElement("Images");
            var nodes = xelement.Descendants("image").ToList();
            foreach (var node in nodes) {
                if (node.Value != imageName)
                {
                    //tìm ra những element nào mà value không phải là hình cần xóa add vào newelement
                    newxelement.Add(node);
                }
            }
            var dao = new GalleryDAO();
            //Update lại field ImageDetail
            bool result = dao.updateImage(id,newxelement.ToString());
            if (result)
            {
                //Xóa file hình trong thư mục /Data/Galleries
                string imagePath = Request.MapPath(imageName);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                return Json(new
                {
                    status = true
                });
            }
            else {
                return Json(new
                {
                    ststus = false
                });
            }

            //foreach (var node in nodes)
            //{
            //    //Xóa tên image trong trường ImageDetail của bảng [ImageGalleries]
            //    nodes.Remove();

            //    //Xóa file hình tương ứng trong thư mục /data/Galleries
            //    string imagePath = Request.MapPath(imageName);
            //    if (System.IO.File.Exists(imagePath))
            //    {
            //        System.IO.File.Delete(imagePath);
            //    }
            //}
            
            //try
            //{
               
            //}
            //catch (Exception e) {
            //    return Json(new
            //    {
            //        ststus = false
            //    });
            //}
        }

        //Phương thức này dùng cho lời gọi Ajax từ trạng thái user trong trang Index
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new GalleryDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }

        //Phương thức này dùng cho lời gọi Ajax từ  trang Index
        [HttpPost]
        public JsonResult GalleryChangeTopHot(long id)
        {
            var result = new GalleryDAO().ChangeTopHot(id);
            return Json(new
            {
                status = result
            });
        }


        //Load hình cho mỗi album không gian quán
        public JsonResult LoadImage(long id)
        {
            ImageGallery imageGallery = new GalleryDAO().imageGalleryDetail(id);
            var images = imageGallery.ImageDetail;
            XElement xElement = XElement.Parse(images);
            List<string> listImageReturn = new List<string>();
            foreach (XElement element in xElement.Elements())
            {
                listImageReturn.Add(element.Value);
            }
            return Json(new
            {
                data = listImageReturn
            }, JsonRequestBehavior.AllowGet);
        }

    }
}
