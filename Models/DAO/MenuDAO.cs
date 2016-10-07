using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class MenuDAO
    {
        BookCoffeeShopDbContext db = null;
        public MenuDAO() {
            db = new BookCoffeeShopDbContext();
        }
        //Lấy ra danh sách  tin tức
        public List<Menu> listMenu()
        {
            return db.Menus.OrderByDescending(x=>x.status).ToList();
        }

        //Lấy ra danh sách theo MenuType
        public List<Menu> listMenuByMenuType(int id) {
            return db.Menus.Where(x => x.TypeID == id && x.status==true).OrderBy(x => x.DisplayOrder).ToList();
        }

        //Kiểm tra sự tồn tại của 1 Menu trước khi Create
        public bool MenuExits(string MenuName)
        {
            var result = db.Menus.SingleOrDefault(x => x.Text == MenuName);
            if (result == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //Kiểm tra sự tồn tại của 1 Menu trước khi Update
        public bool MenuExitsForUpdate(string MenuName, int id) {
            var result = db.Menus.SingleOrDefault(x => x.Text == MenuName && x.ID != id);
            if (result == null)
            {
                return false;
            }
            else {
                return true;
            }
        }


        //Lấy ra 1 Menu theo ID
        public Menu menuDetail(int id) {
            return db.Menus.Find(id);
        }

        //Tạo mới Menu
        public bool CreateMenu(Menu entity) {
            try
            {
                db.Menus.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex) {
                return false;
            }
        }

        //Update Menu information
        public bool UpdateMenu(Menu entity) {
            try
            {
                var model = db.Menus.Find(entity.ID);
                model.Text = entity.Text;
                model.Link = entity.Link;
                model.DisplayOrder = entity.DisplayOrder;
                model.Target = entity.Target;
                model.status = entity.status;
                model.TypeID = entity.TypeID;
                db.SaveChanges();
                return true;
            }
            catch {
                return false;
            }
        }

        //Phương thức này sử dụng cho ajax
        public bool ChangeStatus(long id)
        {
            var menu = db.Menus.Find(id);
            menu.status = !menu.status;
            db.SaveChanges();
            return menu.status;
        }
    }
}
