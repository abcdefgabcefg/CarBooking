using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarBooking.DAL.Entities;
using CarBooking.DAL.EF;

namespace CarBooking.DAL.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private CarBookingContext db = new CarBookingContext();

        public void Create(Order item)
        {
            db.Orders.Add(item);
        }

        public void Delete(int id)
        {
            db.Orders.Remove(db.Orders.Find(id));
        }

        public Order Get(int id)
        {
            return db.Orders.Find(id);
        }

        public IEnumerable<Order> GetAll()
        {
            return db.Orders;
        }

        public void Update(Order item)
        {
            var order = db.Orders.Find(item.ID);
            order.StartDate = item.StartDate;
            order.FinishDate = item.FinishDate;
            order.PassportNumber = item.PassportNumber;
            order.NeedDriver = item.NeedDriver;
            order.CarID = item.CarID;
        }
    }
}
