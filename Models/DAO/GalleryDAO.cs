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
        public GalleryDAO() {
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

        //Lấy ra tất cả hình anh từ Gallery
        public List<ImageGallery> listGallery(int index, int quality) {
            //Neu index =0 thi lay tat ca
            List<ImageGallery> list = null;
            if (index == 0 ) {
                list = db.ImageGalleries.ToList();
            }
            //Nếu index =1 thì lấy theo quanlity
            if (index == 1) {
                list = db.ImageGalleries.OrderByDescending(x=>x.ID).Take(quality).ToList();
            }
            return list;
           
        }


    }
}
