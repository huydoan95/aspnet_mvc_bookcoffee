using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class OrderDetailDAO
    {
        BookCoffeeShopDbContext db = null;
        public OrderDetailDAO() {
            db = new BookCoffeeShopDbContext();
        }

        public bool insert(OrderDetail entity) {
            db.OrderDetails.Add(entity);
            db.SaveChanges();
            return true;

        }
    }
}
