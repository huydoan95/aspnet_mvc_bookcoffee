using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class CategoryDAO
    {
        BookCoffeeShopDbContext db = null;
        //Khởi tạo CategoryDAO
        public CategoryDAO() {
            db = new BookCoffeeShopDbContext();
        }

        public IEnumerable<Category> ListAllPaging(string searchString, int page, int pagesize)
        {
            IQueryable<Category> model = db.Categories;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Name.Contains(searchString));
            }
            return model.OrderBy(x => x.ID).ToPagedList(page, pagesize);
        }

        //Phương thức lấy ra danh sách tất cả Category có status = true
        public List<Category> listAllCategory() {
            return db.Categories.ToList();
        }

        //Thêm mới cho CateGory
        public long CreateCategory(Category entity) {

            try
            {
                DateTime dateTimeNow = DateTime.Now;
               
                entity.CreatedDate = dateTimeNow;
                var category = db.Categories.Add(entity);
                db.SaveChanges();
            }
            catch (Exception ex) {

            }

            return entity.ID;
        }

        //Delete loại tin
        public bool DeleteCategory(int id)
        {
            try
            {
                var category = db.Categories.Find(id);
                db.Categories.Remove(category);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        //Phương thức kiểm tra tên loại tin có hay chưa
        public bool CategoryExits(string categoryName)
        {
            var result = db.Categories.SingleOrDefault(x => x.Name == categoryName);
            //Trường hợp loai tin chưa tồn tại
            if (result == null)
            {
                return false;
            }
            else
            {
                //Trường hợp loại tin đã tồn tại
                return true;
            }
        }



        //Lấy ra loại tin  theo ID
        public Category ViewCategory(int id)
        {
            return db.Categories.Find(id);
        }


        //Cập nhật thông tin loại tin
        public bool UpdateCategory(Category entity)
        {
            try
            {
                var category = db.Categories.Find(entity.ID);
                category.Name = entity.Name;
                category.Status = entity.Status;
                category.ShowOnHome = entity.ShowOnHome;
                category.ModifiedDate = DateTime.Now;
                category.MetaTitle = entity.MetaTitle;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
