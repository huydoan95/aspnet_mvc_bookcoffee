using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{

    public class ProductCategoryDAO
    {
        BookCoffeeShopDbContext db = null;
        public ProductCategoryDAO()
        {
            db = new BookCoffeeShopDbContext();
        }

        //Lấy ra danh sách loại sản phẩm
        public List<ProductCategory> listProdCategory()
        {
            return db.ProductCategories.ToList();
        }

        //Kiểm tra sự tồn tại của 1 loại sản phẩm trước khi tạo mới
        public bool ProductCategoryExist(string ProductCategoryName) {
            var result = db.ProductCategories.SingleOrDefault(x => x.Name == ProductCategoryName);
            if (result == null)
            {
                return false;
            }
            else {
                return true;
            }
        }

        //Tạo mới loại sản phẩm
        public bool CreateProductCategory(ProductCategory entity)
        {
            try
            {
                db.ProductCategories.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }


        //Lấy ra 1 loại sản phẩm theo ID
        public ProductCategory productCategoryDetail(int id) {
            return db.ProductCategories.Find(id);
        }

        //Cập nhật loại sản phẩm
        public bool UpdateProductCategory(ProductCategory entity) {
            try
            {
                ProductCategory productCategory = db.ProductCategories.Find(entity.ID);
                productCategory.Name = entity.Name;
                productCategory.Status = entity.Status;
                productCategory.ShowOnHome = entity.ShowOnHome;
                productCategory.ModifiedDate = DateTime.Now;
                productCategory.ModifiedBy = entity.ModifiedBy;
                productCategory.MetaTitle = entity.MetaTitle;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex) {
                return false;
            }
        }

        //Xóa loại sản phẩm
        //Delete loại tin
        public bool DeleteProductCategory(int id)
        {
            try
            {
                var productCategory = db.ProductCategories.Find(id);
                db.ProductCategories.Remove(productCategory);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
