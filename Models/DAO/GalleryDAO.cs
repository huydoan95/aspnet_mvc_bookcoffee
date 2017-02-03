using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Models.DAO
{
    public class GalleryDAO
    {
        BookCoffeeShopDbContext db = null;
        public GalleryDAO()
        {
            db = new BookCoffeeShopDbContext();
        }
        //Tạo phương thức phân trang 
        public IEnumerable<ImageGallery> ListAllPaging(string searchString, int page, int pagesize)
        {
            IQueryable<ImageGallery> model = db.ImageGalleries;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Status.ToString().Contains(searchString));
            }
            return model.OrderByDescending(x => x.ID).ToPagedList(page, pagesize);
        }

        //Tạo mới album không gian quán
        public bool CreateGallery(ImageGallery entity)
        {
            try
            {
                entity.CreatedDate = DateTime.Now;
                db.ImageGalleries.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Update album không gián quán
        public bool UpdateGallery(ImageGallery entity) {
            ImageGallery gallery = new ImageGallery();
            try
            {
                gallery.Name = entity.Name;
                gallery.Description = entity.Description;
                gallery.Detail = entity.Detail;
                gallery.ImageDetail = entity.ImageDetail;
                gallery.ImagePath = entity.ImagePath;
                gallery.ModifiedDate = DateTime.Now;
                gallery.Status = entity.Status;
                gallery.TopHot = entity.TopHot;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex) {
                return false;
            }
        }

        //Kiem tra su ton tai cua tên album không gian dùng cho Create
        public bool GalleryExist(string GalleryName)
        {
            var result = db.ImageGalleries.SingleOrDefault(x => x.Name == GalleryName);
            if (result == null)
            {
                return false;
            }
            else
            {

                return true;
            }
        }


        //Lấy ra tất cả hình anh từ Gallery
        /// <summary>
        /// Neu index =0 thi lay tat ca 
        /// Nếu index =1 thì lấy theo quanlity (thông thường là 9 hình để show lên trang chủ)
        /// Nếu index=2  dùng để hiển thị trong trang dành riêng cho gallery của Client
        /// </summary>
        /// <param name="index"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        public List<ImageGallery> listGallery(int index, int quality)
        {

            List<ImageGallery> list = null;
            if (index == 0)
            {
                list = db.ImageGalleries.OrderByDescending(x => x.Status).ToList();
            }

            if (index == 1)
            {
                list = db.ImageGalleries.Where(x => x.Status == true && x.TopHot == true).OrderByDescending(x => x.ID).Take(quality).ToList();
            }
            else if (index == 2)
            {

                list = db.ImageGalleries.Where(x => x.Status == true).OrderByDescending(x => x.ID).ToList();
            }
            return list;

        }

        //Xóa hình ảnh
        public bool DeleteGallery(long id)
        {
            try
            {
                var entity = db.ImageGalleries.Find(id);
                db.ImageGalleries.Remove(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Lấy ra 1 dòng trong ImageGalleries theo ID
        public ImageGallery imageGalleryDetail(long id)
        {
            return db.ImageGalleries.Find(id);
        }

        //Lấy ra Album thuộcTophot
        public ImageGallery getTopHotGallery() {
            return db.ImageGalleries.SingleOrDefault(x => x.TopHot == true);
        }


        //Phương thức này sử dụng cho ajax
        public bool ChangeStatus(long id)
        {
            var imageGallery = db.ImageGalleries.Find(id);
            imageGallery.Status = !imageGallery.Status;
            db.SaveChanges();
            return imageGallery.Status;
        }

        //Gallery được phép tối đa 9 hình top hot
        public int ChangeTopHot(long id)
        {
            //trả về 0 thì thông báo có ít nhất 9 hình thuộc Tophot
            //Trả về 1 thì đã chuyển từ false thành true
            //Trả về 2 thì đã chuyển từ true thành false;
            ImageGallery imageGallery = this.imageGalleryDetail(id);
            int result = 100;
            if (imageGallery.TopHot == false)
            {
                int countTopHot = db.ImageGalleries.Where(x => x.TopHot == true).ToList().Count();
                if (countTopHot >= 1)
                {
                    result = 0;
                }
                else
                {
                    imageGallery.TopHot = !imageGallery.TopHot;
                    db.SaveChanges();
                    result = 1;
                }
            }
            else
            {
                imageGallery.TopHot = !imageGallery.TopHot;
                db.SaveChanges();
                result = 2;
            }
            return result;
        }


        //Gallery được phép tối đa 1 album top hot
        public bool TopHotForCreate()
        {
            var result = db.ImageGalleries.SingleOrDefault(x => x.TopHot == true);
            if (result == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //Gallery được phép tối đa 1 album top hot
        public bool TopHotForUpdate(long id)
        {
            var result = db.ImageGalleries.SingleOrDefault(x => x.TopHot == true && x.ID!=id);
            if (result == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //Add hình vào field ImageDetail
        public bool updateImage(long id, string images)
        {
            try
            {
                ImageGallery imageGallery = db.ImageGalleries.Find(id);
                imageGallery.ImageDetail =  images;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex) {
                return false;
            }
        }
    }
}
