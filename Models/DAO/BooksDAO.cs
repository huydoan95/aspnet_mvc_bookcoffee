using Models.EF;
using Models.ViewModels;
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
        //public List<Book> listBooks()
        //{
        //    return db.Books.OrderByDescending(x => x.Status).ToList();
        //}

        /// <summary>
        /// Danh sách sách dùng cho trang quản trị
        /// </summary>
        /// <returns></returns>
        public List<BooksViewModel> listBooks()
        {
            var model = from a in db.Books
                        join b in db.BookCategories
                        on a.CategoryID equals b.ID
                        where a.CategoryID == b.ID
                        select new BooksViewModel()
                        {
                            ID = a.ID,
                            Name = a.Name,
                            Author = a.Author,
                            publisher = a.publisher,
                            MetaTitle = a.MetaTitle,
                            Description = a.Description,
                            Image = a.Image,
                            Price = a.Price,
                            PromotionPrice = a.PromotionPrice,
                            IncludedVAT = a.IncludedVAT,
                            Quantity = a.Quantity,
                            CategoryName = b.Name,
                            Detail = a.Detail,
                            CreatedDate = a.CreatedDate,
                            CreatedBy = a.CreatedBy,
                            ModifiedDate = a.ModifiedDate,
                            ModifiedBy = a.ModifiedBy,
                            MetaKeywords = a.MetaKeywords,
                            MetaDescriptions = a.MetaDescriptions,
                            Status = a.Status,
                            TopHot = a.TopHot,
                            ViewCount = a.ViewCount
                        };
            return model.ToList();
        }

        //Lấy ra danh sách tất cả các sách dùng cho client
        public List<BooksViewModel> ListBooksClient()
        {
            var model = from a in db.Books
                        join b in db.BookCategories
                        on a.CategoryID equals b.ID
                        where a.CategoryID == b.ID && a.Status==true
                        select new BooksViewModel()
                        {
                            ID = a.ID,
                            Name = a.Name,
                            Author = a.Author,
                            publisher = a.publisher,
                            MetaTitle = a.MetaTitle,
                            Description = a.Description,
                            Image = a.Image,
                            Price = a.Price,
                            PromotionPrice = a.PromotionPrice,
                            IncludedVAT = a.IncludedVAT,
                            Quantity = a.Quantity,
                            CategoryName = b.Name,
                            Detail = a.Detail,
                            CreatedDate = a.CreatedDate,
                            CreatedBy = a.CreatedBy,
                            ModifiedDate = a.ModifiedDate,
                            ModifiedBy = a.ModifiedBy,
                            MetaKeywords = a.MetaKeywords,
                            MetaDescriptions = a.MetaDescriptions,
                            Status = a.Status,
                            TopHot = a.TopHot,
                            ViewCount = a.ViewCount
                        };
            return model.ToList();
        }

        //Lấy danh sách những cuốn sách thuộc tophot
        public List<BooksViewModel> TopHotForClient()
        {
            var model = from a in db.Books
                        join b in db.BookCategories
                        on a.CategoryID equals b.ID
                        where a.CategoryID == b.ID && a.TopHot == true && a.Status == true
                        select new BooksViewModel()
                        {
                            ID = a.ID,
                            Name = a.Name,
                            Author = a.Author,
                            publisher = a.publisher,
                            MetaTitle = a.MetaTitle,
                            Description = a.Description,
                            Image = a.Image,
                            Price = a.Price,
                            PromotionPrice = a.PromotionPrice,
                            IncludedVAT = a.IncludedVAT,
                            Quantity = a.Quantity,
                            CategoryName = b.Name,
                            Detail = a.Detail,
                            CreatedDate = a.CreatedDate,
                            CreatedBy = a.CreatedBy,
                            ModifiedDate = a.ModifiedDate,
                            ModifiedBy = a.ModifiedBy,
                            MetaKeywords = a.MetaKeywords,
                            MetaDescriptions = a.MetaDescriptions,
                            Status = a.Status,
                            TopHot = a.TopHot,
                            ViewCount = a.ViewCount
                        };
            return model.ToList();
        }

        //Lấy danh sách sách mới, số lượng theo quality
        public List<BooksViewModel> ListNewBooks(int quality) {
            var model = from a in db.Books
                        join b in db.BookCategories
                        on a.CategoryID equals b.ID
                        where a.CategoryID == b.ID &&  a.Status == true
                        orderby a.CreatedDate descending
                        select new BooksViewModel()
                        {
                            ID = a.ID,
                            Name = a.Name,
                            Author = a.Author,
                            publisher = a.publisher,
                            MetaTitle = a.MetaTitle,
                            Description = a.Description,
                            Image = a.Image,
                            Price = a.Price,
                            PromotionPrice = a.PromotionPrice,
                            IncludedVAT = a.IncludedVAT,
                            Quantity = a.Quantity,
                            CategoryName = b.Name,
                            Detail = a.Detail,
                            CreatedDate = a.CreatedDate,
                            CreatedBy = a.CreatedBy,
                            ModifiedDate = a.ModifiedDate,
                            ModifiedBy = a.ModifiedBy,
                            MetaKeywords = a.MetaKeywords,
                            MetaDescriptions = a.MetaDescriptions,
                            Status = a.Status,
                            TopHot = a.TopHot,
                            ViewCount = a.ViewCount
                        };
            return model.Take(quality).ToList();
        }

        //Lấy ra viewcount của 1 book dựa vào ID
        public int currentViewcount(long id)
        {
            int result = 0;
            var entity = db.Books.Find(id);
            if (entity.ViewCount.HasValue)
            {
                result = (int)entity.ViewCount;
            }
            return result;
        }

        //Lấy ra danh sách sách được xem nhiều, số lượng theo quanlity truyền vào
        public List<BooksViewModel> BestPreviewBooks(int quanlity) {
            var model = from a in db.Books
                        join b in db.BookCategories
                        on a.CategoryID equals b.ID
                        where a.CategoryID == b.ID && a.Status == true && a.ViewCount.HasValue
                        orderby a.ViewCount descending
                        select new BooksViewModel()
                        {
                            ID = a.ID,
                            Name = a.Name,
                            Author = a.Author,
                            publisher = a.publisher,
                            MetaTitle = a.MetaTitle,
                            Description = a.Description,
                            Image = a.Image,
                            Price = a.Price,
                            PromotionPrice = a.PromotionPrice,
                            IncludedVAT = a.IncludedVAT,
                            Quantity = a.Quantity,
                            CategoryName = b.Name,
                            Detail = a.Detail,
                            CreatedDate = a.CreatedDate,
                            CreatedBy = a.CreatedBy,
                            ModifiedDate = a.ModifiedDate,
                            ModifiedBy = a.ModifiedBy,
                            MetaKeywords = a.MetaKeywords,
                            MetaDescriptions = a.MetaDescriptions,
                            Status = a.Status,
                            TopHot = a.TopHot,
                            ViewCount = a.ViewCount
                        };
            return model.Take(quanlity).ToList();
        }

        //Lấy ra những sách nào thuộc BookCategory đang có ShowOnHome là true để đưa lên trang Index của BooksController
        public List<BooksViewModel> ListBooksShowOnHome() {
            var model = from a in db.Books
                        join b in db.BookCategories
                        on a.CategoryID equals b.ID
                        where a.CategoryID == b.ID && a.Status == true && b.ShowOnHome==true
                        orderby a.ViewCount descending
                        select new BooksViewModel()
                        {
                            ID = a.ID,
                            Name = a.Name,
                            Author = a.Author,
                            publisher = a.publisher,
                            MetaTitle = a.MetaTitle,
                            Description = a.Description,
                            Image = a.Image,
                            Price = a.Price,
                            PromotionPrice = a.PromotionPrice,
                            IncludedVAT = a.IncludedVAT,
                            Quantity = a.Quantity,
                            CategoryName = b.Name,
                            Detail = a.Detail,
                            CreatedDate = a.CreatedDate,
                            CreatedBy = a.CreatedBy,
                            ModifiedDate = a.ModifiedDate,
                            ModifiedBy = a.ModifiedBy,
                            MetaKeywords = a.MetaKeywords,
                            MetaDescriptions = a.MetaDescriptions,
                            Status = a.Status,
                            TopHot = a.TopHot,
                            ViewCount = a.ViewCount
                        };
            return model.ToList();
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
        public bool bookExistForUpdate(string bookName, long id)
        {
            var result = db.Books.SingleOrDefault(x => x.Name == bookName && x.ID != id);
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

        //Update viewcount
        public bool UpdateBookViewcount(Book entity)
        {
            try
            {
                var model = db.Books.Find(entity.ID);
                model.ViewCount = entity.ViewCount;
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
        //public Book TopHotForClient()
        //{
        //    return db.Books.SingleOrDefault(x => x.TopHot == true);
        //}

        

        

    }
}
