using System;
using Models.EF;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Models.DAO
{
    public class PublisherDAO
    {
        BookCoffeeShopDbContext db = null;
        public PublisherDAO()
        {
            db = new BookCoffeeShopDbContext();
        }

        //Danh sách Publisher dùng cho Admin
        public IEnumerable<Publisher> listPublisherAdmin()
        {
            return db.Publishers.OrderByDescending(x => x.status).ToList();
        }

        //Danh sách NXB dùng cho client
        public IEnumerable<Publisher> listPublisherClient()
        {
            return db.Publishers.Where(x => x.status == true).ToList();
        }

        //Tạo mới nhà xuất bản
        public bool CreatePublisher(Publisher entity)
        {
            try
            {
                db.Publishers.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Kiểm tra tồn tại của nhà xuất bản cho Create
        public bool ExistPublisherForCreate(string publisherName)
        {
            var result = db.Publishers.SingleOrDefault(x => x.Name == publisherName);
            if (result == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //Kiểm tra tồn tại của Publisher cho Update
        public bool ExistPublisherForUpdate(string publisherName, long id)
        {
            var result = db.Publishers.SingleOrDefault(x => x.Name == publisherName && x.ID != id);
            if (result == null)
            {
                return false;
            }
            else
            {

                return true;
            }
        }

        //chi tiết 1 Publisher
        public Publisher PublisherDetails(long id)
        {
            return db.Publishers.Find(id);
        }

        //Update Publisher
        public bool UpdatePublisher(Publisher entity)
        {
            try
            {
                var model = db.Publishers.Find(entity.ID);
                model.Name = entity.Name;
                model.Phone = entity.Phone;
                model.Email = entity.Email;
                model.Address = entity.Address;
                model.Content = entity.Content;
                model.status = entity.status;
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
            var publisher = db.Publishers.Find(id);
            publisher.status = !publisher.status;
            db.SaveChanges();
            return publisher.status;
        }
    }
}
