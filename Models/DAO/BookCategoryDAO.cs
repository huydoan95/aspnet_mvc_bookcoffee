using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{

    public class BookCategoryDAO
    {
        BookCoffeeShopDbContext db = null;
        public BookCategoryDAO()
        {
            db = new BookCoffeeShopDbContext();
        }

        //Lấy ra danh sách loại sách dùng cho Admin
        public List<BookCategory> listBookCategory()
        {
            return db.BookCategories.OrderByDescending(x=>x.Status).ToList();
        }

        //Lấy ra danh sách loại sách dùng cho Admin
        public List<BookCategory> listBookCategoryClient()
        {
            return db.BookCategories.Where(x => x.Status==true).ToList();
        }

        //Kiểm tra sự tồn tại của 1 loại sách trước khi tạo mới
        public bool BookCategoryExist(string BookCategoryName)
        {
            var result = db.BookCategories.SingleOrDefault(x => x.Name == BookCategoryName);
            if (result == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //Kiểm tra sự tồn tại của loại sách trước khi update
        public bool BookCategoryExistForUpdate(string BookCategoryName,long id)
        {
            var result = db.BookCategories.SingleOrDefault(x => x.Name == BookCategoryName && x.ID!=id);
            if (result == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        //Tạo mới loại sách
        public bool CreateBookCategory(BookCategory entity)
        {
            try
            {
                db.BookCategories.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }


        //Lấy ra 1 loại sách theo ID
        public BookCategory BookCategoryDetail(int id)
        {
            return db.BookCategories.Find(id);
        }

        //Cập nhật loại sách
        public bool UpdateBookCategory(BookCategory entity)
        {
            try
            {
                BookCategory bookCategory = db.BookCategories.Find(entity.ID);
                bookCategory.Name = entity.Name;
                bookCategory.Status = entity.Status;
                bookCategory.ShowOnHome = entity.ShowOnHome;
                bookCategory.ModifiedDate = DateTime.Now;
                bookCategory.ModifiedBy = entity.ModifiedBy;
                bookCategory.MetaTitle = entity.MetaTitle;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Xóa loại sách
        //Delete loại tin
        public bool DeletebookCategory(int id)
        {
            try
            {
                var bookCategory = db.BookCategories.Find(id);
                db.BookCategories.Remove(bookCategory);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Phương thức này sử dụng cho ajax
        public bool ChangeStatus(long id)
        {
            var bookCategory = db.BookCategories.Find(id);
            bookCategory.Status = !bookCategory.Status;
            db.SaveChanges();
            return bookCategory.Status;
        }
    }
}
