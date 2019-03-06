using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarBooking.DAL.Entities;
using CarBooking.DAL.EF;
using System.Data.Entity;

namespace CarBooking.DAL.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private CarBookingContext db;

        public OrderRepository(CarBookingContext context)
        {
            db = context;
        }

        public void Create(Order item)
        {
            item.Status = Status.NotConfirmed;
            item.Car.IsFree = false;

            decimal price = item.Car.Price;
            price += item.NeedDriver ? 10 : 0;
            price = Convert.ToDecimal((item.FinishDate - item.StartDate).TotalHours) * price;
            item.Price = price;

            db.Orders.Add(item);
        }

        public void Delete(int id)
        {
            var order = db.Orders.Find(id);
            if (order.Status == Status.Refused || order.Status == Status.WPFR || order.Status == Status.Finished)
            {
                order.Car.IsFree = true;
                db.Orders.Remove(order);
            }
        }

        public Order Get(int id)
        {
            return db.Orders.Find(id);
        }

        public IEnumerable<Order> GetAll()
        {
            foreach (var order in db.Orders)
            {
                if(order.FinishDate <= DateTime.Now)
                {
                    Finish(order);
                }
            }
            db.SaveChanges();
            return db.Orders;
        }

        public void Update(Order item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public IEnumerable<Order> GetNotConfirmedAndFinished()
        {
            return from order in GetAll()
                   where order.Status == Status.NotConfirmed || order.Status == Status.Finished
                   select order;
        }

        public IEnumerable<Order> GetUserOrders(int id)
        {
            return from order in GetAll()
                   where order.ClientID == id
                   select order;
        }

        public void Confirm(int id)
        {
            var order = Get(id);
            if (order.Status == Status.NotConfirmed)
            {
                order.Status = Status.Confirmed;
            }
        }

        public void Pay(int id)
        {
            var order = Get(id);
            if (order.Status == Status.Confirmed)
            {
                order.Status = Status.Paid; 
            }
        }

        public void Refuse(int id, string comment)
        {
            var order = Get(id);
            if (order.Status == Status.NotConfirmed)
            {
                order.Status = Status.Refused;
                order.ManagerComment = comment;
            }
        }

        private void Finish(Order order)
        {
            if(order.Status == Status.Paid)
            {
                order.Status = Status.Finished;
            }
        }

        public void ToRepair(int id, decimal repairPrice)
        {
            var order = Get(id);
            if (order.Status == Status.Finished)
            {
                order.Status = Status.WPFR;
                order.RepairPrice = repairPrice;
            }
        }
    }
}
