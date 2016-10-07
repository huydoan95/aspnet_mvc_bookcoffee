using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class BooksDAO
    {
        BookCoffeeShopDbContext db = null;
        public BooksDAO()
        {
            db = new BookCoffeeShopDbContext();
        }

        //Lấy ra danh sách tất cả sách dùng cho Admin
        public List<Book> listBooks()
        {
            return db.Books.OrderByDescending(x => x.Status).ToList();
        }

        //Lấy ra danh sách tất cả các sách dùng cho client
        public List<Book> ListBooksClient() {
            return db.Books.Where(x => x.Status == true).OrderBy(x => x.Name).ToList();
        }

        //Kiem tra su ton tai cua 1 sách dùng cho Create
        public bool bookExist(string bookName)
        {
            var result = db.Books.SingleOrDefault(x => x.Name == bookName);
            if (result == null)
            {
                return false;
            }
            else
            {

                return true;
            }
        }


        //Kiểm tra sự tồn tại của 1 sách dùng cho Update
        public bool bookExistForUpdate(string bookName, long id) {
            var result = db.Books.SingleOrDefault(x => x.Name == bookName && x.ID!=id);
            if (result == null)
            {
                return false;
            }
            else
            {

                return true;
            }
        }

        //Lấy ra 1 cuốn sách dựa theo ID
        public Book bookDetail(int id)
        {
            return db.Books.Find(id);
        }

        //Phương thức tạo mới 1 sản phẩm
        public bool CreateBook(Book entity)
        {
            try
            {
                entity.CreatedDate = DateTime.Now;
                db.Books.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Xóa sản phẩm
        public bool DeleteBook(int id)
        {
            try
            {
                var entity = db.Books.Find(id);
                db.Books.Remove(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Update sản phẩm phẩm
        public bool UpdateBook(Book entity)
        {
            try
            {
                var model = db.Books.Find(entity.ID);
                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = entity.ModifiedBy;
                model.Name = entity.Name;
                model.CategoryID = entity.CategoryID;
                model.Image = entity.Image;
                model.Author = entity.Author;
                model.publisher = entity.publisher;
                model.Description = entity.Description;
                model.Detail = entity.Detail;
                model.Status = entity.Status;
                model.TopHot = entity.TopHot;
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
            var book = db.Books.Find(id);
            book.Status = !book.Status;
            db.SaveChanges();
            return book.Status;
        }

        //Tìm xem có tin thuộc TopHot chưa cho Create
        public bool TopHotForCreate()
        {
            var result = db.Books.SingleOrDefault(x => x.TopHot == true);
            if (result == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //Tìm xem có tin thuộc TopHot cho Update
        public bool TopHotForUpdate(long id)
        {
            var result = db.Books.SingleOrDefault(x => x.TopHot == true && x.ID != id);
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
        public Book TopHotForClient()
        {
            return db.Books.SingleOrDefault(x => x.TopHot == true);
        }
    }
}
