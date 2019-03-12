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
            item.Status = Status.Created;
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
            if (order.Status == Status.Refused || order.Status == Status.RepairPaid
                || order.Status == Status.Finished || order.Status == Status.Created
                || order.Status == Status.NotAnswered || order.Status == Status.Confirmed
                || order.Status == Status.Paid)
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
                if (order.FinishDate <= DateTime.Now)
                {
                    Finish(order);
                }
                if (order.StartDate <= DateTime.Now)
                {
                    MakeNotAnswered(order);
                }
                if(order.Status == Status.NotAnswered && order.StartDate.AddMinutes(30) < DateTime.Now)
                {
                    Delete(order.ID);
                }
                if(order.Status == Status.Refused && order.StartDate.AddDays(3) < DateTime.Now)
                {
                    Delete(order.ID);
                }
            }
            db.SaveChanges();
            return db.Orders;
        }

        public void Update(Order item)
        {
            decimal price = item.Car.Price;
            price += item.NeedDriver ? 10 : 0;
            price = Convert.ToDecimal((item.FinishDate - item.StartDate).TotalHours) * price;
            item.Price = price;

            db.Entry(item).State = EntityState.Modified;
        }

        public IEnumerable<Order> CreateDashBoard()
        {
            return from order in GetAll()
                   where order.Status == Status.Created || order.Status == Status.Finished || order.Status == Status.RepairPaid
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
            if (order.Status == Status.Created)
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
            if (order.Status == Status.Created)
            {
                order.Status = Status.Refused;
                order.ManagerComment = comment;
            }
        }

        private void Finish(Order order)
        {
            if (order.Status == Status.Paid)
            {
                order.Status = Status.Finished;
            }
        }

        public void ToRepair(int id, decimal repairPrice, string comment)
        {
            var order = Get(id);
            if (order.Status == Status.Finished)
            {
                order.Status = Status.WaitPaymentForRepair;
                order.RepairPrice = repairPrice;
                order.ManagerComment = comment;
            }
        }

        private void MakeNotAnswered(Order order)
        {
            if (order.Status == Status.Created || order.Status == Status.Confirmed)
            {
                order.Status = Status.NotAnswered;
            }
        }

        public void PayRepair(int id)
        {
            var order = Get(id);
            if (order.Status == Status.WaitPaymentForRepair)
            {
                order.Status = Status.RepairPaid;
            }
        }
    }
}
