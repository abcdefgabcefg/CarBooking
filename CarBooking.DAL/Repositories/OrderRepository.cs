﻿using System;
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
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
