using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Models.DAO
{
    public class ProductDAO
    {
        BookCoffeeShopDbContext db = null;
        public ProductDAO()
        {
            db = new BookCoffeeShopDbContext();
        }

        //Phương thức lấy ra danh sách tất cả các sản phẩm dùng cho Admin
        public List<Product> listProduct()
        {
            return db.Products.OrderByDescending(x => x.Status).ToList();
        }
        //Phương thức lấy ra danh sách thực đơn dùng cho client
        public List<Product> listProductClient()
        {
            return db.Products.Where(x => x.Status == true).ToList();
        }

        //Danh sách thực đơn bán chạy
        public List<Product> listProductBestSale(int index)
        {
            //Nếu index là 0 thì lấy ra hết những sản phẩm nào thuộc dạng bán chạy
            //Nếu index là 1 thì chỉ lấy ra 6 loại để hiển thị lên trang chủ Client
            List<Product> list=null;
            if (index == 0)
            {
                list= db.Products.Where(x => x.Status == true && x.TopHot == true).ToList();
            }
            else if (index == 1)
            {
                list= db.Products.Where(x => x.Status == true && x.TopHot == true).Take(6).ToList();
            }
            return list;
        }

        //Kiem tra su ton tai cua 1 loai san pham
        public bool ProductExist(string productName)
        {
            var result = db.Products.SingleOrDefault(x => x.Name == productName);
            if (result == null)
            {
                return false;
            }
            else
            {

                return true;
            }
        }

        //Lấy ra 1 sản phẩm dựa theo ID
        public Product productDetail(int id)
        {
            return db.Products.Find(id);
        }

        //Phương thức tạo mới 1 sản phẩm
        public bool CreateProduct(Product entity)
        {
            try
            {
                entity.CreatedDate = DateTime.Now;
                db.Products.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Xóa sản phẩm
        public bool DeleteProduct(int id)
        {
            try
            {
                var entity = db.Products.Find(id);
                db.Products.Remove(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Update sản phẩm phẩm
        public bool UpdateProduct(Product entity)
        {
            try
            {
                var model = db.Products.Find(entity.ID);
                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = entity.ModifiedBy;
                model.Name = entity.Name;
                model.CategoryID = entity.CategoryID;
                model.Image = entity.Image;
                model.Price = entity.Price;
                model.PromotionPrice = entity.PromotionPrice;
                model.IncludedVAT = entity.IncludedVAT;
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
            var product = db.Products.Find(id);
            product.Status = !product.Status;
            db.SaveChanges();
            return product.Status;
        }

        //Phương thức này thay đổi tình trạng Tophot (bán chạy/ bình thường)
        public bool ChageTopHot(long id)
        {
            var product = db.Products.Find(id);
            product.TopHot = !product.TopHot;
            db.SaveChanges();
            return product.TopHot;
        }


        //Phuong thus su dung cho test
        public bool create(Product entity) {
            db.Products.Add(entity);
            db.SaveChanges();
            return true;
        }
    }
}
