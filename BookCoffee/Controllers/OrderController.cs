using Models.DAO;
using Models.EF;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookCoffee.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(OrderModel entity )
        {
            if (ModelState.IsValid)
            {
                var order = new Order();
                order.Name = entity.Name;
                order.Email = entity.Email;
                order.Mobile = entity.Mobile;
                order.HoldDate = entity.HoldDate;
                order.Remark = entity.Remark;

                var id  = new OrderDAO().Insert(order);

                if (id !=0)
                {
                    var orderdetail = new OrderDetail();
                    orderdetail.Order_ID = id;
                    orderdetail.Quantity = entity.Quantity;
                    bool result = new OrderDetailDAO().insert(orderdetail);
                    if (result)
                    {
                        this.ModelState.Clear();
                        ModelState.AddModelError("OrderTableErr", "Đã đặt bàn thành công");
                        OrderModel emptyModel = new OrderModel();
                        return View();
                    }
                    else {
                        ModelState.AddModelError("OrderTableErr", "Chưa đặt bàn được, vui lòng thử lại");
                    }
                    
                }
                else {
                    ModelState.AddModelError("OrderTableErr", "Chưa đặt bàn được, vui lòng thử lại");
                }
            }
            else {
               
            }
            return View(entity);
        }
    }
}