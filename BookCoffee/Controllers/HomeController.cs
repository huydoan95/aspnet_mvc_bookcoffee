using Models.DAO;
using Models.EF;
using PSHostsFile;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Xml.Linq;

namespace BookCoffee.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            string FIRSRLOAD_SESSION = (string)Session["FIRSTLOAD_SESSION"];
            if (FIRSRLOAD_SESSION==null) {
                //Lấy ra giá trị từ appsetting
                string IP1 = WebConfigurationManager.AppSettings["IP1"];
                //Đọc file Hosts và tìm xem host đã tồn tại chưa
                bool ip1 = false;
                string line;
                string IP1Trim = IP1.Replace(" ", "");
                IP1Trim = IP1Trim.Trim();
                // Read the file and display it line by line.
                System.IO.StreamReader file = new System.IO.StreamReader("C:\\WINDOWS\\system32\\drivers\\etc\\hosts");
                while ((line = file.ReadLine()) != null)
                {
                    if (line.Length > 0)
                    {
                        string commentchar = line.Substring(0, 1);
                        if (!commentchar.Equals("#"))
                        {
                            line = line.Replace(" ", "");
                            line = line.Trim();
                            if (line.Equals(IP1Trim))
                            {
                                ip1 = true;
                                break;
                            }
                        }
                    }
                }
                file.Close();

                //Register IP and Hostness();
                if (ip1 == false)
                {
                    string app = Server.MapPath("~") + "/Consoles/addHostname.exe";
                    //string app = Server.MapPath("~") + "/Consoles/test.txt";
                    ProcessStartInfo proinfo = new ProcessStartInfo();
                    proinfo.FileName = app;
                    proinfo.CreateNoWindow = false;
                    proinfo.Arguments = IP1;
                    Process.Start(proinfo);
                    return RedirectToAction("index","route");

                }
            }
               
            
            //Section tin tức
            ViewBag.listContent = new ContentDAO().listContent();
            ViewBag.NewestContent = new ContentDAO().listNewestContent();
            ViewBag.TopHotContent = new ContentDAO().TopHotConten();
            //End Section tin tức

            //Section gallery
            ViewBag.listGallery = new GalleryDAO().getTopHotGallery();
            //End section gallery

            //Section Events
            //Lấy ra 3 sự kiện mới nhất đưa lên trang chủ
            ViewBag.NewestEvents = new EventsDAO().newestEvents(3);

            //Lấy ra 3 sự kiện TopHOt
            ViewBag.EventTopHot = new EventsDAO().TopHotEvents(1, 3);
            //End section events

            //SECTION BOOKS
            //Danh sách sách
            ViewBag.listBooks = new BooksDAO().ListBooksClient();
            //END SECTION BOOKS

            //SECTION PRODUCTS
            //Danh sách thực đơn bán chạy
            ViewBag.TopHotProducts = new ProductDAO().listProductBestSale(1, 6);
            //END SECTION PRODUCTS


            //SECTION LAWCORNER
            ///Lấy ra danh sách tin LawCorner thuộc TopHot
            ViewBag.TopHotLawCorner = new LawCornerDAO().TopHotForClient();

            //Lấy ra 3 sự tin luật mới nhất đưa lên Index
            ViewBag.NewestLawCorner = new LawCornerDAO().listNewestLawCorner(3);
            //END SECTION LAWCORNER
            Session["FIRSTLOAD_SESSION"] = null;
            return View();
        }


        //Lấy ra Menu bên trái Logo
        [ChildActionOnly]
        public ActionResult MenuLeftTop()
        {
            var dao = new MenuDAO();
            var model = dao.listMenuByMenuType(1);
            return PartialView(model);
        }


        //Lấy ra Menu bên phải Logo
        [ChildActionOnly]
        public ActionResult MenuRightTop()
        {
            var dao = new MenuDAO();
            var model = dao.listMenuByMenuType(2);
            return PartialView(model);
        }


    }
}