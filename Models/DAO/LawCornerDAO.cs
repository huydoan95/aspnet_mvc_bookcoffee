using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class LawCornerDAO
    {
        BookCoffeeShopDbContext db = null;
        public LawCornerDAO()
        {
            db = new BookCoffeeShopDbContext();
        }

        //Lấy ra danh sách tất cả  dùng cho Admin
        public List<LawCorner> listLawCorner()
        {
            return db.LawCorners.OrderByDescending(x => x.Status).ToList();
        }

        //Lấy ra danh sách tất cả  dùng cho client
        public List<LawCorner> ListLawCornerClient()
        {
            return db.LawCorners.Where(x => x.Status == true).OrderBy(x => x.Status).ToList();
        }

        //Lấy ra danh sách tin mới nhất
        public List<LawCorner> listNewestLawCorner(int quanlity) {
            //Lấy ra số tin mới nhất theo quanlity
            return db.LawCorners.Where(x=>x.Status==true && x.TopHot!=true).OrderByDescending(x => x.CreatedDate).Take(quanlity).ToList();
        }

        //Kiem tra su ton tai cho create
        public bool LawCornerExist(string LawCornerTitle)
        {
            var result = db.LawCorners.SingleOrDefault(x => x.Name == LawCornerTitle);
            if (result == null)
            {
                return false;
            }
            else
            {

                return true;
            }
        }


        //Kiểm tra sự tồn tại dùng cho Update
        public bool LawCornerExistForUpdate(string LawCornerTitle, long id)
        {
            var result = db.LawCorners.SingleOrDefault(x => x.Name == LawCornerTitle && x.ID != id);
            if (result == null)
            {
                return false;
            }
            else
            {

                return true;
            }
        }

        //Lấy ra 1 tin luật dựa theo ID
        public LawCorner LawCornerDetail(long  id)
        {
            return db.LawCorners.Find(id);
        }

        //Phương thức tạo mới 1 tin luật
        public bool CreateLawCorner(LawCorner entity)
        {
            try
            {
                entity.CreatedDate = DateTime.Now;
                db.LawCorners.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Xóa sản phẩm
        public bool DeleteLawCorner(long id)
        {
            try
            {
                var entity = db.LawCorners.Find(id);
                db.LawCorners.Remove(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Update sản phẩm phẩm
        public bool UpdateLawCorner(LawCorner entity)
        {
            try
            {
                var model = db.LawCorners.Find(entity.ID);
                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = entity.ModifiedBy;
                model.Name = entity.Name;
                model.Image = entity.Image;
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
            var lawCorner = db.LawCorners.Find(id);
            lawCorner.Status = !lawCorner.Status;
            db.SaveChanges();
            return lawCorner.Status;
        }

        //Phương thức này sử dụng cho ajax
        public int ChangeTopHot(long id)
        {
            //Giá trị trả về
            // 0 là đang có
            // 1 là TopHot = true
            // 2 là TopHot = fasle
            int final = 100;
            LawCorner selectedObj = db.LawCorners.Find(id);
            //Nếu TopHot của LawCorner đang được chọn là false thì cần kiểm tra xem hiện đang có LawCorner nào thuộc TopHot không?
            if (selectedObj.TopHot == false)
            {
                var result = db.LawCorners.Where(x => x.TopHot == true && x.ID != id).Count();
                if (result == 0)
                {
                    //Nếu hiện tại chưa có thì chuyển TopHot của nó thành true
                    selectedObj.TopHot = true;
                    db.SaveChanges();
                    final = 1;
                }
                else
                {
                    final = 0;
                }
            }
            else {
                selectedObj.TopHot = false;
                db.SaveChanges();
                final = 2;
            }
           
            return final;
        }

        //Tìm xem có tin thuộc TopHot chưa cho Create
        public bool TopHotForCreate()
        {
            var result = db.LawCorners.SingleOrDefault(x => x.TopHot == true);
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
            var result = db.LawCorners.SingleOrDefault(x => x.TopHot == true && x.ID != id);
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
        public LawCorner TopHotForClient()
        {
            return db.LawCorners.SingleOrDefault(x => x.TopHot == true);
        }
    }
}
