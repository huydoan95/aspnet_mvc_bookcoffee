using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class ContentDAO
    {
        BookCoffeeShopDbContext db = null;
        public ContentDAO()
        {
            db = new BookCoffeeShopDbContext();
        }

        //Lấy ra danh sách tin tức dùng cho Admin
        public List<Content> listContentAdmin()
        {
            return db.Contents.OrderByDescending(x => x.Status).ToList();
        }

        //Lấy ra danh sách  tin tức dung cho client
        public List<Content> listContent() {
            return db.Contents.Where(x=>x.Status==true).OrderByDescending(x => x.CreatedDate).ToList();
        }

        //Lấy ra danh sách 6 tin mới nhất
        public List<Content> listNewestContent()
        {
            return db.Contents.Where(x => x.Status == true).OrderByDescending(x => x.CreatedDate).Take(6).ToList();
        }

        //Thêm mới tin tức
        public long CreateContent(Content entity)
        {

            try
            {
                DateTime CreatedDate  = DateTime.Now;
                entity.CreatedDate = CreatedDate;
                var content = db.Contents.Add(entity);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
            return entity.ID;
        }

        //Xóa bản tin
        public bool DeleteContent(int id) {
            try
            {
                var entity = db.Contents.Find(id);
                db.Contents.Remove(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex) {
                return false;
            }

        }

        //Lấy ra chi tiết 1 tin tức theo ID

        public Content ContentDetail(int id) {
            return db.Contents.Find(id);
        }
        //Cập nhật thông tin tin tức
        public bool UpdateContent(Content entity)
        {
            try
            {
                var content = db.Contents.Find(entity.ID);
                content.CategoryID = entity.CategoryID;
                content.Name = entity.Name;
                content.Image = entity.Image;
                content.Description = entity.Description;
                content.Detail = entity.Detail;
                content.TopHot = entity.TopHot;
                content.Status = entity.Status;
                content.Tags = entity.Tags;
                content.ModifiedBy = entity.ModifiedBy;
                content.ModifiedDate = entity.ModifiedDate;
                content.MetaTitle = entity.MetaTitle;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Tìm xem có tin thuộc TopHot chưa cho Create
        public bool TopHotForCreate() {
            var result = db.Contents.SingleOrDefault(x => x.TopHot == true);
            if (result == null)
            {
                return false;
            }
            else {
                return true;
            }
        }

        //Tìm xem có tin thuộc TopHot cho Update
        public bool TopHotForUpdate(long id) {
            var result = db.Contents.SingleOrDefault(x => x.TopHot == true && x.ID != id);
            if (result == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //Lấy ra tin TopHot hiển thị ra Client
        public Content TopHotConten() {
            return db.Contents.SingleOrDefault(x => x.TopHot == true);
        }
    }
}
