using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    
    public class AuthorDAO
    {
        BookCoffeeShopDbContext db = null;
        public AuthorDAO() {
            db = new BookCoffeeShopDbContext();
        }

        //Danh sách Author dùng cho Admin
        public IEnumerable<Author> listAuthorsAdmin() {
            return db.Authors.OrderByDescending(x => x.status).ToList();
        }

        //Danh sách Author dung cho client
        public IEnumerable<Author> listAuthorsClient()
        {
            return db.Authors.Where(x => x.status == true).ToList();
        }

        //Tạo mới tác giả
        public bool CreateAuthor(Author entity) {
            try
            {
                db.Authors.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex) {
                return false;
            }
        }

        //Kiểm tra tồn tại của author cho Create
        public bool ExistAuthorForCreate(string authorName) {
            var result = db.Authors.SingleOrDefault(x => x.Name == authorName);
            if(result ==null) {
                return false;
            }
            else {
                return true;
            }
        }

        //Kiểm tra tồn tại của Author cho Update
        public bool ExistAuthorForUpdate(string AuthorName, long id)
        {
            var result = db.Authors.SingleOrDefault(x => x.Name == AuthorName && x.ID != id);
            if (result == null)
            {
                return false;
            }
            else
            {

                return true;
            }
        }

        //chi tiết 1 Author
        public Author AuthorDetails(long id) {
            return db.Authors.Find(id);
        }

        //Update Author
        public bool UpdateAuthor(Author entity) {
            try
            {
                var model = db.Authors.Find(entity.ID);
                model.Name = entity.Name;
                model.Phone = entity.Phone;
                model.Email = entity.Email;
                model.Address = entity.Address;
                model.Content = entity.Content;
                model.status = entity.status;
                model.MetaTitle = entity.MetaTitle;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex) {
                return false;
            }

        }

        //Phương thức này sử dụng cho ajax
        public bool ChangeStatus(long id)
        {
            var author = db.Authors.Find(id);
            author.status = !author.status;
            db.SaveChanges();
            return author.status;
        }
    }
}
