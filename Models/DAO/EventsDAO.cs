using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Models.DAO
{
    public class EventsDAO
    {
        BookCoffeeShopDbContext db = null;
        public EventsDAO()
        {
            db = new BookCoffeeShopDbContext();
        }

        //Phương thức lấy ra danh sách tất cả các sự kiện dùng cho Admin
        public List<_event> listEvents()
        {
            return db.events.OrderByDescending(x => x.Status).ToList();
        }
        //Phương thức lấy ra danh sách sự kiện dùng cho client
        public List<_event> listEventsClient()
        {
            return db.events.Where(x => x.Status == true).OrderByDescending(x=>x.EventDate).ToList();
        }

        //Lấy ra sự kiện mới nhất nhưng không thuộc tophot, số lượng theo quanlity
        /// <summary>
        /// select by quanlity
        /// </summary>
        /// <param name="quanlity"></param>
        /// <returns></returns>
        public List<_event> newestEvents(int quanlity) {
            
            return db.events.Where(x => x.Status == true && x.TopHot == false)
                .OrderByDescending(x => x.CreatedDate).Take(quanlity).ToList();
        }

        //Lấy ra  sự kiện thuộc tophot
        /// <summary>
        /// Nếu index =0 thì lấy ra tất cả những sự kiện thuộc tophot
        /// Nếu index =1 thì lấy ra quanlity sự kiện tophot
        /// </summary>
        /// <param name="index"></param>
        /// <param name="quanlity"></param>
        /// <returns></returns>
        public List<_event> TopHotEvents(int index, int  quanlity =0) {
            List<_event> list=null;
            if (index == 0)
            {
                list = db.events.Where(x => x.Status == true && x.TopHot == true).ToList();
            }
            else if (index == 1) {
                list = db.events.Where(x => x.Status == true && x.TopHot == true).OrderByDescending(x => x.CreatedDate).Take(quanlity).ToList();
            }
            return list;
        }
  

        //Kiem tra su ton tai cua 1 sự kiện dùng cho Create
        public bool EventExist(string eventName)
        {
            var result = db.events.SingleOrDefault(x => x.Name == eventName);
            if (result == null)
            {
                return false;
            }
            else
            {

                return true;
            }
        }
        //Kiểm tra sự tồn tại của sự kiện dùng cho updte
        public bool EventExistForUpdate(string eventName, int id)
        {
            var result = db.events.SingleOrDefault(x => x.Name == eventName && x.ID!=id);
            if (result == null)
            {
                return false;
            }
            else
            {

                return true;
            }
        }


        //Lấy ra 1 sự kiện dựa theo id
        public _event EventDetail(int id)
        {
            return db.events.Find(id);
        }

        //Phương thức tạo mới sự kiện
        public bool CreateEvent(_event entity)
        {
            try
            {
                entity.CreatedDate = DateTime.Now;
                db.events.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Xóa sự kiện
        public bool DeleteEvent(int id)
        {
            try
            {
                var entity = db.events.Find(id);
                db.events.Remove(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Update sự kiện
        public bool UpdateEvent(_event entity)
        {
            try
            {
                var model = db.events.Find(entity.ID);
                model.EventDate = entity.EventDate;
                model.StartTime = entity.StartTime;
                model.EndTime = entity.EndTime;
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
            var Event = db.events.Find(id);
            Event.Status = !Event.Status;
            db.SaveChanges();
            return Event.Status;
        }

        //Phương thức này thay đổi tình trạng Tophot 
        public bool ChageTopHot(long id)
        {
            var Event = db.events.Find(id);
            Event.TopHot = !Event.TopHot;
            db.SaveChanges();
            return Event.TopHot;
        }
    }
}
