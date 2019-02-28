using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using CarBooking.DAL.Entities;

namespace CarBooking.DAL.EF
{
    public class CarBookingContex : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Order> Orders { get; set; }

        public CarBookingContex()
        {

        }
    }
}
