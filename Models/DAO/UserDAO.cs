using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.EF;
using System.Data.Entity.Validation;
using PagedList;

namespace Models.DAO
{
    public class UserDAO
    {
        BookCoffeeShopDbContext db = null;
        public UserDAO()
        {
            db = new BookCoffeeShopDbContext();
        }

        //Tạo phương thức phân trang 
        public IEnumerable<User> ListAllPaging(string searchString,int page, int pagesize) {
            IQueryable<User> model = db.Users;
            if (!string.IsNullOrEmpty(searchString)) {
                model = model.Where(x=>x.UserName.Contains(searchString) || x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.ID).ToPagedList(page,pagesize);
        }

        public List<User> listUsers() {

            return db.Users.ToList();
        }

        //Tạo mới 1 user
        public long CreateUser(User entity)
        {
            
            try {
                var user = db.Users.Add(entity);
                db.SaveChanges(); 
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
            return entity.ID;
        }

        //Phương thức lấy ra user dăng nhập
        public User GetUserID(string userName)
        {
            return db.Users.SingleOrDefault(x => x.UserName == userName);

        }

        //Phương thức kiểm tra Username có hay chưa
        public bool UserExists(string UserName) {
            var result = db.Users.SingleOrDefault(x => x.UserName == UserName);
            //Trường hợp chưa có username tồn tại
            if (result == null)
            {
                return false;
            }
            else {
                //Trường hợp đã có username này trong CSDL
                return true;
            }
        }

        //Kiểm tra đăng nhập
        public int login(string username, string password)
        {
            //0 = username không tồn tại
            //-1 username bị khóa
            //1 password đúng =>Login thành công
            //-2 password sai
            var result = db.Users.SingleOrDefault(x => x.UserName == username);
            if (result ==null)
            {
                return 0;
            }
            else
            {
                if (result.Status == false)
                {
                    return -1;
                }
                else {
                    if (result.Password == password)
                    {
                        return 1;
                    }
                    else {
                        return -2;
                    }
                }
            }
        }

        //Lấy ra user theo ID
        public User ViewUserDetail(int id) {
            return db.Users.Find(id);
        }


        //Cập nhật thông tin người dùng
        public bool UpdateUser(User entity) {
            try
            {
                var user = db.Users.Find(entity.ID);
                user.Password = entity.Password;
                user.Name = entity.Name;
                user.Address = entity.Address;
                user.Email = entity.Email;
                user.Phone = entity.Phone;
                user.ModifiedBy = entity.ModifiedBy;
                user.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Delete người dùng
        public bool DeleteUser(int id) {
            try
            {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception) {
                return false;
            }
        }

        //Phương thức này sử dụng cho ajax
        public bool ChangeStatus(long id) {
            var user = db.Users.Find(id);
            user.Status = !user.Status;
            db.SaveChanges();
            return user.Status;
        }
    }
}
