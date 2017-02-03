using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.ViewModels;


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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ProductViewModels> listProductView() {
            var model = from a in db.Products
                   join b in db.ProductCategories
                    on a.CategoryID equals b.ID
                   where a.CategoryID == b.ID
                   select new ProductViewModels()
                   {
                       ID= a.ID,
                       ProCateName = b.Name,
                       ProCateMetaTitle = b.MetaTitle,
                       Name=a.Name,
                       Description=a.Description,
                       Image= a.Image,
                       Price = a.Price,
                       Detail=a.Detail,
                       CreatedDate=a.CreatedDate,
                       CreatedBy=a.CreatedBy,
                       ModifiedDate = a.ModifiedDate,
                       ModifiedBy=a.ModifiedBy,
                       Status=a.Status,
                       TopHot= a.TopHot
                   };
            return model.ToList();
        }
        //Phương thức lấy ra danh sách thực đơn dùng cho client
        public List<Product> listProductClient()
        {
            return db.Products.Where(x => x.Status == true).ToList();
        }

        //Danh sách thực đơn bán chạy
        /// <summary>
        /// index : 0 => get all
        /// index : 1 => get by quanlity
        /// </summary>
        /// <param name="index"></param>
        /// <param name="quanlity"></param>
        /// <returns></returns>
        public List<Product> listProductBestSale(int index, int  quanlity)
        {
            //Nếu index là 0 thì lấy ra hết những sản phẩm nào thuộc dạng bán chạy
            //Nếu index là 1 thì chỉ lấy ra số lượng theo quanlity  để hiển thị lên trang chủ Client
            List<Product> list=null;
            if (index == 0)
            {
                list= db.Products.Where(x => x.Status == true && x.TopHot == true).ToList();
            }
            else if (index == 1)
            {
                list= db.Products.Where(x => x.Status == true && x.TopHot == true).OrderByDescending(x=>x.CreatedDate).Take(quanlity).ToList();
            }
            return list;
        }

        //Danh sách product theo ProductCategory id
        public List<Product> ProductByCategory(long id) {
            return db.Products.Where(x => x.CategoryID == id && x.Status == true).OrderBy(x => x.Name).ToList(); 
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

        //Add hình vào field MoreImgae
        public void updateImage(long id,string images)
        {
            Product product= db.Products.Find(id);
            product.MoreImage = images;
            db.SaveChanges();
        }
        //Phuong thus su dung cho test
        public bool create(Product entity) {
            db.Products.Add(entity);
            db.SaveChanges();
            return true;
        }

        //Lấy ra những sảng phẩm nào thuộc ProductCategory có ShowOnHome = true
        /// <summary>
        /// index : 0 => Get all
        /// index : 1 => Select by input quanlity
        /// </summary>
        /// <param name="index"></param>
        /// <param name="quanlity"></param>
        /// <returns></returns>
        public List<Product> productsOfProdCateforyShowOnHome(int index, int quanlity) {
            List<Product> lstproductsOfProdCateforyShowOnHome= new List<Product>();
            var ProdCateDAO = new ProductCategoryDAO();
            var ProdCateShowOnHomeID = ProdCateDAO.ProCategoryShowOnHome().ID;
            if (index == 0)
            {
                lstproductsOfProdCateforyShowOnHome = db.Products.Where(x => x.CategoryID == ProdCateShowOnHomeID && x.Status == true).ToList();
            }
            else if (index == 1) {
                lstproductsOfProdCateforyShowOnHome = db.Products.Where(x => x.CategoryID == ProdCateShowOnHomeID && x.Status == true).Take(quanlity).ToList();
            }
            return lstproductsOfProdCateforyShowOnHome;
        }

        //Lấy ra danh sách các món mới
        /// <summary>
        /// index : 0 => Get ALL ; index : 1 => Select by input quanlity
        /// </summary>
        /// <param name="index"></param>
        /// <param name="quanlity"></param>
        /// <returns></returns>
        public List<Product> lstNewProducts(int index, int quanlity) {
            List<Product> lstnewProd = new List<Product>();
            if (index == 0)
            {
                lstnewProd = db.Products.Where(x => x.Status == true).OrderByDescending(x => x.CreatedDate).ToList();
            }
            else if (index == 1) {
                lstnewProd = db.Products.Where(x => x.Status == true).OrderByDescending(x => x.CreatedDate).Take(quanlity).ToList();
            }
            return lstnewProd;
        }

        //Lấy ra viewcount của 1 product dựa vào ID
        public int currentViewcount(long id)
        {
            int result = 0;
            var entity = db.Products.Find(id);
            if (entity.ViewCount.HasValue)
            {
                result = (int)entity.ViewCount;
            }
            return result;
        }

        //Lấy ra danh sách sản phẩm được xem nhiều, số lượng theo quanlity truyền vào
        public List<Product> BestPreviewProduct(int quanlity)
        {
            return db.Products.Where(x => x.Status == true && x.ViewCount.HasValue).OrderByDescending(x => x.ViewCount).Take(quanlity).ToList();
        }
    }
}
