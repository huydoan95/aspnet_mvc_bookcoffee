using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class MenuTypeDAO
    {
        BookCoffeeShopDbContext db = null;
        public MenuTypeDAO() {
            db = new BookCoffeeShopDbContext();
        }
        //Phương thức lấy ra danh sách toàn bộ loại Menu hiện  đang có
        public List<MenuType> listMenuType() {
            return db.MenuTypes.OrderByDescending(x=>x.status).ToList();
        }

        //Lấy ta 1 MenuType theo id
        public MenuType menuTypeDetail(int id) {
            return db.MenuTypes.Find(id);
        }

        //Phương thức kiểm tra tên MenuType
        public bool MenuTypeExist(string MenuTypeName) {
            var result = db.MenuTypes.SingleOrDefault(x => x.Name == MenuTypeName);
            if (result == null)
            {
                return false;
            }
            else {
                return true;
            }
        }

        //Phương thức kiểm tra sự tồn tại của MenuType trước khi update
        public bool MenuTypeExistforUpdate(string MenuTypeName, int id) {
            var result = db.MenuTypes.SingleOrDefault(x => x.Name == MenuTypeName && x.ID != id);
            if (result == null)
            {
                return false;
            }
            else {
                return true;
            }
            
        }

        //Tạo mới MenuType
        public bool CreateMenuType(MenuType entity) {
            try
            {
                db.MenuTypes.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex) {
                return false;
            }
        }

        //Phương thức sửa MenuType
        public bool UpdateMenuType(MenuType entity) {
            try
            {
                var model = db.MenuTypes.Find(entity.ID);
                model.Name = entity.Name;
                model.status = entity.status;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex) {
                return false;
            }
        }

        //Phương thức chuyển status MenuType gọi từ Ajax
        public bool ChangeStatus(long id) {
            var menuType = db.MenuTypes.Find(id);
            menuType.status = !menuType.status;
            db.SaveChanges();
            return menuType.status;
        }
    }
}
