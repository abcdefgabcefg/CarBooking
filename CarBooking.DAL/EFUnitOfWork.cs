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
        public CarRepository Cars { get; }
        public OrderRepository Orders { get; }

        public void Save()
        {
            try
            {
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
