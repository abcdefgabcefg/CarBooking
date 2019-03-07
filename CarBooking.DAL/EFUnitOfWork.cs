using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarBooking.DAL.Repositories;
using CarBooking.DAL.EF;

namespace CarBooking.DAL
{
    public class EFUnitOfWork : IDisposable
    {
        private CarBookingContext db = new CarBookingContext();
        private CarRepository cars;
        private OrderRepository orders;
        private UserRepository users;
        public CarRepository Cars
        {
            get
            {
                if(cars == null)
                {
                    cars = new CarRepository(db);
                }
                return cars;
            }
        }
        public OrderRepository Orders
        {
            get
            {
                if(orders == null)
                {
                    orders = new OrderRepository(db);
                }
                return orders;
            }
        }

        public UserRepository Users
        {
            get
            {
                if(users == null)
                {
                    users = new UserRepository(db);
                }
                return users;
            }
        }

        public void Save()
        {
            try
            {
                //throw new Exception();
                db.SaveChanges();
            }
            catch(Exception e)
            {
                throw new Exception("Error when trying to save changes to database", e);
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing && db != null)
                {
                    db.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
