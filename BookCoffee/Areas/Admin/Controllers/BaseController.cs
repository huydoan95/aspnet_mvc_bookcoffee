using BookCoffee.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookCoffee.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null) {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(
                    new { controller = "Login", action = "Index", Area = "Admin" }));
            }
            base.OnActionExecuting(filterContext);
        }

        //Phương thức nhận kiểu thông báo và gán thông báo cho user 
        protected void SetAltert(string message, int type) {
            TempData["AlertMessage"] = message;
            if (type == 0)
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == 1)
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == 2) {
                TempData["AlertType"] = "alert-danger";
            }
        }
    }
}