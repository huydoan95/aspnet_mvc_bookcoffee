using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.ViewModels;

namespace Models.DAO
{
    public class OrderDAO
    {
        BookCoffeeShopDbContext db = null;
        public OrderDAO() {
            db = new BookCoffeeShopDbContext();
        }

        public long Insert(Order entity) {
            db.Orders.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        //lấy ra số lương đơn đặt bàn chưa giải quyết
        public int quanlityOfTableBookingOrder() {
            int quanlity = db.Orders.Where(x => x.status == false).Count();
            return quanlity;
        }

        //View list TableBooking
        public List<OrderModel> listTableBooking() {
            var model = from a in db.Orders
                        join b in db.OrderDetails
                        on a.ID equals b.Order_ID
                        where a.ID == b.Order_ID
                        select new OrderModel()
                        {
                            ID=a.ID,
                            Name=a.Name,
                            Mobile=a.Mobile,
                            Address=a.Address,
                            Email=a.Email,
                            HoldDate=a.HoldDate,
                            Quantity=(b.Quantity.HasValue==true?(int)b.Quantity : 0),
                            status=a.status
                        };
            return model.ToList();
        }
    }
}
