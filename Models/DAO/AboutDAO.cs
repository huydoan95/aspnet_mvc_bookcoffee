using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class AboutDAO
    {
        BookCoffeeShopDbContext db = null;
        public AboutDAO() {
            db = new BookCoffeeShopDbContext();
        }

        //Tạo list About cho Admin
        public List<About> lstAboutAdmin() {
            return db.Abouts.OrderByDescending(x => x.CreatedDate).ToList();
        }

        //Kiem tra su ton tai cua 1 bài giới dùng cho Create
        public bool AboutExistForCreate(string aboutName)
        {
            var result = db.Abouts.SingleOrDefault(x => x.Name == aboutName);
            if (result == null)
            {
                return false;
            }
            else
            {

                return true;
            }
        }

        //Kiểm tra sự tồn tại của 1 bài giới thiệu dùng cho Update
        public bool AboutExistForUpdate(string aboutName, long id)
        {
            var result = db.Abouts.SingleOrDefault(x => x.Name == aboutName && x.ID != id);
            if (result == null)
            {
                return false;
            }
            else
            {

                return true;
            }
        }

        //Lấy ra 1 bài giới dựa theo ID
        public About aboutDetail(long id)
        {
            return db.Abouts.Find(id);
        }

        //Phương thức tạo mới 1 bài giới thiệu
        public bool CreateABout(About entity)
        {
            try
            {
                entity.CreatedDate = DateTime.Now;
                db.Abouts.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Xóa bài giới thiệu
        public bool DeleteAbout(long id)
        {
            try
            {
                var entity = db.Abouts.Find(id);
                db.Abouts.Remove(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Update sản bài giời thiệu
        public bool UpdateAbout(About entity)
        {
            try
            {
                var model = db.Abouts.Find(entity.ID);
                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = entity.ModifiedBy;
                model.Name = entity.Name;
                model.Image = entity.Image;
                model.Description = entity.Description;
                model.Detail = entity.Detail;
                model.Status = entity.Status;
                model.MetaTitle = entity.MetaTitle;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        //Phương thức này sử dụng cho ajax
        public bool ChangeStatus(long id)
        {
            var about = db.Abouts.Find(id);
            about.Status = !about.Status;
            db.SaveChanges();
            return about.Status;
        }

        
    }
}
